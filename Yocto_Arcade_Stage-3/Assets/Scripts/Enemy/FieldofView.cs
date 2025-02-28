using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldofView : MonoBehaviour
{
    // Скрипт, который представляет из себя поле зрения волка
    public Destination dotagressive;
    public Animator anim;

    public void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.tag == "Player")
            {
            anim.SetBool("wolfwalk", true);
            dotagressive.Checkbox = true;
            }

    }
}
