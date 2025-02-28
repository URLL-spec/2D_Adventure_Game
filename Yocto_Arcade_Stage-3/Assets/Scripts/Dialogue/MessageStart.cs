using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageStart : MonoBehaviour
{
    // Скрипт который начинает сообщения для общения с персонажем, объект MessagePanel

    public GameObject MessageManager; // Сам объект который нужно отключать и выключать
    public bool checkinbox; // Проверка наличия того находится ли персонаж ещё в box триггер

    void Start()
    {
        checkinbox = false;
        MessageManager.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            checkinbox= true;
            MessageManager.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            checkinbox = false;
        }
    }

}
