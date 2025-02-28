using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float damageEnemy; //damageEnemy �������� �� ���������� �����, ������� ����� ������� ��������.
    public Rigidbody2D CharacterRb; // ���������� ��� ����������� � ���������� ������ 
    public BoxCollider2D box; // ������������ � ������� ����� �������� ����
    public Animator anim; // ��������
    public PlayerMove move; // ���������� ��� ����������� � ���������� ������ 

    [SerializeField] private AudioSource hitWolfSoundEffect;
    [SerializeField] private AudioSource hitBatSoundEffect;

    private void Start()
    {
        box.enabled= false; // �� ������ ��������� ����
    }

    private void Update()
    {
        BoxActive(); // ������ ���� ����� ����������� ������������� �������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���, ������� ��������� � �������, ����� �������� ���� ���� �� ����� ���������� � ������� ���� ����������� ������
        BoxActive();
        if (collision.tag == "EnemyDamage")
            {
                collision.GetComponent<HealthEnemy>().TakeDamageEnemy(damageEnemy); // ������ �������, ������� �������� ����
                hitWolfSoundEffect.Play();
            }
        else if (collision.tag == "EnemyDamageBat")
            {
                collision.GetComponent<HealthEnemyBat>().TakeDamageEnemy(damageEnemy);
                hitBatSoundEffect.Play();
            }
    }

    void BoxActive() //����� ���� ������� ����������� � ��������� ������� ��������� ����� �.�. ����� �� ����� ����� �������� �� ��� �������� ��� ������� � �.�.
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X)) && !lockattack && !move.lockjump)
        {
            lockattack = true;
            move.enabled = false;
            anim.SetTrigger("attackrandom");
            anim.SetInteger("attackid", Random.Range(0, 2)); // ����� ��� ��������� �������� +1 � ������ ���������� 
            if (move.OnGround) // ���� �������� �� ����� ����� ������ ����� �� �����
            {
                CharacterRb.velocity = new Vector2(0, 0);
            }
            Invoke("LockAttack", 0.3f); // ����� �����
            Invoke("DamageTimeON", 0.1f); // ����� ������ ����������� � ��������� boxcollider2d ��� ������� ������ ���� � ������������ ����������
        }
    }

    void DamageTimeON()
    {
        box.enabled = true;
        Invoke("DamageTimeOFF", 0.1f); // ����� ������ ������� ���� ����������
    }

    public bool lockattack = false;
    void LockAttack() // ����� ��� �������� ����� (����� ����� �� ������ ������)
    {
        lockattack = false;
    }
    void DamageTimeOFF()
    {
        move.enabled = true;
        box.enabled = false;
    }

}
