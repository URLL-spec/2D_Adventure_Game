using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageWolf : MonoBehaviour
{
    [SerializeField] private float damage;

    public Animator animationdamage;
    public Health animationdie;
    public Rigidbody2D rb;
    public AIPath ai;
    public BoxCollider2D col;


    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("act", 0.01f);
            animationdamage.SetTrigger("wolfattack");
            collision.GetComponent<Health>().TakeDamage(damage);
            if (animationdie.dead)
            {
                animationdamage.SetBool("wolfauf", true);
                ai.enabled = false;
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    void act()
    {
        col.enabled = false;
        Invoke("act2", 2f);
    }   

    void act2()
    {
        col.enabled = true;
    }
}
