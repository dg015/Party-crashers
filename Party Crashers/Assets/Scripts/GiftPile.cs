using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GiftPile : MonoBehaviour , Iinteractable
{

    [Header("Gift spawner")]
    [SerializeField] private GameObject giftPrefab;
    [SerializeField] private Transform spawnGiftLocation;
    [SerializeField] private float shootGiftForce;

    [SerializeField] private float m_giftRandomDirMod;

    [Header("Timer")]
    [SerializeField] private float maxTime;
    [SerializeField] private float currentProgress;

    //super scuffed solution
    [Header("Interaction timeout")]
    [SerializeField] private float interactTimeout = 0.2f;
    private float interactTimer;

    [Header("UI")]
    [SerializeField] private Image bar;



    // Update is called once per frame
    void Update()
    {
        currentProgress = Mathf.Clamp(currentProgress, 0, maxTime);

        bar.fillAmount = Mathf.Clamp01(currentProgress / maxTime);

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

        //add silly little animation for the gift to grow on size
        newGift.transform.localScale = Vector3.zero;
        newGift.transform.DOScale(Vector3.one, .25f);

        
        Rigidbody giftRB = newGift.GetComponent<Rigidbody>();
        //generate a bit of randomness to go up
        float RandomnessDirX = Random.Range(-m_giftRandomDirMod, m_giftRandomDirMod);
        float RandomnessDirZ = Random.Range(-m_giftRandomDirMod, m_giftRandomDirMod);

        giftRB.AddForce(new Vector3 (RandomnessDirX, 1, RandomnessDirZ) * shootGiftForce,ForceMode.Impulse);
    }

}
