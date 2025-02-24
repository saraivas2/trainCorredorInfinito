using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject pointPosition;

    public PortalPlayer(GameObject pointPosition)
    {
        this.pointPosition = pointPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = pointPosition.transform.position;
        }
    }
}
