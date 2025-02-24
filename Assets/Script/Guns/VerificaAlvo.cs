using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificaAlvo : MonoBehaviour
{
    public Camera camera;
    public LayerMask mask;
    public Image alvo;
    float sensibilidade = 1.2f;
    public Transform armaMira;

    private void Update()
    {
        VerificaAlvoEnemies();
   
    }

    public void VerificaAlvoEnemies()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,100,mask))
        {
            alvo.color = Color.red;
        }
        else
        {
            alvo.color = Color.white;
        }
    }

  
}
