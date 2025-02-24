using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passengers : MonoBehaviour
{
    [SerializeField] private GameObject[] passMale;
    [SerializeField] private GameObject[] passFemale;
    [SerializeField] private Transform[] pointsMale;
    [SerializeField] private Transform[] pointsFemale;

    // Start is called before the first frame update
    void Start()
    {
        //distributionFemale();
        //distributionMale();
    }

    private void distributionFemale()
    {
        for (int i = 0; i < 7; i++)
        {
            int valpoint = Random.Range(0, pointsMale.Length);
            int valpass = Random.Range(0,passMale.Length);
            Instantiate(passMale[valpass], pointsMale[valpoint].position, pointsMale[valpoint].rotation);
        }

    }

    private void distributionMale()
    {
        for (int i = 0; i < 7; i++)
        {
            int valpoint = Random.Range(0, pointsFemale.Length);
            int valpass = Random.Range(0, passFemale.Length);
            Instantiate(passFemale[valpass], pointsFemale[valpoint].position, pointsFemale[valpoint].rotation);
        }
    }
}
