using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovTrain : MonoBehaviour
{
    public float vel;
  
    
    void Update()
    {
        transform.Translate(new Vector3(-1,0,0) * vel * Time.deltaTime);
        
    }
}
