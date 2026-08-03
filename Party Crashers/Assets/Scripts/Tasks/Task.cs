using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] private float m_remainingTime;
    [SerializeField] private int m_scoreValue;
    private int m_taskTimeLimit;

    [SerializeField] private Image m_UIbar;
    [SerializeField] private TaskScriptableObject m_taskData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
        TaskTimer(m_taskTimeLimit);
        UpdateUIBar(m_UIbar, m_remainingTime);
    }

    private void TaskTimer(int taskTimeLimit)
    {
        m_remainingTime = Mathf.Clamp (taskTimeLimit - Time.deltaTime,0, taskTimeLimit);

        if(m_remainingTime <= 0)
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
