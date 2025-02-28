using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyExitDamageWolf : MonoBehaviour
{
    // ������ ��� ������������� �������� ����� � ��������� 
    public AIPath aiPath;
    public EnemyReflectWolf enemyreflectwolf;
    public Animator anim;
    public BoxCollider2D enemydamagewolf;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemydamagewolf.enabled = false;
            anim.SetTrigger("wolfexitdamage");
            StartCoroutine(timeObj1());
            aiPath.enabled = false;
            enemyreflectwolf.enabled = false;
        }
        IEnumerator timeObj1() // ����� ��� ����������� �������
        {
            yield return new WaitForSeconds(1f); // ����� ������ ������� ���� ����� ������� �������� �� ������ 
            enemydamagewolf.enabled = true;
            aiPath.enabled = true; // ��������� ��
            enemyreflectwolf.enabled = true; // ��������� �������� �����
        }
    }
    
}
