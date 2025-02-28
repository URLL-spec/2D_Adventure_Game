using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; //будет ссылаться на компонент AudioMixer, который управляет звуком в игре

    public void SetVolume(float volume) //вызывается при изменении громкости звука;
    {
        audioMixer.SetFloat("volume", volume); // Имя такое же как в Аудио микшере для парсингования, изменяет значение параметра "volume"
                                               // в компоненте AudioMixer на переданное значение;
    }

    public void SetQuality(int qulityindex) //вызывается при изменении качества графики
    {
        QualitySettings.SetQualityLevel(qulityindex); //устанавливает качество графики на переданное значение
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; //устанавливает полноэкранный режим на переданное значение
    }

}
