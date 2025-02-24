using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemiesRifle : MonoBehaviour
{

    bool tiro = false;
    private NavMeshAgent enemy;
    private GameObject player;
    public float distance;
    private Animator animator;
    public float tempo = 0;
    public float vida = 100;
    public bool attackTemp = true;
    private Player1 scriptPlayer;
    private float timerAttack = 1.5f;
    private bool rifleBool, SemTiro;
    public GameObject Rifle;
    public Transform posAttack;
    bool death = false;
    public bool hunting = false;
    float timer = 15;
    public GameObject enemyGameobject;
    public Transform pointPosition;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Pistol Idle");
        animator = GetComponent<Animator>();
        if (player != null)
        {
            scriptPlayer = player.GetComponent<Player1>();
        }
        else
        {
            Debug.LogError("Player não foi encontrado! Verifique o nome do objeto.");
            return;
        }


        if (NavMesh.SamplePosition(pointPosition.position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            enemy.Warp(hit.position);
        }
        else
        {
            Debug.LogWarning("NPC foi instanciado fora do NavMesh. Verifique a posição inicial!");
        }
    }

    void Update()
    {

        if (!death)
        {
            Hunting();
            AttackDust(); ;
            if (!hunting) idleEnemy();
            if (vida <= 0) deathEnemy();
        }
        else
        {
            TimerDestroy();
        }
    }


    public float GetVida()
    {
        return vida;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("balaPistol"))
        {
            VidaEnemy(other.GetComponent<balas>().Damage());
            
        }

        if (other.gameObject.CompareTag("balaRifle"))
        {
            VidaEnemy(other.GetComponent<balas>().Damage());
            
        }
    }
    
    private void Hunting()
    {
        if (player == null || enemy == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        hunting = dist < distance;

        if (hunting)
        {
            if (enemy.isOnNavMesh)
            {

                if (NavMesh.SamplePosition(player.transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                {
                    enemy.SetDestination(hit.position);
                }
                else
                {
                    Debug.LogWarning("Destino do NPC fora do NavMesh.");
                }
            }
            else
            {
                Debug.LogWarning("NPC não está no NavMesh.");
            }
        }
    }

    public bool GetHunting()
    {
        return hunting;
    }

    private void TimerDestroy()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(enemyGameobject);
        }
    }
    
    private void AttackDust()
    {
        if (!hunting) return;

        animator.SetBool("attack", true);

        Invoke(nameof(DamageDust), 0.5f);
    }

    private void DamageDust()
    {
        if (!attackTemp) return;

        ChamaTiroRifle();
        attackTemp = false;
        
        Invoke(nameof(ResetarAtaque), timerAttack);
        
    }

    private void ChamaTiroRifle()
    {


        if (Rifle == null)
        {
            Debug.LogWarning("Rifle não foi atribuído");
            return;
        }

        AtirarInstantiate RifleInstantiateScript = Rifle.gameObject.GetComponent<AtirarInstantiate>();
        if (RifleInstantiateScript != null)
        {
            RifleInstantiateScript.InstantiateBala();
            tempo = 0.1f;
            AudioRifleEnemyPlay();
            tiro = true;
        }
    }

    private void ResetarAtaque()
    {
        attackTemp = true;
    }

    private void movimentEnemy()
    {
        enemy.SetDestination(player.transform.position);
    }

    public void VidaEnemy(int damage)
    {
        vida -= damage;
    }

    private void deathEnemy()
    {
        if (death) return;

        animator.SetBool("attack", false);
        animator.SetBool("die", true);
        if (enemy != null) enemy.enabled = false;
        death = true;
    }
        
    private void idleEnemy()
    {
        animator.SetBool("attack", false);
    }

    public bool TiroEnemy()
    {
        return tiro;
    }

    public void AudioRifleEnemyPlay()
    {
        ExecutaAudio scritp = GameObject.Find("Rifle").GetComponent<ExecutaAudio>();
        scritp.InstantiateAudio();
    }
}
