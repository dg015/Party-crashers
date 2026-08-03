using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a tool to generate runtimeIDs for scripts and objects;
/// </summary>
public class RunTimeIDGenerator : MonoBehaviour
{
    [SerializeField] private int m_IDLenght;
    [SerializeField] private int m_maxNumber;

    [SerializeField] private Dictionary<string,int> m_IDs = new Dictionary<string, int>();

    [SerializeField] private int lastGeneratedNum;

    //Example of RunTime ID SCL005

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public string GenerateID(string prefix, int lenght, int maxNum)
    {

        //Create the variable
        int number = 1;

        //adding the houses
        for (int i = 0; i < maxNum; i++)
        {
            //increase the houses based on the number of indexes that we need
            number = number * 10;
        }

        string numberString = number.ToString();

        return prefix + number;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
