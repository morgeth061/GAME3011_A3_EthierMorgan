using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePositionDetection : MonoBehaviour
{
    public int row;

    public int col;

    public GameObject currentNode;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<A3NodeBehaviour>().row = row;
        other.gameObject.GetComponent<A3NodeBehaviour>().col = col;
        currentNode = other.gameObject;
    }
}
