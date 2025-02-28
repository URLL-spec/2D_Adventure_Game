using Cinemachine;//����������� � ������ ���������� Cinemachine, ������� �������� �������� ��� Unity
                  //� ������������� ���������� ��� �������� � ��������� �������� ��������.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //���� ��� �������� �� ������ � ��������� � ���� ������ ������ ��� ������������ �������. 
    [SerializeField] CinemachineVirtualCamera vCamera; //�������� �� ����������� ������, ������� ���� ��������� � ����� ��������� � ������������ � ������������ ����.
    [SerializeField] float Amplitude; //�������� �� ������������ ��������� ������ ������.
    [SerializeField] float Frequency; //�������� �� ������� ��������� ������.
    public PlayerDamage dopMeaning; // ���������� ��� ���� ����� ������ �� �������� ��� ������ ������� ���

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake() //�������� Shake() �������� �������� Amplitude � Frequency ��������������� ���������� Cinemachine
                        //� ����� ����������� ���������� �� 0,2 �������, ����� ���� ���������� ��������
                        //Amplitude � Frequency � �������� ���������. ���� ����� �������� ������ ������ ������ ������.
    {
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Amplitude;
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Frequency;
        yield return new WaitForSeconds(0.2f);
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

}
