using System.Collections; //������������ ��� ������ � ����������� ������ ����� C#.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//���� ��� �������� �� ����� � ����

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; //���������� ������� ��������� ���� � �� ����� ��� ���.

    public GameObject pauseMenuUI; //��� ������-����, �������������� �� ����� �����. 

    public GameObject ScreenBlack; //����� ��� ���������� ������ 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //���������, ���� �� ������ ������� Escape. ���� ��, �� ���� ������ ���� �� �����,
                                             //� ��������� ������ � ������������ ����.
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

    public void Resume() //���������� ��� ������������� ����, � �� ��������� ������ "pauseMenuUI",
                         //������������� ����� (Time.timeScale) �� 1 � �������� �������� GameIsPaused �� false,
                         //����� ���� �������������.
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() //���������� ��� ������������ ���� � ���������� ������ "pauseMenuUI",
                 //������������� ����� (Time.timeScale) �� 0 � �������� �������� GameIsPaused �� true.
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() //������������ ��� ������ �� ���� � ������� ���� (Main Menu). ���� ����� ��������� ������� ���� (Main Menu),
                           //������������� ����� (Time.timeScale) �� 1 � �������� �������� GameIsPaused �� false. 
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }    

}
