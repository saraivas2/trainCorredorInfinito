using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarInstantiate : MonoBehaviour
{

    public GameObject bala;
    public Transform PontoFire;
    public Camera camera;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void InstantiateBala()
    {
        if (player == null)
        {
            Debug.LogError("O objeto 'player' está nulo!");
            return;
        }

        if (PontoFire == null)
        {
            Debug.LogError("O objeto 'PontoFire' está nulo!");
            return;
        }

        Vector3 direction = (player.position - PontoFire.position).normalized;

        GameObject balas = Instantiate(bala, PontoFire.position, Quaternion.identity);
        balas.GetComponent<balas>().SetDirection(direction);
    }

    public void InstantiateBalaDust()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        Vector3 direction = (targetPoint - PontoFire.position).normalized;

        GameObject balas = Instantiate(bala, PontoFire.position, Quaternion.identity);
        balas.GetComponent<balas>().SetDirection(direction);
    }
    
        
}

