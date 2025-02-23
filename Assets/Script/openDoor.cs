using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public Transform doorPos;

    void Update()
    {
        float distance = Vector3.Distance(player.position, doorPos.position);
        if (distance <= 3) 
        {
            anim.SetBool("Near", true);
        }
        else
        {
            anim.SetBool("Near", false);
        }
    }


}