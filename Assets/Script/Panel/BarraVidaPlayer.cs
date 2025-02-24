using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class BarraVidaPlayer : MonoBehaviour
{
    public GameObject barra;
    public Canvas canvas;
    public GameObject Dust;
    Player1 ScriptDust;
    float barravidaFull;
    float vidafull;

    private void Start()
    {
        barravidaFull = barra.transform.localScale.x;

        ScriptDust = Dust.gameObject.GetComponent<Player1>();
        vidafull = ScriptDust.GetVida();
    }

    // Update is called once per frame
    void Update()
    {
        
        float barraVida = barra.transform.localScale.x;
        float vida = ScriptDust.GetVida();


        float barraAtual = (barravidaFull * vida) / vidafull;
        if (barraAtual < 0)
        {
            barraAtual = 0;
        }
        barra.transform.localScale = new Vector3(barraAtual, barra.transform.localScale.y, barra.transform.localScale.z);
    }
}
