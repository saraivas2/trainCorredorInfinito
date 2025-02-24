using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarInstantiate : MonoBehaviour
{

    public GameObject bala;
    public Transform PontoFire;
    public Camera camera;
    public Transform player;

    public void InstantiateBala()
    {
        // Calcula a direção normalizada da bala
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

        // Calcula a direção normalizada da bala
        Vector3 direction = (targetPoint - PontoFire.position).normalized;

        GameObject balas = Instantiate(bala, PontoFire.position, Quaternion.identity);
        balas.GetComponent<balas>().SetDirection(direction);
    }
    
        
}
