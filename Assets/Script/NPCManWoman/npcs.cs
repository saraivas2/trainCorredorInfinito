using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcs : MonoBehaviour
{
    public GameObject npc;
    private Animator animator;
    GameObject Dust;
    GameObject[] EnemiesRifle;
    GameObject[] EnemiesPistol;
    bool tiroEnemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Dust = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        bool tiro = Dust.GetComponent<Player1>().HouveTiro();

        if (tiro | tiroEnemy) 
        {
            OcorreTiro();
      
        }
        else
        {
            SemOcorrenciaTiro();
        }
    }

    void TiroEnemiesPistol()
    {
        EnemiesPistol = GameObject.FindGameObjectsWithTag("enemyPistol");
        tiroEnemy = false;

        foreach (GameObject enemy in EnemiesPistol)
        {
            enemiesPistol enemyScript = enemy.GetComponent<enemiesPistol>();

            if (enemyScript != null)
            {
                tiroEnemy = enemyScript.TiroEnemy();

                if (tiroEnemy)
                {
                    break;
                }
            }
        }
    }


    void TiroEnemiesRifle()
    {
        EnemiesRifle = GameObject.FindGameObjectsWithTag("enemyRifle");
        tiroEnemy = false;

        foreach (GameObject enemy in EnemiesRifle)
        {
            enemiesRifle enemyScript = enemy.GetComponent<enemiesRifle>();

            if (enemyScript != null)
            {
                tiroEnemy = enemyScript.TiroEnemy();

                if (tiroEnemy)
                {
                    break;
                }
            }
        }
    }
    void OcorreTiro()
    {
        animator.SetBool("tiro", true);
    }

    void SemOcorrenciaTiro()
    {
        animator.SetBool("tiro", false);
    }
}
