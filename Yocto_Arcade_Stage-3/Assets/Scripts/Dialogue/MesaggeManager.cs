using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MesaggeManager : MonoBehaviour
{
    public GameObject obekt1;
    public GameObject obekt2;
    public GameObject obekt3;
    public GameObject obekt4;
    public GameObject obekt5;
    public GameObject obekt6;
    public GameObject obekt7;
    public GameObject obekt8;
    public GameObject obekt9;
    public GameObject obekt10;

    public DialogueManager dialogueManager;

    public Animator AnimationMessageList1;
    public Animator AnimationMessageList2;
    public Animator AnimationMessageList3;
    public Animator AnimationMessageList4;
    public Animator AnimationMessageList5;
    public Animator AnimationMessageList6;
    public Animator AnimationMessageList7;
    public Animator AnimationMessageList8;
    public Animator AnimationMessageList9;
    public Animator AnimationMessageList10;

    public MessageStart checkinbox;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        MessageNext();
    }


    void MessageNext()
    {
        if (dialogueManager.i == 0)
        {
            AnimationMessageList1.SetBool("DialogExitMessage", false); //Изначально bool является false
            obekt1.SetActive(true);                                    //Включение нулевого
        }
        if (dialogueManager.i == 1)
        {
            AnimationMessageList1.SetBool("DialogExitMessage", true);  //bool становится true, из-за чего объект пропадает 
        }

        if (dialogueManager.i == 3)
        {
            AnimationMessageList2.SetBool("DialogExitMessage1", false);//bool является false
            obekt1.SetActive(false);                                   //Выключение нулевого
            obekt2.SetActive(true);                                    //Включение первого 
        }
        if (dialogueManager.i == 4) 
        {
            AnimationMessageList2.SetBool("DialogExitMessage1", true); //bool становится true, из-за чего объект пропадает 
        }

        if (dialogueManager.i == 6)
        {
            AnimationMessageList3.SetBool("DialogExitMessage2", false);
            obekt2.SetActive(false);
            obekt3.SetActive(true);
        }
        if (dialogueManager.i == 7)
        {
            AnimationMessageList3.SetBool("DialogExitMessage2", true);
        }

        if (dialogueManager.i == 9)
        {
            AnimationMessageList4.SetBool("DialogExitMessage3", false);
            obekt3.SetActive(false);
            obekt4.SetActive(true);
        }
        if (dialogueManager.i == 10)
        {
            AnimationMessageList4.SetBool("DialogExitMessage3", true);
        }

        if (dialogueManager.i == 12)
        {
            AnimationMessageList5.SetBool("DialogExitMessage4", false);
            obekt4.SetActive(false);
            obekt5.SetActive(true);
        }
        if (dialogueManager.i == 13)
        {
            AnimationMessageList5.SetBool("DialogExitMessage4", true);
        }

        if (dialogueManager.i == 15)
        {
            AnimationMessageList6.SetBool("DialogExitMessage5", false);
            obekt5.SetActive(false);
            obekt6.SetActive(true);
        }
        if (dialogueManager.i == 16)
        {
            AnimationMessageList6.SetBool("DialogExitMessage5", true);
        }

        if (dialogueManager.i == 18)
        {
            AnimationMessageList7.SetBool("DialogExitMessage6", false);
            obekt6.SetActive(false);
            obekt7.SetActive(true);
        }
        if (dialogueManager.i == 19)
        {
            AnimationMessageList7.SetBool("DialogExitMessage6", true);
        }

        if (dialogueManager.i == 21)
        {
            AnimationMessageList8.SetBool("DialogExitMessage7", false);
            obekt7.SetActive(false);
            obekt8.SetActive(true);
        }
        if (dialogueManager.i == 22)
        {
            AnimationMessageList8.SetBool("DialogExitMessage7", true);
        }

        if (dialogueManager.i == 24)
        {
            AnimationMessageList9.SetBool("DialogExitMessage8", false);
            obekt8.SetActive(false);
            obekt9.SetActive(true);
        }
        if (dialogueManager.i == 25)
        {
            AnimationMessageList9.SetBool("DialogExitMessage8", true);
        }

        if (dialogueManager.i == 27)
        {
            AnimationMessageList10.SetBool("DialogExitMessage9", false);
            obekt9.SetActive(false);
            obekt10.SetActive(true);
        }
        if (dialogueManager.i == 28)
        {
            AnimationMessageList10.SetBool("DialogExitMessage9", true);
        }
    }

}
