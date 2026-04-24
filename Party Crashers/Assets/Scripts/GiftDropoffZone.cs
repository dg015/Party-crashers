using UnityEngine;

public class GiftDropoffZone : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private BoxCollider col;
    [SerializeField] private string tagName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(tagName))
        {
            //destroy gift
            Destroy(other.gameObject, 1f);
            
            // increase points here

        }
    }
}
