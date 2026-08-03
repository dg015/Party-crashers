using UnityEngine;

public class TimerScript
{
    private float m_elapsedTime;
    private bool m_isPaused;
    private bool m_isComplete;
    private bool m_isAutoReset;
    private bool m_autoPause;
    private float m_recentDuration;

    public bool Tick(float duration, float deltaTime)
    {
        if (m_isPaused)
            return false;

        //if it has autopause and it has already been complete then stop it
        if (m_isComplete && m_autoPause)
            return false;

        m_recentDuration = duration;

        m_elapsedTime += deltaTime;
        if (m_elapsedTime >= duration)
        {
            m_isComplete = true;

            //if its auto reset then reset timer
            if(m_isAutoReset)
                m_elapsedTime = 0;

            return true;
        }
        else
        {
            m_isComplete = false;
            return false;
        }
    }

    public float GetElapsedTime()
    {
        return m_elapsedTime;
    }

    //Set the timer to auto reset (back to 0) when finished)
    public void SetRepeating(bool status)
    {
        m_isAutoReset = status;
    }

    //Sets auto pause so its stops after its complete for the first time
    //(good if u want triggers)
    public void SetAutoPause(bool status)
    {
        m_autoPause = status;
    }

    //Returns if its complete or not
    public bool IsComplete()
    {
        return m_isComplete;
    }

    //Toggles pause
    public void PauseTimer(bool status)
    {
         m_isPaused = status;
    }

    //Resets timer
    public void ResetElapsedTime()
    {
        m_elapsedTime = 0;
        m_isComplete = false;
    }

    public float GetNormalizedProgress()
    {
        if (m_recentDuration <= 0)
            return 0;

        float normalizedTime = m_elapsedTime / m_recentDuration;
        return normalizedTime;
    }
}
