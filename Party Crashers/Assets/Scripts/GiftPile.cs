using UnityEngine;

public class GiftPile : MonoBehaviour , Iinteractable
{
    [SerializeField] private GameObject giftPrefab;
    [SerializeField] private Transform spawnGiftLocation;
    [SerializeField] private float shootGiftForce;
    [SerializeField] private float radius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("interacted with");
        spawnGift();

    }

    private void spawnGift()
    {
        GameObject newGift;
        newGift = Instantiate (giftPrefab, spawnGiftLocation.position,Quaternion.identity);

        Rigidbody giftRB = newGift.GetComponent<Rigidbody>();

        giftRB.AddExplosionForce(shootGiftForce, spawnGiftLocation.up,radius);
    }

}
