using UnityEngine;

public class Guest : MonoBehaviour
{
    private int m_drunkLevel = 0;
    private bool m_isDrunk = false;

    //for funsies later
    private string m_guestName;

    private void Awake()
    {
        GuestManager.Instance.AddGuest(gameObject);
    }

    private void OnDisable()
    {
        GuestManager.Instance.RemoveGuest(gameObject);
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
