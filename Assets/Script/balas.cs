using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class balas : MonoBehaviour
{
    public float vel;
    [SerializeField] private GameObject fire;
    float timeLife = 5;

    void Update()
    {
        transform.Translate(Vector3.forward * vel * Time.deltaTime);

        timeLife -= Time.deltaTime;

        if (timeLife <= 0)
        {
            Destroy(this.gameObject);
            timeLife = 5.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            enemies scriptenemies =  gameObject.GetComponent<enemies>();
            //scriptenemies.VidaSpider(20);
        } 
    }

}
