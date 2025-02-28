using UnityEngine;


public class Enemy_SidewaysY : MonoBehaviour
{
    [SerializeField] private float damage; // [SerializeField]. ��������� ������������� ���������� ��� ����������� �� �� ������� ���������.
                                           //                   ����� �������� �������, ������� ��������� ������� ��� ���������� ������ ����������, 
                                           //                   �� �������������� � ����������.
    [SerializeField] private float movementDistance;   // ���������� �������� 
    [SerializeField] private float speed;
    private bool movingleft;
    private float leftEdge;
    private float rightEdge;
    private void Awake()
    {
        leftEdge = transform.position.y - movementDistance; // ��� ��� ���������, ����� �������� �������� ��� ������, �������, �� -5,
        rightEdge = transform.position.y + movementDistance; // � ������ �������� � ����� �.�. ������ 5
    }
    // �� ������� �� ��������� ��������� ������� � ���������� movementDistance �.�. ����������, ������� ����� �� ������� ������� ����� ��������
    private void Update()
    {
        if (movingleft) // ���� �� �������� ������ ����� �� �������� 
        {
            if (transform.position.y > leftEdge) // ������ �� ������� ������� ��� ����� ���� (�������� ������� ������� ��� �������� �� X � ������ Position � ���������� Unity)
            {
                transform.position = new Vector3(transform.position.x , transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else // ����� ������ �� ��������� ����� 
                movingleft = false;
        }
        else
        {
            if (transform.position.y < rightEdge) // ���� ����� ������ � ������ �����
            {
                transform.position = new Vector3(transform.position.x , transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
                movingleft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
