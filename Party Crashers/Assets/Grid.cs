using UnityEngine;

public class Grid 
{
    private int m_width = 5;
    private int m_height = 5;

    private float m_cellSize = 1;
    private int[,] m_cells;
    private Vector3 m_originPosition;

    public Grid (int width, int height, float cellSize, Vector3 originPosition)
    {
        //set base data
        this.m_width = width;
        this.m_height = height;
        this.m_cellSize = cellSize;
        this.m_originPosition = originPosition;

        m_cells = new int[width, height];

        //create the grid
        //create X Cells
        for (int x = 0; x < m_width; x++)
        {
            //now create the Y value
            for (int y = 0; y < m_height; y++)
            {
                Debug.DrawLine(GetCellWorldPosition(x,y), GetCellWorldPosition(x,y + 1),Color.green, 10000f);
                Debug.DrawLine(GetCellWorldPosition(x, y), GetCellWorldPosition(x + 1, y ),Color.green, 10000f);
            }
        }
        //finish the top lines
        Debug.DrawLine(GetCellWorldPosition(0, height), GetCellWorldPosition(width, height), Color.green, 10000f);
        Debug.DrawLine(GetCellWorldPosition(width, 0), GetCellWorldPosition(width, height), Color.green, 10000f);
    }


    private Vector3 GetCellWorldPosition(int x,int y)
    {
        //divide by 2 so I get the center
        return new Vector3 (x, y) * m_cellSize + m_originPosition;
    }

    //get coordinates grid location from world coordinate
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - m_originPosition).x / m_cellSize);
        y = Mathf.FloorToInt((worldPosition - m_originPosition).y / m_cellSize);
    }

    public void SetCellValue(int x , int y, int value)
    {
        if( x < 0 || y <0 || x >= m_width || y >= m_height )
        {
            Debug.Log("Negative or invalid value, IGNORING NEW ASSIGNED VALUE");
            return;
        }

        m_cells[x, y] = value;    
    }
}
