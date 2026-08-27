using NodeCanvas.Tasks.Actions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public static int numberOfGuests;
    private List<GameObject> m_guestList = new List<GameObject>();

    private List<GameObject> m_drunkGuestList = new List<GameObject>();
    private int m_numberOfDrunkGuests => m_drunkGuestList.Count;
    [SerializeField] private int m_maxNumberOfDrunkGuests;

    public static GuestManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetGuestToDrink(int Amount)
    {
        if(m_numberOfDrunkGuests >= m_maxNumberOfDrunkGuests)
        {
            Debug.LogWarning("MAX NUMBER OF DRUNK GUESTS " + m_numberOfDrunkGuests + "OUT OF " + m_maxNumberOfDrunkGuests);
            return;
        }
        //add as many guests as the amount sets for
        int currentAdded = 0;

        //if theres its adding more then able just add just enough to top it off
        if (Amount + m_numberOfDrunkGuests >= m_maxNumberOfDrunkGuests)
            Amount = (int)MathF.Abs (m_maxNumberOfDrunkGuests - m_numberOfDrunkGuests);

        while (currentAdded < Amount)
        {
            int rng = UnityEngine.Random.Range(0, m_guestList.Count);
            //pick a random guest, if they're already drinking add another
            //if its not drinking make drink
            if (!m_guestList[rng].GetComponent<Wander>().IsDrinking == true)
            {
                currentAdded++;
                m_guestList[rng].GetComponent<Wander>().IsDrinking = true;
            }
            else
            {
                // idk how to but re-roll
            }
        }
    }

    public void SetDrunkValue(int value)
    {
        //safety nets to not roll any out of bounds or set higher then 100% (We're working on percentages here people)
        if(value > 100)
            gameObject.GetComponent<Wander>().drunkPecentege = 100;
        else if (value < 0)
            gameObject.GetComponent<Wander>().drunkPecentege = 0;
        else
            gameObject.GetComponent<Wander>().drunkPecentege = value; 
    }


    public void AddGuest(GameObject guest)
    {
        //call when a guest is spawned
        //quick check if the guest already exists in the list
        if(!m_guestList.Contains(guest))
        {
            numberOfGuests++;
            m_guestList.Add(guest);
            Debug.Log("Guest Added | " + guest.name.ToString());
        }
    }

    public void RemoveGuest(GameObject guest)
    {
        //call when a guest is removed (probably kicked out of the event)
        numberOfGuests--;
        m_guestList.Remove(guest);
    }
}
