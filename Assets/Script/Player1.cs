using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player1 : MonoBehaviour
{
    public float velocity;
    [SerializeField] private GameObject idlePistol;
    [SerializeField] private GameObject idleRifle;
    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject Rifle;
    private bool travarMouse = true;
    private float mouseY = 0.0f;
    float sensibilidade = 1.2f;
    private Animator animator;
    private Rigidbody rb;
    private bool pistolBool, rifleBool,SemTiro;
    public float forca;
    private int vel = 1, val=1;
    public float tempo = 0;
    private Vector3 camVect;

    private void Start()
    {
        if (travarMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        SemTiro = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        comandosMove();

        //comandosAttacks();

        if (Input.GetMouseButton(1))
        {
            val *= -1;
        }

        if (val > 0)
        {
            pistolBool = true;
            rifleBool = false;
            idlePistol.SetActive(true);
            idleRifle.SetActive(false);
        }
        else
        {
            pistolBool = false;
            rifleBool = true;
            idlePistol.SetActive(false);
            idleRifle.SetActive(true);
        }

        tempo -= Time.deltaTime;
        if (tempo <= 0)
        {
            SemTiro = false;
        }
    }

    
    private void comandosMove()
    {
        mouseY += Input.GetAxis("Mouse X") * sensibilidade;
        //mouseX += Input.GetAxis("Mouse Y") * sensibilidade;

        //transform.eulerAngles = new Vector3(0, mouseY, 0);

        Quaternion quat = Quaternion.Euler(new Vector3(0, mouseY, 0));
        rb.MoveRotation(quat);

        camVect = GetDirectionVector(mouseY);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPlayerMoviment();
        }
        else if (Input.GetKey(KeyCode.R))
        {
            moveRun();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveWalk();
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movePlayerBack();
            //transform.Translate(Time.deltaTime * velocity * Vector3.back);

            rb.velocity = -camVect * Time.deltaTime * velocity;
        }
        else
        {
            if (pistolBool)
            {
                if (Input.GetMouseButton(0))
                {
                    ChamaTiroPistol();
                }
                AttackPistolFirePlayer();
            }
            else if (rifleBool)
            {
                if (Input.GetMouseButton(0))
                {
                    ChamaTiroRifle();
                }
                AttackRifleFirePlayer();
            }
            //transform.Translate(Vector3.zero);
            rb.velocity = Vector3.zero;
            
        }
    }

    private void ChamaTiroPistol()
    {
       
        
        if (!SemTiro)
        {
            AtirarInstantiate PistolInstantiateScript = Pistol.gameObject.GetComponentInChildren<AtirarInstantiate>();
            PistolInstantiateScript.InstantiateBala();
            tempo = 0.375f;
            SemTiro = true;
        }   
    }

    private void ChamaTiroRifle()
    {
        if (!SemTiro)
        {
            AtirarInstantiate RifleInstantiateScript = Rifle.gameObject.GetComponentInChildren<AtirarInstantiate>();
            {
                RifleInstantiateScript.InstantiateBala();
                tempo = 0.1f;
                SemTiro = true;
            }
        }
    }

    private void jumpPlayerMoviment()
    {
        if (pistolBool)
        {
            if (Input.GetMouseButton(0))
            {
                ChamaTiroPistol();
            }
            JumpPlayerPistol();
        }
        else if (rifleBool)
        {
            if (Input.GetMouseButton(0))
            {
                ChamaTiroRifle();
            }
                       
            JumpPlayerRifle();
        }
        
        rb.AddForce(Vector3.up * forca * Time.deltaTime, ForceMode.Impulse);
    }


    private void moveRun()
    {
        if (pistolBool)
        {
            if (Input.GetMouseButton(0))
            {
                ChamaTiroPistol();
            }
            
            RunPistoPlayer();
            vel = 4;
        }
        else if (rifleBool)
        {
            if (Input.GetMouseButton(0))
            {
                ChamaTiroRifle();
            }
            
            RunRiflePlayer();
            vel = 4;
        }
        //transform.Translate(Time.deltaTime * Vector3.forward * velocity * vel);
        rb.velocity = camVect * Time.deltaTime * velocity * vel;
    }


    private void moveWalk()
    {
        if (Input.GetMouseButton(0) & pistolBool)
        {
            AttackPistolFireWalkPlayer();
            ChamaTiroPistol();

        } else if (Input.GetMouseButton(0) & rifleBool)
        {
            AttackRifleFireWalkPlayer();
            ChamaTiroRifle();
        }
        else
        {
            movePlayer();
            vel = 1;          
        }
        //transform.Translate(Time.deltaTime * Vector3.forward * velocity * vel);
        rb.velocity = camVect * Time.deltaTime * velocity;
    }

    private void movePlayer()
    {

        if (pistolBool)
        {
            AttackPistolFireWalkPlayer();
        }
        else if (rifleBool)
        {
            AttackRifleFireWalkPlayer();
        }
    }

    private void movePlayerBack()
    {

        if (pistolBool)
        {
            AttackPistolFireWalkBack();
        }
        else if (rifleBool)
        {
            AttackRifleFireWalkBack();
        }
    }
    private void AttackPistolFireWalkBack()
    {
        animator.SetBool("rifle", false);
        animator.SetFloat("X", -1);
        animator.SetFloat("Y", 1);
    }

    private void AttackRifleFireWalkBack()
    {
        animator.SetBool("rifle", true);
        animator.SetFloat("Z", -1);
        animator.SetFloat("K", 0);
    }

    private void JumpPlayerPistol()
    {
        animator.SetBool("rifle", false);
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 1);
    }

    private void JumpPlayerRifle()
    {
        animator.SetBool("rifle", true);
        animator.SetFloat("Z", 1);
        animator.SetFloat("K", -1);
    }

    private void AttackGolpePlayer()
    {
        animator.SetBool("rifle", true);
        animator.SetFloat("Z",0);
        animator.SetFloat("K",1);

    }
    
    private void AttackPistolFirePlayer()
    {
        animator.SetBool("rifle", false);
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 0);
    }


    private void AttackPistolFireWalkPlayer()
    {
        animator.SetBool("rifle", false);
        animator.SetFloat("X", 1);
        animator.SetFloat("Y", 0);
    }

    private void AttackRifleFirePlayer()
    {
        animator.SetBool("rifle", true);
        animator.SetFloat("Z", 0);
        animator.SetFloat("K", 0);
    }

    private void AttackRifleFireWalkPlayer()
    {
        animator.SetBool("rifle", true);
        animator.SetFloat("Z", 1);
        animator.SetFloat("K", 1);
    }

    private void DeathPlayer()
    {
        animator.SetBool("death",true);
        /*

        bool resp = gameover.ShowTelaGameOver(true);
        
        Invoke("ReloadScene", 3f);*/

    }

    private void RunPistoPlayer()
    {
        animator.SetBool("rifle", false);
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
    }

    private void RunRiflePlayer()
    {
        animator.SetBool("rifle", true);
        animator.SetFloat("Z", 0);
        animator.SetFloat("K", 1);
    }
    private void ReloadScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void IdlePlayer()
    {
        if (pistolBool)
        {
            AttackPistolFirePlayer();
        }
        else if (rifleBool)
        {
            AttackRifleFirePlayer();
        }
    }

    private Vector3 GetDirectionVector(float angleY)
    {
        float radianY = angleY * Mathf.Deg2Rad;

        float x = Mathf.Sin(radianY);
        float z = Mathf.Cos(radianY);

        return new Vector3(x, 0, z);
    }


}
