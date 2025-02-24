using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainInteriorInactive : MonoBehaviour
{

    public GameObject train;
    public GameObject player;
    public Transform point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            train.SetActive(false);
            player.transform.position = point.position;
        }

    }
}
