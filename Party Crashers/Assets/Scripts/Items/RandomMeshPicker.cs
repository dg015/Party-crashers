using System.Collections.Generic;
using UnityEngine;

public class RandomMeshPicker : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_objectList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //disable the cylinder at parent object
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        PickRandomMesh();
    }

    private void PickRandomMesh()
    {
        int randomNum = Random.Range(0, m_objectList.Count);
        Instantiate(m_objectList[randomNum],transform);
    }

}
