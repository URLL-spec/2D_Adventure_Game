using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay; // Задержка активации после нажатия игрока
    [SerializeField] private float activeTime; // Как долго остаётся активной после её активации
    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool triggered; // Когда ловукшка запущена
    private bool active; // Когда ловушка активана с может нанести урон
    private Health player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Если коллизия пересекает объект с тэгом Player
        {
            if (!triggered)// тогда ловушка перестаёт быть незапущенной и запускает Корутин
            { 
            StartCoroutine(ActivateFiretrap());
            player = collision.GetComponent<Health>();
            } 
        }
    }


    private void OnTriggerExit2D(Collider2D collision) // Нужно чтобы игрок получал урон ТОЛЬКО тогода когда он находится внутри коллайдера
    {
        player = null;
    }

    private void Update()
    {
        if (active && player != null) // Когда ставновится активной и игрок находится в коллайдере ловушки начинает наносить урон
                                      // Благодаря методу Update позволяет получать урон не один раз а до тех пор пока игрок стоит в колладере
        {
            player.TakeDamage(damage);
            player = null;
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true; // Ловушка запускается
        spriteRend.color = Color.blue;
        yield return new WaitForSeconds(activationDelay); // Ждёт определённоё время
        spriteRend.color = Color.white;
        active = true; // Урон включается
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activeTime); // Урон включается на определённое время
        active = false; // Урон выключается
        triggered = false; // Ловушка перестаёт работать
        anim.SetBool("activated", false);
    }
}
