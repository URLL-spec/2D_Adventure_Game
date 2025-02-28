using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;

public class FireTrap2 : MonoBehaviour
{
    [SerializeField] private float damage;
    public Animator anim;
    private void Start()
    {
        GetComponent<Collider2D>().enabled = true;
        InvokeRepeating("act", 0f, 2f); // Каждые две секунды будет активироваться функция atc()
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    void act()
    {
        GetComponent<Collider2D>().enabled = false;
        Invoke("act2", 1.9f); // Урон будет включен на протяжении 1.5 секунды
    }

    void act2() // и за тем всё по новой
    {
        anim.Play("Firetrap");
        GetComponent<Collider2D>().enabled = true;
    }

}

