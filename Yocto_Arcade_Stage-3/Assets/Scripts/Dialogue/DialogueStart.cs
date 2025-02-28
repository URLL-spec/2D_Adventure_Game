using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    // Скрипт который начинает диалог запуском объекта Panel

    public GameObject start; // Объект который нужно запускать

    public PlayerMove Animation; // Объект с помощью которого можно запускать анимацию idle персонажа
    void Start()
    {
        start.SetActive(false); // С самого начала объет будет выключен
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Animation.StartAnimDialoge(); // Запуск анимации idle при пересечении
            start.SetActive(true); // Запуск объекта
        }
    }
}
