using UnityEditorInternal;
using UnityEngine;




public class Item : MonoBehaviour, Iinteractable
{
    [Header("main")]
    [SerializeField] private bool isBeingHeld;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;

    [Header("types")]
    [SerializeField] public bool consumable;
    [SerializeField] private bool breakable;

    [Header("Breakable")]
    [SerializeField] private GameObject brokenGlass;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float breakingSpeed;

    public void Interact(GameObject interactor)
    {
        Debug.Log("interacted with");
        player = interactor;
        holdItem();
    }


    public void consume(GameObject target)
    {
        //do effects here

        Destroy(gameObject, 1f);
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

    private void shatter()
    {
        Instantiate(brokenGlass,transform.position,Quaternion.identity);
        Destroy (gameObject);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != mask && rb.linearVelocity.magnitude >= breakingSpeed && breakable)
        {
            shatter();
        }
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
