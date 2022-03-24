using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapManager : MonoBehaviour
{
    public GameObject firstNode;
    public GameObject secondNode;

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
                  swapNode.GetComponent<A3NodeBehaviour>().col == firstNode.GetComponent<A3NodeBehaviour>().col - 1))) //Check for adjacent
            {
                print("SWAP");
                secondNode = swapNode;

                int firstColour = firstNode.GetComponent<A3NodeBehaviour>().nodeColour;
                int secondColour = secondNode.GetComponent<A3NodeBehaviour>().nodeColour;

                firstNode.GetComponent<A3NodeBehaviour>().nodeColour = secondColour;
                secondNode.GetComponent<A3NodeBehaviour>().nodeColour = firstColour;

                firstNode.GetComponent<A3NodeBehaviour>().UpdateColour();
                secondNode.GetComponent<A3NodeBehaviour>().UpdateColour();

                firstNode = null;
                secondNode = null;

                GameObject.FindWithTag("GameWindow").gameObject.GetComponent<A3GameManager>().CheckForMatches();

            }
            else
            {
                firstNode = swapNode;
            }
        }
    }
}
