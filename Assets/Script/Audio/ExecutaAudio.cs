using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutaAudio : MonoBehaviour
{
    public GameObject[] prefabGO; 
    private GameObject currentInstance;
    private float timerCount = 3f;

    public void InstantiateAudio()
    {
        int i = Random.Range(0, prefabGO.Length);
        currentInstance = Instantiate(prefabGO[i], transform.position, Quaternion.identity);

        Invoke(nameof(DestroyCurrentInstance), timerCount);
    }

    private void DestroyCurrentInstance()
    {
        if (currentInstance != null)
        {
            Destroy(currentInstance);
        }
    }
}
