using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Invoke("Delay", 1.8f); //Время спустя которое сработает, нужно для затемнения экрана
    }

    void Delay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Загрузить сцену, которая на 1 значение больше чем индекс текущей сцены
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}
