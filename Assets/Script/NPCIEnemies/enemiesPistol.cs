using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemiesPistol : MonoBehaviour
{
    bool tiro = false;
    private NavMeshAgent enemy;
    private GameObject player;
    public float distance;
    private Animator animator;
    public float tempo = 0;
    public float vida = 100;
    public bool attackBool = false, attackTemp = true;
    private Player1 scriptPlayer;
    private float timerAttack = 1.5f;
    private bool pistolBool, SemTiro;
    public GameObject Pistol;
    public Transform posAttack;
    bool death = false;
    public bool hunting = false;
    float timer = 15;
    public GameObject enemyGameobject;
    public Transform pointPosition;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
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

        enemy.enabled = false;
        Invoke(nameof(EnableAgent), 0.1f);  // Ativa após um pequeno delay
    }

    void EnableAgent()
    {
        if (pointPosition.position != null)
        {
            enemy.Warp(pointPosition.position);
        }
        else
        {
            Debug.LogWarning("NPC não possui posição inicial!");
        }
    }

    void Update()
    {
        if (!death)
        {
            Hunting();
            AttackDust();
            if (!hunting) idleEnemy();
            if (vida <= 0) deathEnemy();
        }
        else
        {
            TimerDestroy();
            enemy.enabled = false;
        }
    }

    public float GetVida()
    {
        return vida;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("balaPistol") || other.gameObject.CompareTag("balaRifle"))
        {
            balas balaScript = other.GetComponent<balas>();
            if (balaScript != null)
            {
                VidaEnemy(balaScript.Damage());
            }
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
        Invoke(nameof(DamageDust), 1f);
    }

    private void DamageDust()
    {
        if (!attackTemp) return;
        ChamaTiroPistol();
        attackTemp = false;
        Invoke(nameof(ResetarAtaque), timerAttack);
    }

    private void ChamaTiroPistol()
    {
        if (Pistol == null)
        {
            Debug.LogWarning("Pistol não foi atribuído");
            return;
        }

        AtirarInstantiate pistolScript = Pistol.GetComponent<AtirarInstantiate>();
        if (pistolScript != null)
        {
            pistolScript.InstantiateBala();
            tiro = true;
            tempo = 0.375f;
            AudioPistolEnemyPlay();
        }
    }

    private void ResetarAtaque()
    {
        attackTemp = true;
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

    public void AudioPistolEnemyPlay()
    {
        AudioController script = GameObject.Find("Pistol").GetComponent<AudioController>();
        if (script != null)
        {
            script.AudioArmasPlay();
        }
    }
}
