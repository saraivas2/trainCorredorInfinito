using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject player;
    public GameObject pointPosition;

    public Portal(GameObject pointPosition)
    {
        this.pointPosition = pointPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = pointPosition.transform.position;
        }

        if (other.CompareTag("Player1"))
        {
            player.transform.position = pointPosition.transform.position;
        }
    }
}
