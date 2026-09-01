using BrunoToolsTimeUtil;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] private int m_scoreValue;
    [SerializeField] private int m_taskTimeLimit;

    [SerializeField] private Image m_UIbar;
    [SerializeField] private TaskScriptableObject m_taskData;

    private TimerScript m_timerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        m_timerScript = new TimerScript();
    }

    private void Update()
    {
        TaskTimer(m_taskTimeLimit);
        UpdateUIBar(m_UIbar, m_timerScript.GetElapsedTime());
    }

    private void TaskTimer(int taskTimeLimit)
    {
        if (m_timerScript.TickDown(m_taskTimeLimit, Time.deltaTime))
        {
            EndTask();
        }
    }

    public void GetTaskData(TaskScriptableObject taskData)
    {
        m_taskData = taskData;
        UpdateTaskData(taskData);
    }

    private void UpdateTaskData(TaskScriptableObject taskData)
    {
        m_taskTimeLimit = taskData.timeDuration;
        m_scoreValue = taskData.scoreValue;
    }

    //Made into a method, maybe later we can call use this to our advantage
    private void EndTask()
    {
        Destroy(gameObject);
    }

    private void UpdateUIBar(Image UIbar, float barValue)
    {
        UIbar.fillAmount = barValue/m_taskTimeLimit;
    }
}
