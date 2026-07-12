using DG.Tweening;
using UnityEngine;

public class GiftDropoffZone : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private BoxCollider col;
    [SerializeField] private string tagName;
    [SerializeField] private float dissapearTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(tagName))
        {
            //make cool effect
            other.transform.DOScale(Vector3.zero, dissapearTime);
            //destroy gift
            Destroy(other.gameObject, dissapearTime);
            
            // increase points here

        }
    }
}
