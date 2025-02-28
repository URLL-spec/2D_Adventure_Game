using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent; 
    public string[] lines;  
    public float textSpeed; // �������� ������ 

    public GameObject DialogStart; // ������ ������� ����� ����� ���������
    public GameObject DialogText; // ������ ������� ����� ����� ���������
    public CameraShake CameraShake;
    public PlayerMove Movement; // ���������� ������ �� ������ (�� �� ������, ������ ��������)
    public PlayerDamage Damage; // ���������� ������ �� ������ 
    public Rigidbody2D rb; // ���������� ������ ��������� ��� ��� ���������
    public Animator anim; // ��������

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
        else // ����� �.�. ������ ������������ 
        {
            Movement.enabled = true; // ������� ����������
            Damage.enabled = true; // ������� ����������� �������� ����
            CameraShake.enabled = true;
            //Damage.SetActive(true);
            Movement.anim.SetBool("startdialog", false); // ��������� �������� idle 
            DialogStart.gameObject.SetActive(false); // ��������� ������
            DialogText.gameObject.SetActive(false); // ��������� ������
            anim.SetTrigger("TriggerExitImage"); // ������ �������� ������������ �������

            Invoke("ExitDialogAnimationImage", 0.35f);
        }
    }

    void ExitDialogAnimationImage()
    {
        gameObject.SetActive(false); // ���������� ������ �������
    }

}
