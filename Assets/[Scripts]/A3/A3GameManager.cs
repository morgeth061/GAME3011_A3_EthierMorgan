using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3GameManager : MonoBehaviour
{
    //Column Spawn Objects
    public GameObject[] colSpawnObjects;

    public GameObject nodeRef;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < colSpawnObjects.Length; i++)
        {
            Instantiate(nodeRef, colSpawnObjects[i].transform.position, Quaternion.identity, colSpawnObjects[i].transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
