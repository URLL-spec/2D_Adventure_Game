using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Destination : MonoBehaviour
{
    //������, ������� ��������� ����� ���������� ����
    public Transform target; // ���� 1 �.�. ����� ��� ��������� ���� ���������� (����� ��� ������������ ���������)
    public Transform target2; // ���� 2 �.�. ��� �������� �� ������� ����� ����������� ������������� 
    IAstarAI ai; //������������ ��� ���������� ��������� ����������;
    public bool Checkbox = false;

    void OnEnable() //����������� 
    {
        ai = GetComponent<IAstarAI>();
        if (ai != null) ai.onSearchPath += Update; // ����� ����������� ������ ���� ����� ���������
    }

    void OnDisable() //�� ����������� 
    {
        if (ai != null) ai.onSearchPath -= Update; // ����� �� ����������� ������ ���� ����� ������
    }

    void Update()
    {
        if (!Checkbox) ai.destination = target.position; // ���� checkbox = false ����� ��������� �� ����� 1 ����� ��������� �� ����� 2
        else ai.destination = target2.position;
    }
}
