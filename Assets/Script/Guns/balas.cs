using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class balas : MonoBehaviour
{
    public float vel;
    [SerializeField] private GameObject fire;
    float timeLife = 5;
    private Vector3 direcao;
    public int damage;

    void Start()
    {
        // Salva a direção inicial baseada na rotação do objeto ao ser instanciado
        direcao = transform.forward;
    }

    void Update()
    {
        // Move a bala na direção correta
        transform.position += direcao * vel * Time.deltaTime;

        // Destroi a bala após um tempo
        timeLife -= Time.deltaTime;
        if (timeLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int Damage()
    {
        return damage;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direcao = newDirection;
        transform.forward = newDirection;
    }
}
