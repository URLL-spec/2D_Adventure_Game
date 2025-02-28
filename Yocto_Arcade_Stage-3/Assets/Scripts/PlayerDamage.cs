using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float damageEnemy; //damageEnemy отвечает за количество урона, который может нанести персонаж.
    public Rigidbody2D CharacterRb; // Переменная для манимуляции с движениями игрока 
    public BoxCollider2D box; // Пространство в котором будет считатся урон
    public Animator anim; // Анимации
    public PlayerMove move; // Переменная для манимуляции с движениями игрока 

    [SerializeField] private AudioSource hitWolfSoundEffect;
    [SerializeField] private AudioSource hitBatSoundEffect;

    private void Start()
    {
        box.enabled= false; // Со старта отключаем урон
    }

    private void Update()
    {
        BoxActive(); // Каждый кадр будет возможность задействовать функцию
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Тэг, который прикреплён к объекту, будет наносить урон если он будет перескатся с другими ниже написанными тэгами
        BoxActive();
        if (collision.tag == "EnemyDamage")
            {
                collision.GetComponent<HealthEnemy>().TakeDamageEnemy(damageEnemy); // Просто функция, которая включает урон
                hitWolfSoundEffect.Play();
            }
        else if (collision.tag == "EnemyDamageBat")
            {
                collision.GetComponent<HealthEnemyBat>().TakeDamageEnemy(damageEnemy);
                hitBatSoundEffect.Play();
            }
    }

    void BoxActive() //Смысл этой функции заключается в адаптации функции нанесения урона т.е. чтобы во время удара персонаж не мог двигатся или прыгать и т.д.
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X)) && !lockattack && !move.lockjump)
        {
            lockattack = true;
            move.enabled = false;
            anim.SetTrigger("attackrandom");
            anim.SetInteger("attackid", Random.Range(0, 2)); // Нужно для рандомных анимации +1 к общему количеству 
            if (move.OnGround) // Если персонаж на земле тогда пускай стоит на месте
            {
                CharacterRb.velocity = new Vector2(0, 0);
            }
            Invoke("LockAttack", 0.3f); // Откат атаки
            Invoke("DamageTimeON", 0.1f); // Смысл метода заключается в включении boxcollider2d при нажатии кнопки мыши и последующего отключения
        }
    }

    void DamageTimeON()
    {
        box.enabled = true;
        Invoke("DamageTimeOFF", 0.1f); // Время спустя которое урон выключится
    }

    public bool lockattack = false;
    void LockAttack() // Метод для задержки атаки (Чтобы игрок не спамил кнопку)
    {
        lockattack = false;
    }
    void DamageTimeOFF()
    {
        move.enabled = true;
        box.enabled = false;
    }

}
