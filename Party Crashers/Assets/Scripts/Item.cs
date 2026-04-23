using UnityEngine;




public class Item : MonoBehaviour, Iinteractable
{

    [SerializeField] private bool isBeingHeld;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;
    


    public void Interact(GameObject interactor)
    {
        Debug.Log("interacted with");
        player = interactor;
        holdItem();
    }


 

    private void holdItem()
    {
        //get componenent
        Transform itemHoldLocation = player.GetComponent<PlayerController>().itemHoldLocation.transform;

        //setting the object new position
        transform.position = itemHoldLocation.position;
        transform.SetParent(itemHoldLocation.transform);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
