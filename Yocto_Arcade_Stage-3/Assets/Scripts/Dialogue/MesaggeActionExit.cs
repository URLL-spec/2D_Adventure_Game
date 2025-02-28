using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaggeActionExit : MonoBehaviour
{
    //Скрипт для выключения сообщения при нажатии на объект F

    //public GameObject MessageExiting;
    public Animator AnimationMessageExit;

    // Start is called before the first frame update
    //void Start()
    //{
    //    MessageExiting.SetActive(true); //!!!!!!!!!
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AnimationMessageExit.SetBool("DialogExitMessage10", true);
        }
    }
}
