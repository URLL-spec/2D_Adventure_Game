using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Скрипт запускает в конце чёрный экран
public class StartEnding : MonoBehaviour
{
    public GameObject StartFog;
    public GameObject StartWall;
    public GameObject StartBlackScreen;
    
    void Start()
    {
        StartFog.SetActive(false);
        StartWall.SetActive(false);
        StartBlackScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartBlackScreen.SetActive(true);
            StartFog.SetActive(true);
            StartWall.SetActive(true);
            Invoke("StartEndingDelay", 7.5f);
        }
    }

    void StartEndingDelay()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
