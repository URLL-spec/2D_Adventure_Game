using UnityEngine;
//���� ��� �������� �� ������ ���������� � �������� ���� ������������ ������ � ���� Unity 2D. 
//��� ��������� ������� ����������� �������� ������� � ������ ���������, ��� ��� ������ ���������� ������� � ����.
public class Parallax : MonoBehaviour
{
    public GameObject cam; //���������� ������, ������������ ������� ����� ��������� ������ ����.

    float stratRox;
    public float parallax; //���������� ������� ���������� � ��������� ������ ����� ��������� ������ ���� �� ��������� � ������.
    float startPosX; //������ ����������� ������� ������� �� ��� X

    void Start()
    {
        startPosX = transform.position.x;
    }

    void Update()
    {
        float distX = (cam.transform.position.x * (1 - parallax)); //����������� ��� ������������ ������� ������� ������ �� (1 - parallax),
                                                                   //�� ����, ��� ������ �������� parallax, ��� ������ ��������� ������,
                                                                   //� ������ ���� ����� ��������� ��������� ������������ ������.
        transform.position = new Vector3(startPosX + distX, transform.position.y, transform.position.z); //����� transform.position ������ ����� ������� �������
                                                                                                         //������������ ��������� ��������� ������� �� ��� X. 

    }
}
