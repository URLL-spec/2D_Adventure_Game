using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent; 
    public string[] lines;  
    public float textSpeed; // Скорость текста 

    public GameObject DialogStart; // Объект который нужно будет отключить
    public GameObject DialogText; // Объект который нужно будет отключить
    public CameraShake CameraShake;
    public PlayerMove Movement; // Переменная ссылка на скрипт (не на объект, обрати внимание)
    public PlayerDamage Damage; // Переменная ссылка на скрипт 
    public Rigidbody2D rb; // Переменная физики персонажа для его остановки
    public Animator anim; // Анимация

    private int index;
    public int i;

    // Start is called before the first frame update
    void Start()
    {
        CameraShake.enabled = false;
        Movement.enabled = false;
        Damage.enabled = false;
        rb.velocity = new Vector2(0, 0);

        i = 0;

        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        { 
            if(textComponent.text == lines[index]) 
            {
                i = i + 1;
                NextLine();
            }
            //else
            //{
            //    StopAllCoroutines();
            //    textComponent.text = lines[index];
            //}
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1) 
        {
            index++; 
            textComponent.text = string.Empty; 
            StartCoroutine(TypeLine());
        }
        else // Иначе т.е. диалог заканчиватся 
        {
            Movement.enabled = true; // Вернуть управление
            Damage.enabled = true; // Вернуть возможность наносить урон
            CameraShake.enabled = true;
            //Damage.SetActive(true);
            Movement.anim.SetBool("startdialog", false); // Закончить анимацию idle 
            DialogStart.gameObject.SetActive(false); // Выключить объект
            DialogText.gameObject.SetActive(false); // Выключить объект
            anim.SetTrigger("TriggerExitImage"); // Начать анимацию исчезновения диалога

            Invoke("ExitDialogAnimationImage", 0.35f);
        }
    }

    void ExitDialogAnimationImage()
    {
        gameObject.SetActive(false); // Выключение объета диалога
    }

}
