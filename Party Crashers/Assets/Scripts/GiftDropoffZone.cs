using System;
using DG.Tweening;
using UnityEngine;

public class GiftDropoffZone : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private BoxCollider col;
    [SerializeField] private string tagName;
    [SerializeField] private float dissapearTime;

    //scoring
    public event Action<PlayerScore> GiftDeliveredEvent;
    [SerializeField] private int taskID;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TaskManager.Instance.AddZoneToList(this);
        col = gameObject.GetComponent<BoxCollider>();
    }

    private void ColllectGift(Collider other)
    {
        if (other.gameObject.CompareTag(tagName))
        {
            //make cool effect
            other.transform.DOScale(Vector3.zero, dissapearTime);
            //destroy gift
            Destroy(other.gameObject, dissapearTime);

            //scuffed as hell
            PlayerScore player = other.GetComponent<Item>().
                GetLastPlayer().
                GetComponent<PlayerScore>();

            // increase points here
            GiftDeliveredEvent?.Invoke(player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ColllectGift(other);
    }
}
