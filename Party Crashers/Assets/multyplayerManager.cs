using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class multyplayerManager : MonoBehaviour
{
    public static multyplayerManager Instance { get; private set; }
    private List<PlayerScore> m_playerScoreList;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddPlayerScoreToList(PlayerScore scoreScript)
    {
        if(scoreScript != null)
            m_playerScoreList.Add(scoreScript);
    }


    public PlayerScore CheckHighestScore()
    {
        int highestScore = 0;
        PlayerScore bestScorePlayer = null;
        foreach (PlayerScore score in m_playerScoreList)
        {
            if (score.PlayerScoreValue > highestScore)
            {
                highestScore = score.PlayerScoreValue;
                bestScorePlayer = score;
            }
        }
        return bestScorePlayer;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
