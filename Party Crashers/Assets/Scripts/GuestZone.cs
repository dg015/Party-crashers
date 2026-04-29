using Unity.Mathematics;
using UnityEngine;

public class GuestZone : MonoBehaviour
{


    [Header("Guests")]
    [SerializeField] private int maxNumberOfGuests;
    private int currentNumberOfGuests;
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private float spawnTime;
    private float currentTime;

    [Header("detection")]
    [SerializeField] private string tagName;
    [SerializeField] private bool autoFill;
    [SerializeField] private BoxCollider boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(autoFill == true)
        {
            for (int i = 0; i < maxNumberOfGuests; i++)
            {
                Instantiate(guestPrefab, chooseRandomPoint(boxCollider.bounds), quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentNumberOfGuests < maxNumberOfGuests)
            guestCooldown();
        spawnGuest();
    }

    private Vector3 chooseRandomPoint(Bounds bounds)
    {
        return new Vector3
        (
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            transform.position.y,
             UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }


    //spawn guests
    private void spawnGuest()
    {
        if (currentNumberOfGuests <= maxNumberOfGuests && currentTime >= spawnTime)
        {
            Instantiate(guestPrefab, chooseRandomPoint(boxCollider.bounds), quaternion.identity);
        }
    }

    //cooldown beforeSpwaning guests
    private void guestCooldown()
    {
        currentTime = Mathf.Clamp(currentTime, 0, spawnTime);

        if (currentTime < spawnTime)
            currentTime = +Time.deltaTime;

        if(currentTime>= spawnTime)
        {
            currentTime = 0;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
    
        if (other.gameObject.CompareTag(tagName))
        {
            currentNumberOfGuests++;

        }
    
    }

}
