using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspense : MonoBehaviour
{
    float rangeTemp;

    private void Start()
    {
        rangeTemp = Random.Range(10, 40);
    }
    // Update is called once per frame
    void Update()
    {
        rangeTemp -= Time.deltaTime;
        if (rangeTemp < 0)
        {
            SoundEffectTerror();
            rangeTemp = Random.Range(10, 40);
        }
    }

    public void SoundEffectTerror()
    {
        ExecutaAudio scritpDante = GameObject.Find("Surface").GetComponent<ExecutaAudio>();
        scritpDante.InstantiateAudio();
    }
}
