using UnityEngine;


public class Enemy_SidewaysX_Left : MonoBehaviour
{
    [SerializeField] private float damage; // [SerializeField]. ��������� ������������� ���������� ��� ����������� �� �� ������� ���������.
                                           //                   ����� �������� �������, ������� ��������� ������� ��� ���������� ������ ����������, 
                                           //                   �� �������������� � ����������.
    [SerializeField] private float movementDistance;   // ���������� �������� 
    [SerializeField] private float speed;
    private bool movingright;
    private float leftEdge;
    private float rightEdge;
    private void Awake()
    {
        leftEdge = transform.position.x + movementDistance; // ��� ��� ���������, ����� �������� �������� ��� ������, �������, �� -5,
        rightEdge = transform.position.x - movementDistance; // � ������ �������� � ����� �.�. ������ 5
    }
    // �� ������� �� ��������� ��������� ������� � ���������� movementDistance �.�. ����������, ������� ����� �� ������� ������� ����� ��������
    private void Update()
    {
        if (movingright) // ���� �� �������� ����� ����� �� �������� 
        {
            if (transform.position.x < leftEdge) // ������ �� ������� ������� ��� ����� ���� (�������� ������� ������� ��� �������� �� X � ������ Position � ���������� Unity)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else // ����� ������ �� ��������� ����� 
                movingright = false;
        }
        else
        {
            if (transform.position.x > rightEdge) // ���� ����� ������ � ������ �����
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingright = true;
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
