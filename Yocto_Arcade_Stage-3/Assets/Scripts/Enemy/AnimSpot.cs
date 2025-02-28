using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpot : MonoBehaviour
{
    public Animator anim;
    public AnimVoid anim2;
    //public EnemyExitDamageWolf asd;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "DotSpot")
        {
            anim.Play("wolfidle");
            //anim.Play("wolfidle");
        }
        //else anim.Play("wolfwalk");

    }
}
