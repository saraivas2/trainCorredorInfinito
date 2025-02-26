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
    private bool pistolBool, rifleBool, SemTiro;
    bool death = false, tiro = false;
    public float forca;
    private int vel = 1, val = 1;
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
            // Troca de arma pelo botão "1"
            if (Input.GetKeyDown(KeyCode.Alpha1))
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

            comandosMove();
            TempoTiro();

            // Atualiza o "timer" de recarga de tiro
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
            if (RecupVidaERifle() && RecupVidaEPistol())
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
        return TimerAction <= 0;
    }

    bool RecupVidaERifle()
    {
        GameObject[] enemyrifle = GameObject.FindGameObjectsWithTag("enemyRifle");
        foreach (GameObject enemy in enemyrifle)
        {
            enemiesRifle enemyScript = enemy.GetComponent<enemiesRifle>();
            if (enemyScript != null && enemyScript.GetHunting())
            {
                return false;
            }
        }
        return true;
    }

    bool RecupVidaEPistol()
    {
        GameObject[] enemypistol = GameObject.FindGameObjectsWithTag("enemyPistol");
        foreach (GameObject enemy in enemypistol)
        {
            enemiesPistol enemyScript = enemy.GetComponent<enemiesPistol>();
            if (enemyScript != null && enemyScript.GetHunting())
            {
                return false;
            }
        }
        return true;
    }

    void TempoTiro()
    {
        if (tiro)
        {
            tempotiro -= Time.deltaTime;
            if (tempotiro <= 0)
            {
                tiro = false;
                tempotiro = 30;
            }
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

    public float GetVida() => vida;
    public bool HouveTiro() => tiro;

    private void comandosMove()
    {
        mouseY += Input.GetAxisRaw("Mouse X") * sensibilidade;
        transform.eulerAngles = new Vector3(0, mouseY, 0);

        // Prioriza movimento
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPlayerMoviment();
            return;
        }
        if (Input.GetKey(KeyCode.R))
        {
            moveRun();
            AudioRunPlay();
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveWalk();
            AudioWalkPlay();
            return;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movePlayerBack();
            transform.Translate(Time.deltaTime * velocity * Vector3.back);
            return;
        }

        // Se não houver movimento, atualiza a animação e dispara se necessário
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
                // Usa animação que permite o movimento ao disparar
                AttackRifleFireWalkPlayer();
                ChamaTiroRifle();
            }
            else
            {
                AttackRifleFirePlayer();
            }
        }
        // Não forçamos uma parada se não há movimento explícito para não "travar" a animação.
    }

    private void ChamaTiroPistol()
    {
        if (!SemTiro)
        {
            AtirarInstantiate script = Pistol.GetComponentInChildren<AtirarInstantiate>();
            if (script != null)
            {
                script.InstantiateBalaDust();
            }
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
            AtirarInstantiate script = Rifle.GetComponentInChildren<AtirarInstantiate>();
            if (script != null)
            {
                script.InstantiateBalaDust();
            }
            tempo = 0.1f;
            AudioRiflePlay();
            SemTiro = true;
            tiro = true;
            tempotiro = 30;
        }
    }

    public void AudioWalkPlay() { /* Implementação se necessário */ }
    public void AudioWalkStop()
    {
        AudioController scritp = GameObject.Find("Walk").GetComponent<AudioController>();
        scritp.AudioArmasStop();
    }
    public void AudioRunPlay() { /* Implementação se necessário */ }
    public void AudioRunStop() { /* Implementação se necessário */ }
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
    public void AudioTrocaArma() { /* Implementação se necessário */ }
    public void AudioRiflePlay()
    {
        ExecutaAudio scritp = GameObject.Find("Rifle").GetComponent<ExecutaAudio>();
        scritp.InstantiateAudio();
    }
    public void SoundEffectDamage() { /* Implementação se necessário */ }

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
        transform.Translate(Vector3.forward * velocity * vel * Time.deltaTime);
    }

    private void moveWalk()
    {
        if (Input.GetMouseButton(0))
        {
            if (pistolBool)
            {
                AttackPistolFireWalkPlayer();
                ChamaTiroPistol();
            }
            else if (rifleBool)
            {
                AttackRifleFireWalkPlayer();
                ChamaTiroRifle();
            }
        }
        else
        {
            // Caso nenhum botão de disparo seja pressionado, segue a animação de caminhada
            movePlayer();
            vel = 1;
        }
        transform.Translate(Vector3.forward * velocity * vel * Time.deltaTime);
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
        animator.SetFloat("Z", 0);
        animator.SetFloat("K", 1);
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
