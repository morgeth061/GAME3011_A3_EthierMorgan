using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3GameManager : MonoBehaviour
{
    //Column Spawn Objects
    public GameObject[] colSpawnObjects;

    private GameObject[] nodes;

    public GameObject nodeRef;

    private bool gameBegin = false;

    public bool gridLock = false;

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
            bool fullyFilled = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (GameObject.FindGameObjectsWithTag("A3Row")[i].transform.GetChild(j).gameObject
                        .GetComponent<NodePositionDetection>().filled == false ||
                        nodes[(i * 10) + j].GetComponent<Rigidbody2D>().velocity.y != 0)
                    {
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
                print("HELP");
                gameBegin = true;
                gridLock = true;
                //ChangeLock(RigidbodyType2D.Static);
            }
        }
        else
        {
            if (gridLock == true && nodes[0].GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                //ChangeLock(RigidbodyType2D.Static);
            }

            if (gridLock == false && nodes[0].GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
            {
                //ChangeLock(RigidbodyType2D.Dynamic);
            }
        }
    }

    private void ChangeLock(UnityEngine.RigidbodyType2D type)
    {
        foreach (GameObject node in nodes)
        {
            node.GetComponent<Rigidbody2D>().bodyType = type;
        }
    }

    public void CheckForMatches()
    {
        ChangeLock(RigidbodyType2D.Dynamic);
        GameObject gridObj = GameObject.FindWithTag("Grid");

        foreach (Transform rowTransform in gridObj.transform)
        {
            foreach (Transform nodeTransform in rowTransform)
            {
                GameObject currentNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().currentNode;
                
                if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode)
                {
                    GameObject rightNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode.GetComponent<NodePositionDetection>().currentNode;
                    if (rightNode.GetComponent<A3NodeBehaviour>().nodeColour == currentNode.GetComponent<A3NodeBehaviour>().nodeColour)
                    {
                        currentNode.GetComponent<A3NodeBehaviour>().horizMatchNum = 1 + currentNode.GetComponent<A3NodeBehaviour>().horizMatchNum;
                        rightNode.GetComponent<A3NodeBehaviour>().horizMatchNum = currentNode.GetComponent<A3NodeBehaviour>().horizMatchNum;

                    }
                }

                if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode)
                {
                    GameObject downNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode.GetComponent<NodePositionDetection>().currentNode;
                    if (downNode.GetComponent<A3NodeBehaviour>().nodeColour == currentNode.GetComponent<A3NodeBehaviour>().nodeColour)
                    {
                        currentNode.GetComponent<A3NodeBehaviour>().vertMatchNum = 1 + currentNode.GetComponent<A3NodeBehaviour>().vertMatchNum;
                        downNode.GetComponent<A3NodeBehaviour>().vertMatchNum = currentNode.GetComponent<A3NodeBehaviour>().vertMatchNum;

                    }
                }
            }
        }


        nodes = GameObject.FindGameObjectsWithTag("SwapNode");

        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<A3NodeBehaviour>().horizMatchNum >= 3 ||
                node.GetComponent<A3NodeBehaviour>().vertMatchNum >= 3)
            {
                print(("DESTROY"));
                Destroy(node);
            }
            else
            {
                node.GetComponent<A3NodeBehaviour>().horizMatchNum = 1;
                node.GetComponent<A3NodeBehaviour>().vertMatchNum = 1;
            }
        }

        gridLock = false;
    }
}
