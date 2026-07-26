using System;
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


    private void CheckForActiveTask(PlayerScore player)
    {
        Debug.Log("Task complete");
        player.UpdateScore(20);
    }

}
