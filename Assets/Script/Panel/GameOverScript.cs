using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public Canvas canvas;
    private float timer = 1.0f;
    private bool show = false;

    void Start()
    {
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }

    void Update()
    {
        if (show)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                canvas.enabled = false;
                timer = 1.0f;
                show = false;
            }
        }
    }

    public void ShowTelaGameOver(bool value)
    {
        show = value;
        if (show)
        {
            canvas.enabled = true;
            timer = 1.0f;  
        }
    }
}
