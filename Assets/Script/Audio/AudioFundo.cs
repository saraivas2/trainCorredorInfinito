using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFundo : MonoBehaviour
{

    GameObject Dust;
    GameObject audiofundoInstance;
    public GameObject audiofundo;
    public bool tiro;
    // Start is called before the first frame update
    void Start()
    {
        Dust = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        tiro = Dust.GetComponent<Player1>().HouveTiro();

        if (tiro)
        {
            if (audiofundoInstance == null) 
            {
                audiofundoInstance = Instantiate(audiofundo, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (audiofundoInstance != null)
            {
                Destroy(audiofundoInstance);
                audiofundoInstance = null; 
            }
        }

    }
}
