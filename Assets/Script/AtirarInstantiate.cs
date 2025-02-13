using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarInstantiate : MonoBehaviour
{

    [SerializeField] private GameObject bala;
    [SerializeField] private Transform PontoFire;

    public void InstantiateBala()
    {
        Instantiate(bala, PontoFire.position, PontoFire.rotation);
    }
        
}
