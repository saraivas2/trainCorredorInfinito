using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float velocity;
    private bool travarMouse = true;
    private float mouseX = 0.0f, mouseY = 0.0f;
    float sensibilidade = 1.2f;
    int num = 1;
  
    private void Start()
    {
        if (travarMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            num *= -1;
        }

        if (num < 0)
        {
            mouseY += Input.GetAxis("Mouse X") * sensibilidade;
            mouseX += Input.GetAxis("Mouse Y") * sensibilidade;

            transform.eulerAngles = new Vector3(-mouseX, mouseY, 0);

            move();
        }
        
    }

    private void move()
    {
        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");
        transform.Translate(Time.deltaTime * velocity * new Vector3(horiz, 0, -vert));
    }

}
