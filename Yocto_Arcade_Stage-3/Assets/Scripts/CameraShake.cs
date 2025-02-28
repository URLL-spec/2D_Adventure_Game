using Cinemachine;//импортирует в проект библиотеку Cinemachine, котора€ €вл€етс€ плагином дл€ Unity
                  //и предоставл€ет компоненты дл€ создани€ и настройки камерных движений.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //Ётот код отвечает за камеру и добавл€ет в игру эффект тр€ски при срабатывании услови€. 
    [SerializeField] CinemachineVirtualCamera vCamera; //отвечает за виртуальную камеру, котора€ была добавлена в сцену настроена в соответствии с требовани€ми игры.
    [SerializeField] float Amplitude; //отвечает за максимальную амплитуду тр€ски камеры.
    [SerializeField] float Frequency; //отвечает за частоту колебаний камеры.
    public PlayerDamage dopMeaning; // переменна€ дл€ того чтобы камера не тр€слась при каждом нажатии Ћ ћ

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake() // орутина Shake() измен€ет значени€ Amplitude и Frequency кинематического компонента Cinemachine
                        //и затем задерживает выполнение на 0,2 секунды, после чего возвращает значени€
                        //Amplitude и Frequency в исходное состо€ние. Ётот набор действий создаЄт эффект тр€ски камеры.
    {
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Amplitude;
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Frequency;
        yield return new WaitForSeconds(0.2f);
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

}
