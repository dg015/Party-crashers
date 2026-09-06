using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    private List<Grid> m_gridList;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddGridTolist(Grid grid)
    {
        m_gridList.Add(grid);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //testing
        Grid grid = new Grid(5, 5,5f,transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
