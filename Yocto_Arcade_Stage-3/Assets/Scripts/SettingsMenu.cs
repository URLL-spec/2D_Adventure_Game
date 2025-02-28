using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; //����� ��������� �� ��������� AudioMixer, ������� ��������� ������ � ����

    public void SetVolume(float volume) //���������� ��� ��������� ��������� �����;
    {
        audioMixer.SetFloat("volume", volume); // ��� ����� �� ��� � ����� ������� ��� �������������, �������� �������� ��������� "volume"
                                               // � ���������� AudioMixer �� ���������� ��������;
    }

    public void SetQuality(int qulityindex) //���������� ��� ��������� �������� �������
    {
        QualitySettings.SetQualityLevel(qulityindex); //������������� �������� ������� �� ���������� ��������
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; //������������� ������������� ����� �� ���������� ��������
    }

}
