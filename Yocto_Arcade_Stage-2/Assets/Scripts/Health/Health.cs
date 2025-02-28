using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth; // Частная переменная, которая будет определять сколько у вас ХП, когда вы начинаете игру. 
                                                   // [SerializeField]. Позволяет сериализовать переменные вне зависимости от их области видимости.
                                                   //                   Очень полезный атрибут, который позволяет сделать все переменные класса приватными, 
                                                   //                   но настраиваемыми в инспекторе.
    //[SerializeField] private PlayerMove Ground;
                     public float currentHealth { get; private set; }  // Текущее ХП игрока.

    // get означает, что можно получить переменную из любого другого скрипта
    // private set означает, что можно установить её только внутри ЭТОГО скрипта
    // Т.е. использовать переменную в других скриптах я могу, а изменять нет, только в этом скрипте.
    // Благодаря этому теперь значение ХП можно изменить только в методе TakeDamage
                     private Animator anim;
                     public bool dead;
                    

    public Rigidbody2D rb; // Переменная для манимуляции с движениями игрока 
    public GameObject time; // Переменная для метода AddHealth т.е. подбирания лечилок 
    public PlayerDamage playerdamage;
    void Start() // Нужно для остнановки после смерти
    {
        rb = GetComponent<Rigidbody2D>(); //присваивание переменной rb ссылку на компонент Rigidbody2D
    }
    private void Awake() // При старте делаем текущее здоровье равным исходному т.е. startingHealth
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }


    public void TakeDamage(float _damage) // Метод получения урона
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // Mathf для того чтобы ХП не опускалось ниже 0 или выше максимального значения т.е. startingHealth.  

        if (currentHealth > 0) // условие проверки урона 
        {
            // игрок получил ранение
            anim.SetTrigger("hurt");
            StartCoroutine(timeObj1());
            GetComponent<PlayerMove>().enabled = false;
            rb.velocity = new Vector2(7, 7); // При получении урона персонаж отскакивает
            Physics2D.IgnoreLayerCollision(7, 13, true);// Обращение к компоненту Physics2D к методу Игнорировании столкновения слоёв. Указываем что для 7 и 13 слоя
                                                        // его надо включить true
            Physics2D.IgnoreLayerCollision(7, 11, true);
            Invoke("InvulTraps", 1.5f);               //Invoke - метод отложенного запуска функции. Задаём ему указание что некую фу-ю Invul
                                                      //нужно запустить через x секунд
            Invoke("InvulEnemy", 1.5f);
            IEnumerator timeObj1() // Метод где описывается корутин
            {
                yield return new WaitForSeconds(0.25f); // Время спустя которое игроку возобновится управление. Длина анимации = Времени установки т.е. 0.21f
                GetComponent<PlayerMove>().enabled = true; // Включение управления игроку 
            }
    }
        else
        {
            if (!dead)
            {
                // игрок мёртв 
                anim.SetTrigger("die");
                GetComponent<PlayerMove>().enabled = false; // Отключаем возможность ходить когда игрок погиб.
                playerdamage.enabled = false;
                rb.velocity = new Vector2(0, 0); // Остнановка после смерти
                dead = true;
            }

        }

    }
    void InvulTraps() // Создание метода Invul для TakeDamage. Создаёт промежуток времени в котор персонаж неуязвим 
    {
        Physics2D.IgnoreLayerCollision(7, 13, false); // для 7 и 13 слоёв отключаем игнорирование столкновений (false - означает что отключено)
    }

    void InvulEnemy() // Создание метода Invul для TakeDamage. Создаёт промежуток времени в котор персонаж неуязвим 
    {
        Physics2D.IgnoreLayerCollision(7, 11, false);
    }

    public void AddHealth(float _value) // Метод для скрипта HelthCollectible чтобы была возможность лечится
    {
        if (startingHealth == currentHealth) // Если начальное хп равно текущему т.е. повреждений нет тогда просто полечится БЕЗ анимации
        {
            GetComponent<HealthCollectible>().enabled = false; // Отключаем HealthCollectible
        }
        else
        {
            if (!GetComponent<PlayerMove>().OnGround) // Если персонаж НЕ стоит на земле тогда просто полечится БЕЗ анимации
            { 
                currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth); // Взаимодействе с HealthCollectible для возможности лечения 
            }
            else // Иначе т.е. если персонаж стоит на земле, тогда полечится обычным путём
            {
                anim.Play("takeheal");
                StartCoroutine(timeObj()); // Старт Корутина
                GetComponent<PlayerMove>().enabled = false; // Отключение управления игроку 
                rb.velocity = new Vector2(0, 0); // Отключение действующих сил на персонажа 
                currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth); // Взаимодействе с HealthCollectible для возможности лечения 

                IEnumerator timeObj() // Метод где описывается корутин
                {
                    yield return new WaitForSeconds(0.21f); // Время спустя которое игроку возобновится управление. Длина анимации = Времени установки т.е. 0.21f
                    GetComponent<PlayerMove>().enabled = true; // Включение управления игроку 
                }
            }
        }
    }

    // Корутины (Coroutines, сопрограммы) в Unity — простой и удобный способ запускать функции, которые должны работать параллельно в течение некоторого времени.
}
