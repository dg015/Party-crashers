using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int m_score;
    public int PlayerScoreValue { get { return m_score; } } 
    public void UpdateScore(int value)
    {
        m_score += value;
    }


    private void Awake()
    {
        multyplayerManager.Instance.AddPlayerScoreToList(this);
    }

    private void OnDestroy()
    {
        multyplayerManager.Instance.RemovePlayerScoreToList(this);
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
