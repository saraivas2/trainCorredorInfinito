using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Texture2D crosshairTex;

    private void Start()
    {
        // Cria uma textura 1x1 de cor branca para representar o ponto do crosshair
        crosshairTex = new Texture2D(1, 1);
        crosshairTex.SetPixel(0, 0, Color.white);
        crosshairTex.Apply();
    }

    private void OnGUI()
    {
        // Define o tamanho do ponto (por exemplo, 4x4 pixels)
        int size = 4;
        float xMin = (Screen.width  - size) / 2;
        float yMin = (Screen.height - size) / 2;
        GUI.DrawTexture(new Rect(xMin, yMin, size, size), crosshairTex);
    }
}