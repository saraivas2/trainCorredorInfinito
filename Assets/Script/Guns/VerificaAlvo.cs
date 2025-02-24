using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificaAlvo : MonoBehaviour
{
    public Camera camera;
    public LayerMask mask;
    public Image alvo;
    float mouseY = 0f, mouseX = 0f;
    float sensibilidade = 1.2f;

    private void Update()
    {
        VerificaAlvoEnemies();


        mouseY += Input.GetAxisRaw("Mouse X") * sensibilidade;
        mouseX += Input.GetAxisRaw("Mouse Y") * sensibilidade;

        mouseX = Mathf.Clamp(mouseX, -90f, 90f);

        camera.transform.eulerAngles = new Vector3(-mouseX, mouseY, 0);

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
