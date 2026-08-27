using BrunoTools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private float m_maxTimer;
    [SerializeField] private float m_currentTime;
    [SerializeField] private bool m_isRunning;

    [Header("Bonus / Deduct")]
    [SerializeField] private float m_bonusAddTime;
    [SerializeField] private float m_deductTime;

    [Header("Settings")]
    [SerializeField] private bool m_isPaused;

    [SerializeField] private bool m_isSpedUp;
    [SerializeField] private float m_spedUpValue = 2f;

    [SerializeField] private bool m_isSlowedDown;
    [SerializeField] private float m_slowDownTime = .5f;

    private TimerScript m_timerScript;


    public bool IsRunning { get { return m_isRunning; } }

    public static GameTimer Instance { get; private set; }


    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_timerScript = new TimerScript();
    }

    //call this on start game event
    private void StartTimer()
    {
        m_isRunning = true;
    }

    public void DisplayOnUIText(TextMeshProUGUI textItem)
    {
        textItem.text = m_timerScript.FormatTimeMinutes();
    }

    public void DisplayOnUIFillBar(Image fillBar)
    {
        fillBar.fillAmount = m_timerScript.GetElapsedTime()/m_maxTimer;
    }


    private void CheckTimer()
    {
        if (m_timerScript.GetElapsedTime() <= 0)
        {
            m_isRunning = false;
            Debug.Log("Time's over");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (m_isRunning)
        {
            m_timerScript.TickDown(m_maxTimer,Time.deltaTime);
        }
        CheckTimer();
    }
}
