using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth; // ������� ����������, ������� ����� ���������� ������� � ��� ��, ����� �� ��������� ����. 
                                                   // [SerializeField]. ��������� ������������� ���������� ��� ����������� �� �� ������� ���������.
                                                   //                   ����� �������� �������, ������� ��������� ������� ��� ���������� ������ ����������, 
                                                   //                   �� �������������� � ����������.
    //[SerializeField] private PlayerMove Ground;
                     public float currentHealth { get; private set; }  // ������� �� ������.

    // get ��������, ��� ����� �������� ���������� �� ������ ������� �������
    // private set ��������, ��� ����� ���������� � ������ ������ ����� �������
    // �.�. ������������ ���������� � ������ �������� � ����, � �������� ���, ������ � ���� �������.
    // ��������� ����� ������ �������� �� ����� �������� ������ � ������ TakeDamage
                     private Animator anim;
                     public bool dead;
                    

    public Rigidbody2D rb; // ���������� ��� ����������� � ���������� ������ 
    public GameObject time; // ���������� ��� ������ AddHealth �.�. ���������� ������� 
    public PlayerDamage playerdamage;
    void Start() // ����� ��� ���������� ����� ������
    {
        rb = GetComponent<Rigidbody2D>(); //������������ ���������� rb ������ �� ��������� Rigidbody2D
    }
    private void Awake() // ��� ������ ������ ������� �������� ������ ��������� �.�. startingHealth
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }


    public void TakeDamage(float _damage) // ����� ��������� �����
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // Mathf ��� ���� ����� �� �� ���������� ���� 0 ��� ���� ������������� �������� �.�. startingHealth.  

        if (currentHealth > 0) // ������� �������� ����� 
        {
            // ����� ������� �������
            anim.SetTrigger("hurt");
            StartCoroutine(timeObj1());
            GetComponent<PlayerMove>().enabled = false;
            rb.velocity = new Vector2(7, 7); // ��� ��������� ����� �������� �����������
            Physics2D.IgnoreLayerCollision(7, 13, true);// ��������� � ���������� Physics2D � ������ ������������� ������������ ����. ��������� ��� ��� 7 � 13 ����
                                                        // ��� ���� �������� true
            Physics2D.IgnoreLayerCollision(7, 11, true);
            Invoke("InvulTraps", 1.5f);               //Invoke - ����� ����������� ������� �������. ����� ��� �������� ��� ����� ��-� Invul
                                                      //����� ��������� ����� x ������
            Invoke("InvulEnemy", 1.5f);
            IEnumerator timeObj1() // ����� ��� ����������� �������
            {
                yield return new WaitForSeconds(0.25f); // ����� ������ ������� ������ ������������ ����������. ����� �������� = ������� ��������� �.�. 0.21f
                GetComponent<PlayerMove>().enabled = true; // ��������� ���������� ������ 
            }
    }
        else
        {
            if (!dead)
            {
                // ����� ���� 
                anim.SetTrigger("die");
                GetComponent<PlayerMove>().enabled = false; // ��������� ����������� ������ ����� ����� �����.
                playerdamage.enabled = false;
                rb.velocity = new Vector2(0, 0); // ���������� ����� ������
                dead = true;
            }

        }

    }
    void InvulTraps() // �������� ������ Invul ��� TakeDamage. ������ ���������� ������� � ����� �������� �������� 
    {
        Physics2D.IgnoreLayerCollision(7, 13, false); // ��� 7 � 13 ���� ��������� ������������� ������������ (false - �������� ��� ���������)
    }

    void InvulEnemy() // �������� ������ Invul ��� TakeDamage. ������ ���������� ������� � ����� �������� �������� 
    {
        Physics2D.IgnoreLayerCollision(7, 11, false);
    }

    public void AddHealth(float _value) // ����� ��� ������� HelthCollectible ����� ���� ����������� �������
    {
        if (startingHealth == currentHealth) // ���� ��������� �� ����� �������� �.�. ����������� ��� ����� ������ ��������� ��� ��������
        {
            GetComponent<HealthCollectible>().enabled = false; // ��������� HealthCollectible
        }
        else
        {
            if (!GetComponent<PlayerMove>().OnGround) // ���� �������� �� ����� �� ����� ����� ������ ��������� ��� ��������
            { 
                currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth); // ������������� � HealthCollectible ��� ����������� ������� 
            }
            else // ����� �.�. ���� �������� ����� �� �����, ����� ��������� ������� ����
            {
                anim.Play("takeheal");
                StartCoroutine(timeObj()); // ����� ��������
                GetComponent<PlayerMove>().enabled = false; // ���������� ���������� ������ 
                rb.velocity = new Vector2(0, 0); // ���������� ����������� ��� �� ��������� 
                currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth); // ������������� � HealthCollectible ��� ����������� ������� 

                IEnumerator timeObj() // ����� ��� ����������� �������
                {
                    yield return new WaitForSeconds(0.21f); // ����� ������ ������� ������ ������������ ����������. ����� �������� = ������� ��������� �.�. 0.21f
                    GetComponent<PlayerMove>().enabled = true; // ��������� ���������� ������ 
                }
            }
        }
    }

    // �������� (Coroutines, �����������) � Unity � ������� � ������� ������ ��������� �������, ������� ������ �������� ����������� � ������� ���������� �������.
}
