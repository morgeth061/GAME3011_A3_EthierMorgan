using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawn : MonoBehaviour
{
    public GameObject nodeRef;

    public GameObject spawnPoint;

    public GameObject gameManager;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameManager.GetComponent<A3GameManager>().spawnLock == false || gameManager.GetComponent<A3GameManager>().gameBegin == false)
        {
            Instantiate(nodeRef, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }
        
    }
}
