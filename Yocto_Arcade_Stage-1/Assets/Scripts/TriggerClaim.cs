using UnityEngine;

public class TriggerClaim : MonoBehaviour
{

    PlayerMove CharControl;
    BoxCollider2D box;

    void Start()
    {
        CharControl = GetComponentInParent<PlayerMove>();
        box = GetComponents<BoxCollider2D>()[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6) { box.enabled = false; } // ���� ������������ ������ ��������� �� ���� ��� ������� X �� �� ��������� ��������� �������� 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) { box.enabled = true; } // ���� ������ �� ������������ ����� �������� ��� ������� ��� ������ 
    }

    private void OnCollisionEnter2D(Collision2D collision) // ����� ����� ���������� ����� ��������� ������������ ���������� ��������� � ������� ���������
    {
        if (collision.gameObject.layer == 6 && CharControl.rb.velocity.y <= 0) { CharControl.StartAnimLeadge(); } // ���� ������ � ������� ��������� ������������ ��������� ��� ������� x
                                                                                                                  // � ��������� � ��������� ������� �.�. CharControl.rb.velocity.y <= 0
                                                                                                                  // �� ������ CharControl.StartAnimLeadge
    }

}
