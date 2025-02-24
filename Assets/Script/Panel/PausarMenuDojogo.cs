using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarMenuDojogo : MonoBehaviour
{
    public static bool pausa = false;  // Agora é acessível globalmente
    public Canvas canvas;

    private void Start()
    {
        Time.timeScale = 1.0f;
        canvas.GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausa = !pausa;
            PausarGame();
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("CenaMenu");
    }

    public void PausarGame()
    {
        if (pausa)
        {
            Time.timeScale = 0f;
            canvas.GetComponent<Canvas>().enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1.0f;
            canvas.GetComponent<Canvas>().enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
