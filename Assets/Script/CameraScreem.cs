using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreem : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    private Vector3 difference;
    

    void Start()
    {
        Vector3 posPlayer = player.position;
        Vector3 posCamera = transform.position;
        difference = posCamera - posPlayer;

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + difference;
    }
}
