using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a tool to generate runtimeIDs for scripts and objects;
/// </summary>
public class RunTimeIDGenerator 
{
    [SerializeField] private int m_IDLenght;
    [SerializeField] private int m_maxNumber;

    private Dictionary<string,int> m_IDs = new Dictionary<string, int>();

    //Example of RunTime ID SCL005

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int CalculateMaxNumber(int lenght)
    {
        /*
        //int number = 9;
        for (int i = 0; i < lenght; i++)
        {
            //increase the houses based on the number of indexes that we need
            number = number * 10;
            number = number + 9;
            
        }
        */
        int max = (int)Mathf.Pow(10, lenght) - 1;
        return max;
    }

    public string GenerateID(string prefix, int lenght)
    {
        int number = 0;
        //if it has the prefix extract the number for it
        if (m_IDs.ContainsKey(prefix))
        {
            number = m_IDs[prefix];

            if (number >= CalculateMaxNumber(lenght))
                number = 0;
            else
                number++; 
        }

        m_IDs[prefix] = number;
        string finalID = prefix + number.ToString().PadLeft(lenght, '0');
        return finalID;
    }

    /*
    public void RemoveIDWhere(string ID)
    {
        string prefix = Regex.Replace(ID, @"\d", "");
        int number = int.Parse(Regex.Replace(ID, "[^0-9]", ""));
        m_IDs.Remove(prefix,out number);
    }
    */  

}
