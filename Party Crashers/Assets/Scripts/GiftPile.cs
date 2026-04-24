using UnityEngine;

public class GiftPile : MonoBehaviour , Iinteractable
{

    [Header("gift spawner")]
    [SerializeField] private GameObject giftPrefab;
    [SerializeField] private Transform spawnGiftLocation;
    [SerializeField] private float shootGiftForce;
    [SerializeField] private float radius;

    [Header("timer")]
    [SerializeField] private float maxTime;
    [SerializeField] private float currentProgress;

    //super scuffed solution
    [Header("interaction timeout")]
    [SerializeField] private float interactTimeout = 0.2f;
    private float interactTimer;



    // Update is called once per frame
    void Update()
    {
        currentProgress = Mathf.Clamp(currentProgress, 0, maxTime);

        if (interactTimer >0f)
        {
            interactTimer-= Time.deltaTime;

            increaseTimer();
            if (currentProgress >= maxTime)
            {
                currentProgress = 0;
                spawnGift();
            }      
        }
        else
        {
            decreaseTimer();
        }
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("interacted with");
        interactTimer = interactTimeout;
    }



    private void increaseTimer()
    {
        currentProgress+= Time.deltaTime;

    }

    private void decreaseTimer()
    {
       
        currentProgress -= Time.deltaTime;
    }


    private void spawnGift()
    {
        GameObject newGift;
        newGift = Instantiate (giftPrefab, spawnGiftLocation.position,Quaternion.identity);

        Rigidbody giftRB = newGift.GetComponent<Rigidbody>();

        giftRB.AddExplosionForce(shootGiftForce, spawnGiftLocation.up,radius);
    }

}
