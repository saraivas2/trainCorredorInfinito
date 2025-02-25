using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Player1 : MonoBehaviour
{
    public float velocity;
    [SerializeField] private GameObject idlePistol;
    [SerializeField] private GameObject idleRifle;
    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject Rifle;
    private bool travarMouse = true;
    public float mouseX = 0.0f, mouseY = 0.0f;
    float sensibilidade = 1.2f;
    private Animator animator;
    private Rigidbody rb;
    private bool pistolBool, rifleBool,SemTiro;
    bool death = false, tiro=false;
    public float forca;
    private int vel = 1, val=1;
    private Vector3 camVect;
    public float tempo = 0;
    float tempotiro = 30;
    private float vida = 100;
    bool pausar = false;
    public Camera mycam;
    float TimerAction = 5f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SemTiro = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (PausarMenuDojogo.pausa) return;

        mycam.transform.rotation = transform.rotation;
        
        if (!death)
        {
            comandosMove();

            if (Input.GetMouseButton(1))
            {
                val *= -1;
                AudioTrocaArma();
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

            TempoTiro();

            tempo -= Time.deltaTime;
            if (tempo <= 0)
            {
                SemTiro = false;
            }
        }
        else
        {
            GameOverOn();
        }

        if (vida < 100)
        {
            if (RecupVidaERifle() & RecupVidaEPistol())
            {
                if (TimerCount())
                {
                    RecuperaVida();
                }
            }
        }

    }

    void RecuperaVida()
    {
        if (vida < 100)
        {
            vida += Time.deltaTime;
            if (vida > 100)
            {
                vida = 100;
                TimerAction = 5f;
            }
        }
        vida += Time.deltaTime * 2;
    }

    bool TimerCount()
    {
        TimerAction -= Time.deltaTime;

        if (TimerAction <= 0)
        {

            return true;
        }
        return false;
    }

    bool RecupVidaERifle()
    {
        GameObject[] enemyrifle = GameObject.FindGameObjectsWithTag("enemyRifle");

        bool allEnemyStoppedHunting = true;

        foreach (GameObject enemyr in enemyrifle)
        {
            enemiesRifle enemyScript = enemyr.GetComponent<enemiesRifle>();

            if (enemyScript != null)
            {
                bool isHunting = enemyScript.GetHunting();

                if (isHunting)
                {
                    allEnemyStoppedHunting = false;
                    return allEnemyStoppedHunting;
                }
            }

        }
        return allEnemyStoppedHunting;
    }

    bool RecupVidaEPistol()
    {
        GameObject[] enemypistol = GameObject.FindGameObjectsWithTag("enemyPistol");

        bool allEnemyStoppedHunting = true;

        foreach (GameObject enemyp in enemypistol)
        {
            enemiesPistol enemyScript = enemyp.GetComponent<enemiesPistol>();

            if (enemyScript != null)
            {
                bool isHunting = enemyScript.GetHunting();

                if (isHunting)
                {
                    allEnemyStoppedHunting = false;
                    return allEnemyStoppedHunting;
                }
            }
        }
        return allEnemyStoppedHunting;
    }


    void TempoTiro()
    {
        if (tiro)
        {
            tempotiro -= Time.deltaTime;
        }

        if (tempotiro <= 0)
        {
            tiro = false;
            tempotiro = 30;
        }
    }

    public void VidaDamage(float damage)
    {
        vida -= damage;
        if (vida <= 0 && !death)
        {
            death = true;
            DeathPlayer();
        }
    }

    public float GetVida()
    {
        return vida;
    }

    public bool HouveTiro()
    {
        return tiro;
    }
    
    private void comandosMove()
    {
        mouseY += Input.GetAxisRaw("Mouse X") * sensibilidade;

        transform.eulerAngles = new Vector3(0, mouseY, 0);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPlayerMoviment();
        }
        else if (Input.GetKey(KeyCode.R))
        {
            moveRun();
            AudioRunPlay();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveWalk();
            AudioWalkPlay();


        }
        else if (Input.GetKey(KeyCode.S))
        {
            movePlayerBack();
            transform.Translate(Time.deltaTime * velocity * Vector3.back);
        }else
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
                    AttackFireStopPlayer();
                    ChamaTiroRifle();
                }
                else
                {
                    AttackRifleFirePlayer();
                }
                
            }

            AudioWalkStop();
            AudioRunStop();
            
            transform.Translate(Vector3.zero);
        }
    }

    private void ChamaTiroPistol()
    {
       
        
        if (!SemTiro)
        {
            AtirarInstantiate PistolInstantiateScript = Pistol.gameObject.GetComponentInChildren<AtirarInstantiate>();
            PistolInstantiateScript.InstantiateBalaDust();
            tempo = 0.375f;
            SemTiro = true;
            tiro = true;
            tempotiro = 30;
            AudioPistolPlay();
        }   

    }

    private void ChamaTiroRifle()
    {
        if (!SemTiro)
        {
            AtirarInstantiate RifleInstantiateScript = Rifle.gameObject.GetComponentInChildren<AtirarInstantiate>();
            {
                RifleInstantiateScript.InstantiateBalaDust();
                tempo = 0.1f;
                AudioRiflePlay(); 
                SemTiro = true;
                tiro = true;
                tempotiro = 30;
            }
        }
    }

    public void AudioWalkPlay()
    {
        /*AudioController scritp = GameObject.Find("Walk").GetComponent<AudioController>();
        scritp.AudioArmasPlay();*/
    }

    public void AudioWalkStop()
    {
        AudioController scritp = GameObject.Find("Walk").GetComponent<AudioController>();
        scritp.AudioArmasStop();
    }

    public void AudioRunPlay()
    {
        /*AudioController scritp = GameObject.Find("Run").GetComponent<AudioController>();
        scritp.AudioArmasPlay();*/
    }

    public void AudioRunStop()
    {
        /*AudioController scritp = GameObject.Find("Run").GetComponent<AudioController>();
        scritp.AudioArmasStop();*/
    }

    public void AudioPistolPlay()
    {
        AudioController scritp = GameObject.Find("Pistol").GetComponent<AudioController>();
        scritp.AudioArmasPlay();
    }

    public void AudioDeathPlay()
    {
        AudioController scritp = GameObject.Find("Death").GetComponent<AudioController>();
        scritp.AudioArmasPlay();
    }

    public void AudioTrocaArma()
    {
        /*AudioController scritp = GameObject.Find("TrocaArma").GetComponent<AudioController>();
        scritp.AudioArmasStop();*/
    }

    public void AudioRiflePlay()
    {
        ExecutaAudio scritp = GameObject.Find("Rifle").GetComponent<ExecutaAudio>();
        scritp.InstantiateAudio();
    }

    public void SoundEffectDamage()
    {
        /*ExecutaAudio scritp = GameObject.Find("Damage").GetComponent<ExecutaAudio>();
        scritp.InstantiateAudio();*/
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("balaRifle"))
        {
            VidaDamage(other.GetComponent<balas>().Damage());

        }

        if (other.gameObject.CompareTag("balaPistol"))
        {
            VidaDamage(other.GetComponent<balas>().Damage());

        }
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
        transform.Translate(Time.deltaTime * Vector3.forward * velocity * vel);
        
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
        transform.Translate(Time.deltaTime * Vector3.forward * velocity * vel);
        
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

    private void AttackFireStopPlayer()
    {
        animator.SetBool("rifle", true);
        animator.SetFloat("Z", -1);
        animator.SetFloat("K", -1);

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

    private void GameOverOn()
    {
        GameOverScript gameover = GameObject.Find("GameOver").GetComponent<GameOverScript>();
        gameover.ShowTelaGameOver(true);

        Invoke("ReloadScene", 5f);
    }
    private void DeathPlayer()
    {
        animator.SetBool("death", true);
        AudioDeathPlay();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}
