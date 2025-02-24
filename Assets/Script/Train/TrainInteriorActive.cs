using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainInteriorActive : MonoBehaviour
{
    public GameObject train;
    public GameObject player;
    public Transform point;

    private void Start()
    {
        train.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            train.SetActive(true);
            player.transform.position= point.position;
        }

    }
}
