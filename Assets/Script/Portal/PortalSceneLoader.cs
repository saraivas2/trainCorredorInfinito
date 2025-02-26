using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneLoader : MonoBehaviour
{
    // Nome da cena a ser carregada, conforme definido no Build Settings do Unity
    public string sceneName;

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;
            SceneManager.LoadScene(sceneName);
        }
    }
}