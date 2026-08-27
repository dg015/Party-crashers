using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Tasks")]
    [SerializeField] private List<TaskScriptableObject> m_taskList = new List<TaskScriptableObject>();
    [SerializeField] private List<TaskScriptableObject> m_activeTasks = new List<TaskScriptableObject>();

    [Header("Zones")]
    [SerializeField] private List<GiftDropoffZone> m_dropOffZones = new List<GiftDropoffZone>();
    [SerializeField] private List<GuestZone> m_guestZones = new List<GuestZone>();
    public List<GuestZone> GuestZoneList { get { return m_guestZones; }}

    [Header("UI")]
    [SerializeField] private GameObject m_taskUIPrefab;
    [SerializeField] private Transform m_taskUIList;
    private float runTime;
    public static TaskManager Instance { get; private set; }

    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void CreateTaskTime(float coolDown)
    {
        runTime += Time.deltaTime;
        if(runTime >= coolDown)
        {
            GenerateNewTask();
            runTime = 0;
        }
    }

    private void Update()
    {
        CreateTaskTime(5f);
    }

    public void AddGiftZoneToList(GiftDropoffZone zone)
    {
        m_dropOffZones.Add(zone);
        zone.GiftDeliveredEvent += CheckForActiveTask;
    }

    public void AddGuestZoneToList(GuestZone zone)
    {
        m_guestZones.Add(zone);
        
    }

    private void OnDisable()
    {
        foreach (var zone in m_dropOffZones)
        {
            zone.GiftDeliveredEvent -= CheckForActiveTask;
        }
    }

    public void GenerateNewTask()
    {
        //generate random task and add to the list
        int rng = Random.Range(0, m_taskList.Count);
        m_activeTasks.Add(m_taskList[rng]);

        //instantiate the prefab
        GameObject newTaskElement = Instantiate(m_taskUIPrefab, m_taskUIList.position, Quaternion.identity, m_taskUIList);

        //Update content from task
        newTaskElement.GetComponentInChildren<TextMeshProUGUI>().text = m_taskList[rng].name;

        //we testing this later
        //newTaskElement.transform.Find("Icon").GetComponent<Image>().sprite = m_taskList[rng].taskIcon;

        Task newTaskScript = newTaskElement.GetComponent<Task>();
        newTaskScript.GetTaskData(m_taskList[rng]);
    }

    private void CheckForActiveTask(PlayerScore player,TaskType taskPerformed)
    {
        foreach(var task in m_activeTasks)
        {
            if(task.taskType == taskPerformed)
            {
                player.UpdateScore(task.scoreValue);
                m_activeTasks.Remove(task);
                return;
            }
        }
    }

}
