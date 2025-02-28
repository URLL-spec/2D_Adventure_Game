using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint; //���������� ����������, ������� ����� ������� ������� ���������� ��������� ���������
    private Health playerHealth; //���������� ����������, ������� ����� ��������� �� ������ "Health", ���������� �� �������� ������

    private void Awake() //�����, ������� ���������� �� ������ ������ Start() � ������������ ��� ������������� ������� � ������ ��������
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn() //�����, ������� ���������� ��� �������� ������ ����� ��������;
    {
        transform.position = currentCheckpoint.position; // ���������� ������ �� ������� ���������
        // ��������������� �������� ������ � ���������� ��������
        playerHealth.Respawn();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //��������� �������� �������� ����������� ��� ����������� �������������
            collision.GetComponent<Collider2D>().enabled = false; //����� ��������� ���� �����������
        }    
    }

}
