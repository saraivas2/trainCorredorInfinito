using UnityEngine;

public class EnemyFollowNoNavMeshPistol : MonoBehaviour
{
    public Transform player; // Referência para o transform do player
    public float moveSpeed = 3.5f; // Velocidade do movimento
    public float rotationSpeed = 5f; // Velocidade de rotação

    // Referência para o script que controla o inimigo com pistola
    private enemiesPistol enemyPistol;

    void Start()
    {
        // Se o player não foi atribuído via inspetor, encontra pelo tag "Player"
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
        
        // Tenta obter o script enemiesPistol no mesmo GameObject
        enemyPistol = GetComponent<enemiesPistol>();
    }

    void Update()
    {
        // Se houver um script enemiesPistol e a vida dele estiver zerada, para de seguir
        if (enemyPistol != null && enemyPistol.GetVida() <= 0)
        {
            return;
        }
        
        if (player != null)
        {
            // Calcula a direção normalizada até o player
            Vector3 direction = (player.position - transform.position).normalized;
            
            // Rotaciona suavemente em direção ao player
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
            
            // Move o objeto na direção do player
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}