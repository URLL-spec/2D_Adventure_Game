using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveOutEnemy : MonoBehaviour
{
    //������ ������� ���������� ������ ���������� ����� ���� �� ������������ � ���� �� ����
    public Destination dotleave;
    public Animator animatorbroke;
    //public FieldofView fieldcol;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animatorbroke.SetBool("wolfwalk", false);
            dotleave.Checkbox = false;
            //fieldcol.col.enabled = true;
        }

    }
   

}
