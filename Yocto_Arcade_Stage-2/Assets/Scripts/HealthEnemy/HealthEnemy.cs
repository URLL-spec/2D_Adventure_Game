using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HealthEnemy : MonoBehaviour
{
    // Скрипт здоровья Волка на основе Health 

    [SerializeField] private float startingHealthEnemy; // Частная переменная, которая будет определять сколько у вас ХП, когда вы начинаете игру. 
                                                   // [SerializeField]. Позволяет сериализовать переменные вне зависимости от их области видимости.
                                                   //                   Очень полезный атрибут, который позволяет сделать все переменные класса приватными, 
                                                   //                   но настраиваемыми в инспекторе.
                                                   //[SerializeField] private PlayerMove Ground;
    public float currentHealthEnemy { get; private set; }  // Текущее ХП

    // get означает, что можно получить переменную из любого другого скрипта
    // private set означает, что можно установить её только внутри ЭТОГО скрипта
    // Т.е. использовать переменную в других скриптах я могу, а изменять нет, только в этом скрипте.
    // Благодаря этому теперь значение ХП можно изменить только в методе TakeDamage
    private Animator anim;
    public bool dead;


    public Rigidbody2D rb; // Переменная для манимуляции с движениями игрока 
    public AIPath aipoint; // Переменная для остановки волка при получении урона
    public Transform one; // Переменная положения волка в пространстве
    public Transform two; // Переменная положения игрока в пространстве
    public BoxCollider2D boxCollider2; // Переменная для отключения после смерти
    public Destination destination; // Переменная для отключения достижения пути        // Переменные для остановки после смерти
    public GameObject Obekt; // Переменная для оключения определённого объекта
    private UnityEngine.Object explosion;

    //public EnemyDamageWolf enemydamagewolf;

    void Start() // Нужно для остнановки после смерти
    {
        //rb = GetComponent<Rigidbody2D>(); //присваивание переменной rb ссылку на компонент Rigidbody2D
        //aipoint = GetComponent<AIPath>();
        explosion = Resources.Load("Explosion");
    }
    private void Awake() // При старте делаем текущее здоровье равным исходному т.е. startingHealth
    {
        currentHealthEnemy = startingHealthEnemy;
        anim = GetComponent<Animator>();
    }


    public void TakeDamageEnemy(float _damage) // Метод получения урона
    {
        currentHealthEnemy = Mathf.Clamp(currentHealthEnemy - _damage, 0, startingHealthEnemy); // Mathf для того чтобы ХП не опускалось ниже 0 или выше максимального значения т.е. startingHealth.  

        if (currentHealthEnemy > 0) // условие проверки урона 
        {
            // игрок получил ранение
            anim.SetTrigger("wolfhurt");
            StartCoroutine(timeObj1());
            aipoint.enabled = false;
            GameObject explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (one.transform.position.x > two.transform.position.x) // условия благодаря которым отбрасывание противника будет производится корректно
            {
                rb.velocity = new Vector2(24, 24);
            }    
            else if (one.transform.position.x < two.transform.position.x)
            {
                rb.velocity = new Vector2(-24, 24);
            }
            Invoke("InvulEnemy", 1.5f);
            IEnumerator timeObj1() // Метод где описывается корутин
            {
                yield return new WaitForSeconds(0.41f); // Время спустя которое игроку возобновится управление. Длина анимации = Времени установки т.е. 0.21f
                aipoint.enabled = true; // Включение управления игроку 
            }
        }
        else
        {
            if (!dead)
            {
                // игрок мёртв 
                dead = true;
                GameObject explosionRef = (GameObject)Instantiate(explosion);
                explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                anim.SetTrigger("wolfdeath");
                aipoint.enabled = false; // Отключаем возможность ходить когда противник погиб.
                //boxCollider.enabled = false;
                boxCollider2.enabled = false;
                destination.enabled = false;
                //boxDamageWolfCollider.enabled = false;
                //enemydamagewolf.enabled = false;
                Obekt.gameObject.SetActive(false);
                //gameObject.SetActive(false);
                //Physics2D.IgnoreLayerCollision(11, 7, true);
                rb.velocity = new Vector2(0, 0); // Остнановка после смерти
            }

        }

    }

    void InvulEnemy() // Создание метода Invul для TakeDamage. Создаёт промежуток времени в котор персонаж неуязвим 
    {
        Physics2D.IgnoreLayerCollision(11, 7, false);
    }


    // Корутины (Coroutines, сопрограммы) в Unity — простой и удобный способ запускать функции, которые должны работать параллельно в течение некоторого времени.
}
