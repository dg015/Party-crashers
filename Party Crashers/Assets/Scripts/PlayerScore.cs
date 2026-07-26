using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int m_score;

    public void UpdateScore(int value)
    {
        m_score += value;
    }

    public void ResetScore()
    {
        m_score = 0;
    }

    public int GetScore()
    {
        return m_score;
    }
}
