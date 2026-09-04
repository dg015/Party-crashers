using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private float m_numberOfPointsX = 5;
    [SerializeField] private float m_numberOfPointsY = 5;

    [SerializeField] private float m_cellSize = 1;
    [SerializeField] private List<Vector2> m_cellList;


    private void CreatePoints()
    {
        //create X Cells
        for (int n1 = 0; n1 < m_numberOfPointsX; n1++)
        {
            //get the new location and assign it
            float currentXLocation = m_cellSize * n1;

            //m_cellList.Add(new Vector2(previousXLocation, 0));

            //now create the Y value
            for (int n2 = 0; n2 < m_numberOfPointsY; n2++)
            {
                float currentYLocation = m_cellSize * n2;

                m_cellList.Add(new Vector2(currentXLocation, currentYLocation));
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePoints();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int x = 0; x < m_numberOfPointsX; x++)
        {
            for (int y = 0; y < m_numberOfPointsY; y++)
            {
                //get cell point
                Vector3 point = transform.position + new Vector3(x * m_cellSize,0, y * m_cellSize);

                //draw the x line
                if (x < m_numberOfPointsX - 1)
                {
                    Vector3 nextXPoint = transform.position + new Vector3((x + 1) * m_cellSize,0, y * m_cellSize);

                    Gizmos.DrawLine(point, nextXPoint);
                }

                //draw the Y line
                if (y < m_numberOfPointsY - 1)
                {
                    Vector3 nextYPoint = transform.position + new Vector3(x * m_cellSize, 0, (y + 1) * m_cellSize);

                    Gizmos.DrawLine(point, nextYPoint);
                }
            }
        }
    }

}
