using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawn : MonoBehaviour
{
    public GameObject nodeRef;

    public GameObject spawnPoint;

    private void OnTriggerExit2D(Collider2D other)
    {
        Instantiate(nodeRef, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
    }
}
