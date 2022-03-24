using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapManager : MonoBehaviour
{
    public GameObject firstNode;
    public GameObject secondNode;
    public Vector3 tempNode;

    public void SwapBehaviour(GameObject swapNode)
    {
        if (!firstNode)
        {
            firstNode = swapNode;
        }
        else
        {
            if ((swapNode.GetComponent<A3NodeBehaviour>().col == firstNode.GetComponent<A3NodeBehaviour>().col &&
                 (swapNode.GetComponent<A3NodeBehaviour>().row == firstNode.GetComponent<A3NodeBehaviour>().row + 1 ||
                  swapNode.GetComponent<A3NodeBehaviour>().row == firstNode.GetComponent<A3NodeBehaviour>().row - 1)) ||
                (swapNode.GetComponent<A3NodeBehaviour>().row == firstNode.GetComponent<A3NodeBehaviour>().row &&
                 (swapNode.GetComponent<A3NodeBehaviour>().col == firstNode.GetComponent<A3NodeBehaviour>().col + 1 ||
                  swapNode.GetComponent<A3NodeBehaviour>().col == firstNode.GetComponent<A3NodeBehaviour>().col - 1)))
            {
                print("SWAP");
                secondNode = swapNode;
                tempNode = firstNode.transform.position;
                firstNode.transform.position = secondNode.transform.position;
                secondNode.transform.position = tempNode;

                firstNode = null;
                secondNode = null;
                tempNode = Vector3.zero;

                GameObject.FindWithTag("GameWindow").gameObject.GetComponent<A3GameManager>().CheckForMatches();

            }
            else
            {
                firstNode = swapNode;
            }
        }
    }
}
