using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class cameras : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    int num = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            num *= -1;
        }

        if (num > 0)
        {
            cam1.enabled = true;
            cam2.enabled = false;
            
        }
        else
        {
            cam1.enabled = false;
            cam2.enabled = true;
           
        }
    }

}
