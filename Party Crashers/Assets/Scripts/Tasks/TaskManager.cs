using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> m_taskList = new List<Task>();
    [SerializeField] private List<Task> m_activeTasks = new List<Task>();

    [SerializeField] private List<GiftDropoffZone> m_dropOffZones = new List<GiftDropoffZone>();

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

    public void AddZoneToList(GiftDropoffZone zone)
    {
        m_dropOffZones.Add(zone);
        zone.GiftDeliveredEvent += CheckForActiveTask;
    }

    private void OnDisable()
    {
        foreach (var zone in m_dropOffZones)
        {
            zone.GiftDeliveredEvent -= CheckForActiveTask;
        }
    }

    private void GenerateNewTask()
    {
        int rng = Random.Range(0, m_taskList.Count);
        
        for (int i = 0; i < m_taskList.Count; i++) 
        {
            m_activeTasks.Add(m_taskList[i]);
        }

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
