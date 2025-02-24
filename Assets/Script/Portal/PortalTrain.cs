using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrain : MonoBehaviour
{
    public GameObject train;
    public GameObject pointPosition;

    public PortalTrain(GameObject pointPosition)
    {
        this.pointPosition = pointPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            train.transform.position = pointPosition.transform.position;
        }
    }
}
