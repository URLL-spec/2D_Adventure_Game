using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HealthEnemyBat : MonoBehaviour
{
    // ������ �������� ����� �� ������ Health 

    [SerializeField] private float startingHealthEnemy; // ������� ����������, ������� ����� ���������� ������� � ��� ��, ����� �� ��������� ����. 
                                                        // [SerializeField]. ��������� ������������� ���������� ��� ����������� �� �� ������� ���������.
                                                        //                   ����� �������� �������, ������� ��������� ������� ��� ���������� ������ ����������, 
                                                        //                   �� �������������� � ����������.
                                                        //[SerializeField] private PlayerMove Ground;
    public float currentHealthEnemy { get; private set; }  // ������� ��

    // get ��������, ��� ����� �������� ���������� �� ������ ������� �������
    // private set ��������, ��� ����� ���������� � ������ ������ ����� �������
    // �.�. ������������ ���������� � ������ �������� � ����, � �������� ���, ������ � ���� �������.
    // ��������� ����� ������ �������� �� ����� �������� ������ � ������ TakeDamage
    private Animator anim;
    public bool dead;


    public Rigidbody2D rb; // ���������� ��� ����������� � ���������� ������ 
    public AIPath aipoint; // ���������� ��� ��������� ����� ��� ��������� �����
    public Transform one; // ���������� ��������� ����� � ������������
    public Transform two; // ���������� ��������� ������ � ������������
    public Destination destination; // ���������� ��� ���������� ���������� ����        // ���������� ��� ��������� ����� ������
    public GameObject Obekt; // ���������� ��� ��������� ������������ �������
    private UnityEngine.Object explosion;

    //public EnemyDamageWolf enemydamagewolf;

    void Start() // ����� ��� ���������� ����� ������
    {
        //rb = GetComponent<Rigidbody2D>(); //������������ ���������� rb ������ �� ��������� Rigidbody2D
        //aipoint = GetComponent<AIPath>();
        explosion = Resources.Load("Explosion");
    }
    private void Awake() // ��� ������ ������ ������� �������� ������ ��������� �.�. startingHealth
    {
        currentHealthEnemy = startingHealthEnemy;
        anim = GetComponent<Animator>();
    }


    public void TakeDamageEnemy(float _damage) // ����� ��������� �����
    {
        currentHealthEnemy = Mathf.Clamp(currentHealthEnemy - _damage, 0, startingHealthEnemy); // Mathf ��� ���� ����� �� �� ���������� ���� 0 ��� ���� ������������� �������� �.�. startingHealth.  

        if (currentHealthEnemy > 0) // ������� �������� ����� 
        {
            // ����� ������� �������
            anim.SetTrigger("bathurt");
            StartCoroutine(timeObj1());
            aipoint.enabled = false;
            GameObject explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (one.transform.position.x > two.transform.position.x) // ������� ��������� ������� ������������ ���������� ����� ������������ ���������
            {
                rb.velocity = new Vector2(6, 3);
            }
            else if (one.transform.position.x < two.transform.position.x)
            {
                rb.velocity = new Vector2(-6, 3);
            }
            Invoke("InvulEnemy", 1f);
            IEnumerator timeObj1() // ����� ��� ����������� �������
            {
                yield return new WaitForSeconds(0.41f); // ����� ������ ������� ������ ������������ ����������. ����� �������� = ������� ��������� �.�. 0.21f
                aipoint.enabled = true; // ��������� ���������� ������ 
            }
        }
        else
        {
            if (!dead)
            {
                // ����� ���� 
                dead = true;
                GameObject explosionRef = (GameObject)Instantiate(explosion);
                explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                gameObject.SetActive(false);
                aipoint.enabled = false; // ��������� ����������� ������ ����� ��������� �����.
                destination.enabled = false;
                Obekt.gameObject.SetActive(false);
                rb.velocity = new Vector2(0, 0); // ���������� ����� ������
            }

        }

    }

    void InvulEnemy() // �������� ������ Invul ��� TakeDamage. ������ ���������� ������� � ����� �������� �������� 
    {
        Physics2D.IgnoreLayerCollision(11, 7, false);
    }


    // �������� (Coroutines, �����������) � Unity � ������� � ������� ������ ��������� �������, ������� ������ �������� ����������� � ������� ���������� �������.
}
