using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Referência para o transform do player
    public float moveSpeed = 3.5f; // Velocidade do movimento, pode ser ajustada no inspetor

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Define a velocidade do agente
        agent.speed = moveSpeed;

        // Se o player não for atribuído pelo inspetor, tenta encontrá-lo automaticamente usando uma tag "Player"
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Define o destino do agente como a posição atual do player
            agent.SetDestination(player.position);
        }
    }
}