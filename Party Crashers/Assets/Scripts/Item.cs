using UnityEngine;




public class Item : MonoBehaviour, Iinteractable
{

    [SerializeField] private bool isBeingHeld;
   

    public void Interact()
    {
        Debug.Log("interacted with");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
