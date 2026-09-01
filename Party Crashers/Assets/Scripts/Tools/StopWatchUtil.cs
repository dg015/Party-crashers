
namespace BrunoToolsTimeUtil
{
    public class StopWatchUtil
    {
        private float m_elapsedTime;
        private bool m_isPaused;
        private float[] m_laps;

        public StopWatchUtil (int numberOfLaps)
        {
            m_laps = new float [numberOfLaps];
        }


        public void StartStopWatch(float deltaTime)
        {
            if(m_isPaused)
                return;

            m_elapsedTime += deltaTime;
        }

        private int GetEmptySlot()
        {
            for (int i = 0; i < m_laps.Length; i++)
            {
                if( m_laps[i] == 0)
                    return i;
            }

            //if all slots are cleared clear up the first one and use it 
            m_laps[0] = 0;
            return 0;            
        }

        public void SaveLap()
        {
            m_laps[GetEmptySlot()] = m_elapsedTime;
        }

        public float[] GetAllLaps()
        {
            return m_laps;
        }

        public float GetAvarageTime()
        {
            float avarageTime = 0;
            for (int i = 0; i < m_laps.Length; i++)
            {
                avarageTime += m_laps[i];
            }
            return avarageTime/ m_laps.Length;
        }

        public float GetLapAt(int arrayIndex)
        {
            return m_laps[arrayIndex];
        }

        public void ClearLapsArray()
        {
            for (int i = 0; i < m_laps.Length; i++)
            {
                m_laps [i] = 0;
            }
        }

        public void SetPauseStatus(bool isPaused)
        {
            m_isPaused = isPaused;
        }

        public bool GetStopWatchStatus()
        {
            return m_isPaused;
        }

        public float GetStopwatchTime()
        {
            return m_elapsedTime; 
        }

        public void ResetStopWatch()
        {
            m_elapsedTime = 0;
        }

    }


}