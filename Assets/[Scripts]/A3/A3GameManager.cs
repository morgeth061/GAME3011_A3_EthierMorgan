using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class A3GameManager : MonoBehaviour
{
    //Column Spawn Objects
    public GameObject[] colSpawnObjects;

    private GameObject[] nodes;

    public GameObject nodeRef;

    public bool gameBegin = false;

    public bool swapLock = true;

    public bool spawnLock = false;

    public bool recheck = false;

    private bool fullyFilled = false;

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
        nodes = GameObject.FindGameObjectsWithTag("SwapNode");
        if (gameBegin == false && nodes.Length >= 100)
        {
            //print("Test");
            fullyFilled = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (GameObject.FindGameObjectsWithTag("A3Row")[i].transform.GetChild(j).gameObject
                        .GetComponent<NodePositionDetection>().filled == false ||
                        nodes[(i * 10) + j].GetComponent<Rigidbody2D>().velocity.y != 0)
                    {
                        if (GameObject.FindGameObjectsWithTag("A3Row")[i].transform.GetChild(j).gameObject
                            .GetComponent<NodePositionDetection>().filled == false)
                        {
                            //print(i + " , " + j);
                        }
                        
                        fullyFilled = false;
                        break;
                    }

                    if (!fullyFilled)
                    {
                        break;
                    }
                }
            }

            if (fullyFilled)
            {
                print("Grid Fully Filled");
                CheckForMatches();

                if (!recheck)
                {
                    print("Game Begin");
                    gameBegin = true;
                    swapLock = false;
                    spawnLock = true;
                }
            }
        }
        else if (gameBegin && nodes.Length >= 100 && swapLock)
        {
            fullyFilled = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (GameObject.FindGameObjectsWithTag("A3Row")[i].transform.GetChild(j).gameObject
                            .GetComponent<NodePositionDetection>().filled == false ||
                        nodes[(i * 10) + j].GetComponent<Rigidbody2D>().velocity.y != 0)
                    {
                        if (GameObject.FindGameObjectsWithTag("A3Row")[i].transform.GetChild(j).gameObject
                            .GetComponent<NodePositionDetection>().filled == false)
                        {
                            //print(i + " , " + j);
                        }

                        fullyFilled = false;
                        break;
                    }

                    if (!fullyFilled)
                    {
                        break;
                    }
                }
            }

            if (fullyFilled)
            {
                print("Grid Fully Filled");
                CheckForMatches();

                if (!recheck)
                {
                    swapLock = false;
                    spawnLock = true;
                }
            }
        }
        else
        {
            spawnLock = false;
        }
    }

    public void CheckForMatches()
    {
        recheck = false;
        swapLock = true;
        spawnLock = true;

        GameObject gridObj = GameObject.FindWithTag("Grid");
        print(("Checking for matches"));

        foreach (Transform rowTransform in gridObj.transform)
        {
            foreach (Transform nodeTransform in rowTransform)
            {

                GameObject currentNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().currentNode;

                if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode) //Check if right node exists
                {
                    GameObject rightNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode.GetComponent<NodePositionDetection>().currentNode;
                    if (!rightNode.GetComponent<A3NodeBehaviour>().isInMatch && currentNode.GetComponent<A3NodeBehaviour>().nodeColour == rightNode.GetComponent<A3NodeBehaviour>().nodeColour)
                    {
                        if (CheckForMatchingNeigbour(
                            nodeTransform.GetComponent<NodePositionDetection>().rightNode.transform, rightNode, true))
                        {
                            currentNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                            rightNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                        }
                    }
                }

                if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode)
                {
                    GameObject downNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode.GetComponent<NodePositionDetection>().currentNode;
                    if (!downNode.GetComponent<A3NodeBehaviour>().isInMatch && currentNode.GetComponent<A3NodeBehaviour>().nodeColour == downNode.GetComponent<A3NodeBehaviour>().nodeColour)
                    {
                        if (CheckForMatchingNeigbour(
                            nodeTransform.GetComponent<NodePositionDetection>().downNode.transform, downNode, false))
                        {
                            currentNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                            downNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                        }
                    }
                }
            }
        }


        nodes = GameObject.FindGameObjectsWithTag("SwapNode");

        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<A3NodeBehaviour>().isInMatch)
            {
                //print(("DESTROY"));
                gridObj.transform.GetChild(node.GetComponent<A3NodeBehaviour>().row).
                    transform.GetChild(node.GetComponent<A3NodeBehaviour>().col).GetComponent<NodePositionDetection>()
                    .currentNode = null;
                gridObj.transform.GetChild(node.GetComponent<A3NodeBehaviour>().row).transform
                    .GetChild(node.GetComponent<A3NodeBehaviour>().col).GetComponent<NodePositionDetection>()
                    .filled = false;
                Destroy(node);
                
                recheck = true;
            }
        }

        print("Destroyed");

        fullyFilled = false;
        spawnLock = false;
    }

    private bool CheckForMatchingNeigbour(Transform nodeTransform, GameObject node, bool right)
    {
        if (right)
        {
            if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode)
            {
                GameObject rightNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode.GetComponent<NodePositionDetection>().currentNode;
                if (rightNode.GetComponent<A3NodeBehaviour>().nodeColour == node.GetComponent<A3NodeBehaviour>().nodeColour)
                {
                    node.GetComponent<A3NodeBehaviour>().isInMatch = true;
                    rightNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                    return CheckForMatchingNeigbour(nodeTransform.GetComponent<NodePositionDetection>().rightNode.transform, rightNode, true);
                }
            }
        }
        else
        {
            if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode)
            {
                GameObject downNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode.GetComponent<NodePositionDetection>().currentNode;
                //print(downNode.GetComponent<A3NodeBehaviour>().row + " , " + downNode.GetComponent<A3NodeBehaviour>().col);
                if (downNode.GetComponent<A3NodeBehaviour>().nodeColour == node.GetComponent<A3NodeBehaviour>().nodeColour)
                {
                    node.GetComponent<A3NodeBehaviour>().isInMatch = true;
                    downNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                    return CheckForMatchingNeigbour(nodeTransform.GetComponent<NodePositionDetection>().downNode.transform, downNode, false);

                }
            }
        }

        if (node.GetComponent<A3NodeBehaviour>().isInMatch)
        {
            return true;
        }
        return false;

    }
}


/*
 * Check for Match -
 * update node positions
 * next tick, check for matches
 */
