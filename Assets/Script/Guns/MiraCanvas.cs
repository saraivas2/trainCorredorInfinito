using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraCanvas : MonoBehaviour
{
    public Transform player;  
    public Camera mycam;     
    private RectTransform crosshair;

    void Start()
    {
        crosshair = GetComponent<RectTransform>();
    }

    void Update()
    {
        Ray ray = mycam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Vector3 screenPoint = mycam.WorldToScreenPoint(hit.point);
            crosshair.position = screenPoint;
        }
    }
}
