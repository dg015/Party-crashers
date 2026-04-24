using UnityEngine;
using UnityEngine.UI;

public class ShatteredGlass : MonoBehaviour, Iinteractable
{

    [Header("Effects")]
    [SerializeField] private float slowdownEffect;
    [SerializeField] private LayerMask mask;
    [SerializeField] private bool playerDetected;
    [SerializeField] private PlayerController recentPlayer;

    [Header("timer")]
    [SerializeField] private float maxTime;
    [SerializeField] private float currentProgress;

    //super scuffed solution
    [Header("interaction timeout")]
    [SerializeField] private float interactTimeout = 0.2f;
    private float interactTimer;

    [Header("UI")]
    [SerializeField] private Image bar;



    void Update()
    {
        currentProgress = Mathf.Clamp(currentProgress, 0, maxTime);

        bar.fillAmount = Mathf.Clamp01(currentProgress / maxTime);

        

        if (interactTimer > 0f)
        {
            interactTimer -= Time.deltaTime;

            increaseTimer();
            if (currentProgress >= maxTime)
            {
                currentProgress = 0;
                Destroy(gameObject);
            }
        }
        else
        {
            decreaseTimer();
        }
        if(currentProgress == 0)
        {
            bar.gameObject.SetActive(false);
        }
    }

    public void Interact(GameObject interactor)
    {
        bar.gameObject.SetActive(true);
        Debug.Log("interacted with");
        interactTimer = interactTimeout;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && !playerDetected)
        {
            recentPlayer = other.GetComponentInParent<PlayerController>();
            playerDetected = true;
            recentPlayer.slowDownPlayer(playerDetected, slowdownEffect);
            Debug.Log("slow down player");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        playerDetected = false;
        recentPlayer.slowDownPlayer(playerDetected, slowdownEffect);
    }

    private void OnDestroy()
    {
        playerDetected = false;
       
    }

    private void increaseTimer()
    {
        currentProgress += Time.deltaTime;
    }

    private void decreaseTimer()
    {
        currentProgress -= Time.deltaTime;
    }

}
