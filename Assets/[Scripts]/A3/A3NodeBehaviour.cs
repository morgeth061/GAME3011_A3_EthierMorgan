using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A3NodeBehaviour : MonoBehaviour
{
    public int row;
    public int col;

    public int horizMatchNum = 1;
    public int vertMatchNum = 1;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;

    public int nodeColour;

    public bool isInMatch = false;

    public GameObject gridRef;

    // Start is called before the first frame update
    void Start()
    {
        gridRef = GameObject.FindWithTag("Grid");
        int rand = Random.Range(0, 6);
        nodeColour = rand;

        if (rand == 0)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite1;
        }
        else if(rand == 1)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite2;
        }
        else if (rand == 2)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite3;
        }
        else if (rand == 3)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite4;
        }
        else if (rand == 4)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite5;
        }
        else if (rand == 5)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite6;
        }
    }

    public void OnNodeClick()
    {
        gridRef.GetComponent<SwapManager>().SwapBehaviour(this.gameObject);
    }

    public void UpdateColour()
    {
        if (nodeColour == 0)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite1;
        }
        else if (nodeColour == 1)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite2;
        }
        else if (nodeColour == 2)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite3;
        }
        else if (nodeColour == 3)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite4;
        }
        else if (nodeColour == 4)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite5;
        }
        else if (nodeColour == 5)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite6;
        }
    }
}
