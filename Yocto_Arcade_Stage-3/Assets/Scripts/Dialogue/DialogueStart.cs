using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    // ������ ������� �������� ������ �������� ������� Panel

    public GameObject start; // ������ ������� ����� ���������

    public PlayerMove Animation; // ������ � ������� �������� ����� ��������� �������� idle ���������
    void Start()
    {
        start.SetActive(false); // � ������ ������ ����� ����� ��������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Animation.StartAnimDialoge(); // ������ �������� idle ��� �����������
            start.SetActive(true); // ������ �������
        }
    }
}
