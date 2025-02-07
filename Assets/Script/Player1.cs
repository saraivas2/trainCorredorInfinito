using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float velocity;
    private bool travarMouse = true;
    private float mouseX = 0.0f, mouseY = 0.0f;
    float sensibilidade = 1.2f;
    int num = 1;
    public Vector3 vel;
    private Animator animator;
    private Rigidbody rb;
    private int walk = Animator.StringToHash("walk");
    private int jump = Animator.StringToHash("jump");
    private int idle = Animator.StringToHash("idle");
    private int run = Animator.StringToHash("run");
    private int Rifle = Animator.StringToHash("rifleFire");
    private int Pistol = Animator.StringToHash("pistolFire");
    private int walkRifle = Animator.StringToHash("rifleFireWalk");
    private int walkPistol = Animator.StringToHash("pistolFireWalk");
    private int chute1 = Animator.StringToHash("chute1");
    private int chute2 = Animator.StringToHash("chute2");
    private int giro1 = Animator.StringToHash("giro1");
    private int giro2 = Animator.StringToHash("giro2");
    private int soco1 = Animator.StringToHash("soco1");
    private int soco2 = Animator.StringToHash("soco2");
    private int death = Animator.StringToHash("death");
    bool moviment=false;
    bool pistolBool,rifleBool=false;

    private void Start()
    {
        if (travarMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        comandosMove();

        comandosAttacks();
    }

    private void comandosAttacks()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //
        {
            AttackSoco1Player();
        }
        else if (Input.GetKeyDown(KeyCode.E)) //
        {
            AttackSoco2Player();
        }
        else if (Input.GetKeyDown(KeyCode.R)) //
        {
            AttackGiro1Player();
        }
        else if (Input.GetKeyDown(KeyCode.F)) //
        {
            AttackGiro2Player();
        }
        else if (Input.GetKeyDown(KeyCode.X)) //
        {
            AttackChute1Player();
        }
        else if (Input.GetKeyDown(KeyCode.C)) //
        {
            AttackChute2Player();
        }
    }
    private void comandosMove()
    {
        mouseY += Input.GetAxis("Mouse X") * sensibilidade;
        mouseX += Input.GetAxis("Mouse Y") * sensibilidade;

        transform.eulerAngles = new Vector3(-mouseX, mouseY, 0);


        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");

        if (vert != 0 | horiz != 0)
        {
            move(horiz, vert);
        }
        else
        {
            if (Input.GetMouseButton(1) & pistolBool)
            {
                AttackPistoFirePlayer();
            }
            else if (Input.GetMouseButton(1) & rifleBool)
            {
                AttackRifleFirePlayer();
            }
            else
            {
                IdlePlayer();
            }
        }
    }
    private void move(float horiz, float vert)
    {
        if (Input.GetMouseButton(1) & pistolBool)
        {
            AttackPistoFireWalkPlayer();
        } else if (Input.GetMouseButton(1) & rifleBool)
        {
            AttackRifleFireWalkPlayer();
        }
        else
        {
            movePlayer();
        }
        transform.Translate(Time.deltaTime * new Vector3(horiz, 0, vert) * velocity);
    }

    private void movePlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, true);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);
}

    private void JumpPlayer()
    {
        animator.SetBool(jump, true);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);

    }


    private void AttackChute1Player()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, true);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);

    }

    private void AttackSoco2Player()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, true);
        animator.SetBool(death, false);

    }
    private void AttackChute2Player()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, true);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);

    }
    private void AttackSoco1Player()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, true);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);

    }
    private void AttackGiro1Player()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, true);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);

    }
    private void AttackGiro2Player()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, true);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);

    }

    private void AttackPistoFirePlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, true);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);

    }


    private void AttackPistoFireWalkPlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, true);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);
    }

    private void AttackRifleFirePlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, true);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);
    }

    private void AttackRifleFireWalkPlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, true);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);
    }

    private void DeathPlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, true);
        /*

        bool resp = gameover.ShowTelaGameOver(true);
        
        Invoke("ReloadScene", 3f);*/

    }

    private void ReloadScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void IdlePlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, true);
        animator.SetBool(walk, false);
        animator.SetBool(run, false);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);
    }
    private void RunningPlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);
        animator.SetBool(run, true);
        animator.SetBool(Rifle, false);
        animator.SetBool(Pistol, false);
        animator.SetBool(walkRifle, false);
        animator.SetBool(walkPistol, false);
        animator.SetBool(chute1, false);
        animator.SetBool(chute2, false);
        animator.SetBool(giro1, false);
        animator.SetBool(giro2, false);
        animator.SetBool(soco1, false);
        animator.SetBool(soco2, false);
        animator.SetBool(death, false);
        transform.Translate(vel * velocity * 2);
    }


}
