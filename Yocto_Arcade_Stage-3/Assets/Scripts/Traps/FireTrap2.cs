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
        InvokeRepeating("act", 0f, 2f); // ������ ��� ������� ����� �������������� ������� atc()
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
        Invoke("act2", 1.9f); // ���� ����� ������� �� ���������� 1.5 �������
    }

    void act2() // � �� ��� �� �� �����
    {
        anim.Play("Firetrap");
        GetComponent<Collider2D>().enabled = true;
    }

}

