using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimVoid : MonoBehaviour
{
    //������ ��� �������� �������� Wolf
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DotSpot")
        {
            anim.Play("wolfidle");
        }
    }
}
