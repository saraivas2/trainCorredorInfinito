using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCheck : MonoBehaviour
{
    public GameObject roadSection;
    public static int vagaoCounter = 0;
    
    // Armazena o horário (Time.time) em que o último vagão será destruído
    public static float nextDestructionTime = 0f;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Trigger"))
        {
            // Reposiciona apenas o Trigger com o qual o player colidiu
            Vector3 triggerPos = other.gameObject.transform.position;
            other.gameObject.transform.position = new Vector3(triggerPos.x, triggerPos.y + 100, triggerPos.z);
            
            // Cria o novo vagão na posição desejada
            Vector3 spawnPosition = new Vector3(0, 0, transform.position.z + 9);
            GameObject newSection = Instantiate(roadSection, spawnPosition, roadSection.transform.rotation);
            
            vagaoCounter++;
            newSection.name = "vagao-" + vagaoCounter;
            
            // Após instanciar, localiza o Trigger dentro do novo vagão
            Collider[] childColliders = newSection.GetComponentsInChildren<Collider>();
            foreach (Collider col in childColliders)
            {
                if (col.gameObject.CompareTag("Trigger"))
                {
                    // Define a posição local para que o Trigger fique com y = 0
                    Vector3 localPos = col.transform.localPosition;
                    col.transform.localPosition = new Vector3(localPos.x, 5.41f, localPos.z);
                }
            }
            
            // Calcula o delay de destruição:
            // Se o tempo atual é menor que o horário do próximo ciclo de destruição, 
            // aguarda o período remanescente e adiciona 10 segundos; caso contrário, usa 10 segundos
            float currentTime = Time.time;
            float delay = 10f;
            if(currentTime < nextDestructionTime)
            {
                delay = (nextDestructionTime - currentTime) + 10f;
            }
            nextDestructionTime = currentTime + delay;
            
            Destroy(newSection, delay);
        }
    }
}
