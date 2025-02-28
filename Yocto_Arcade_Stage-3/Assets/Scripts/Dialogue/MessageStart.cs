using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageStart : MonoBehaviour
{
    // ������ ������� �������� ��������� ��� ������� � ����������, ������ MessagePanel

    public GameObject MessageManager; // ��� ������ ������� ����� ��������� � ���������
    public bool checkinbox; // �������� ������� ���� ��������� �� �������� ��� � box �������

    void Start()
    {
        checkinbox = false;
        MessageManager.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            checkinbox= true;
            MessageManager.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            checkinbox = false;
        }
    }

}
