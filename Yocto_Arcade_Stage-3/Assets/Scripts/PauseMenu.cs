using System.Collections; //используются для работы с коллекциями данных языка C#.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Этот код отвечает за паузу в игре

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; //определяет текущее состояние игры — на паузе или нет.

    public GameObject pauseMenuUI; //это объект-меню, отображающийся во время паузы. 

    public GameObject ScreenBlack; //нужна для затемнения экрана 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //проверяет, была ли нажата клавиша Escape. Если да, то меню ставит игру на паузу,
                                             //в противном случае — возобновляет игру.
        {
            if (GameIsPaused) 
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void Resume() //вызывается при возобновлении игры, и он отключает объект "pauseMenuUI",
                         //устанавливает время (Time.timeScale) на 1 и изменяет значение GameIsPaused на false,
                         //чтобы игра возобновилась.
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() //вызывается при приостановке игры и показывает объект "pauseMenuUI",
                 //устанавливает время (Time.timeScale) на 0 и изменяет значение GameIsPaused на true.
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() //используется для выхода из игры в главное меню (Main Menu). Этот метод загружает главное меню (Main Menu),
                           //устанавливает время (Time.timeScale) на 1 и изменяет значение GameIsPaused на false. 
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }    

}
