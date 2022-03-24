using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3GameButtonBehaviour : MonoBehaviour
{
    private bool gameVisible = false;

    private Transform parentObj;

    private GameObject gameWindow;


    //**************************
    //Toggle Game Window On And Off
    //**************************
    void Awake()
    {
        parentObj = transform.parent; //GameCanvas
        gameWindow = parentObj.transform.Find("MatchGame").gameObject; //DiggingGame
    }

    public void OnMatchButtonClick()
    {
        gameVisible = !gameVisible;
        gameWindow.SetActive(gameVisible);
        if (gameVisible)
        {
            //gameWindow.transform.Find("Lock").gameObject.GetComponent<LockBehaviour>().Setup(lockDifficulty);
        }
    }
}
