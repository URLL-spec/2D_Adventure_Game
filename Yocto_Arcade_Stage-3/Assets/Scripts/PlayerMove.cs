using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    //Делать переменные публичными нужно чтобы их можно было изменять в Инспекторе
    public Rigidbody2D rb; //используется для манипуляции с физикой персонажа.
    public Animator anim; //используется для управления анимациями персонажа.


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //присваивание переменной rb ссылку на компонент Rigidbody2D
        anim = GetComponent<Animator>(); //присваивание переменной anim ссылку на компонент Animator
        checkRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }

    void Update()
    {
        walk();
        Reflect();
        Jump();
        Lunge();

    }

    private void FixedUpdate() //Нам нужна стабильность срабатывания, а благодаря этой функции она гарантирует вызов внутри себя раз в 0.02 секунды
                               //Поэтому функции Cheking находятся здесь
    {
        CheckingGround();
    }

    public Vector2 moveVector; //используется для манипуляций с координатами движения персонажа.
    public float speed = 2f; // переменная для настройки скорости персонажа  

    public void walk()
    {
        if (!blockMoveX)
        {
            moveVector.x = Input.GetAxis("Horizontal");                     // в нём присваеиваем x moveVector, вектор по горизонтальной оси Input.GetAxis("Horizontal")
            anim.SetFloat("MoveX", Mathf.Abs(moveVector.x));                // присваивание MoveX из Animator значение x из moveVector. Значение брать по модулю
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y); // обращаемся к компоненту RigidBody2d, к свойству velocity и задаём новый вектор
                                                                            // со считанным по горизонтом значением, а "y" оставляем неизменным

        }


        //rb.AddForce(moveVector * speed)   -   Это один из способов движения персонажа, но уже с использованием физики. Его использование зависит от того
        //какой отзывчивости вы хотите
    }


    public bool FaceRight = true;

    void Reflect() //отражает персонажа, если персонаж изменяет направление движения.
    {

        if (!blockMoveX)
        {
            if ((moveVector.x > 0 && !FaceRight) || (moveVector.x < 0 && FaceRight)) //Если вектор движения по оси x БОЛЬШЕ 0 (т.е. персонаж движется в правую сторону)
                                                                                     //и он НЕ СМОТРИТ в право
                                                                                     //ИЛИ 
                                                                                     //вектор движения по оси x МЕНЬШЕ 0 (т.е. персонаж движется в левую сторону)
            {
                transform.localScale *= new Vector2(-1, 1);                         //То тогда берём масштаб объекта и меняем значения по x на отрицательное 
                FaceRight = !FaceRight;                                              //и меняем значение FaceRight на противоположное
            }
        }
    }

    public float jumpforce = 7f;
    public float timedown = 0.20f; //Время в которое персонаж становится неосязаемым для платформ 

    private bool lockdown = false; // для задержки чтобы не спамить конпку 
    void DownLock() // для задержки чтобы не спамить конпку 
    {
        lockdown = false;
    }

    void Jump()
    {

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !lockdown)
        {
            lockdown = true; // для задержки чтобы не спамить конпку 
            Invoke("DownLock", 0.2f); // для задержки чтобы не спамить конпку 
            Physics2D.IgnoreLayerCollision(7, 8, true); // Обращение к компоненту Physics2D к методу Игнорировании столкновения слоёв. Указываем что для 7 и 8 слоя
                                                        // его надо включить true
            Invoke("IgnoreLayerOFF", timedown);         //Invoke - метод отложенного запуска функции. Задаём ему указание что некую фу-ю IgnoreLayerOFF
                                                        //нужно запустить через x секунд
        }


        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && OnGround && !lockjump) //Если нажат space и персонаж стоит на земле тогда импульс направленный вверх умноженный на силу прыжка
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpforce);               //Один из способов, но с ипользование velocity т.е. манипуляцией физикой напрямую

            //Разница в том что velocity задаёт объекту конечную скорость, а с AddForce 
            //конечную скорость рассчитывает сам Unity учитывая всё (массу, гравитацию, трение)
            //т.е. AddForce является более ресурсозатратным, но с использование его в дальнейшей 
            //разработке будет меньше проблем (наприме рассчитывание попаданий стрел в персонажа)
            anim.StopPlayback();
            rb.velocity = new Vector2(rb.velocity.x, 0);                         //Обнуление вектора ускорения персонажа
            rb.AddForce(Vector2.up * jumpforce);                                 //Импульс направленный вверх умноженный на силу прыжка
        }
    }

    void IgnoreLayerOFF() // Создание метода IgnoreLayerOFF для Jump
    {
        Physics2D.IgnoreLayerCollision(7, 8, false); // для 7 и 8 слоёв отключаем игнорирование столкновений (false - означает что отключено)
    }


    //Исправление мультипрыжка

    //Существовала проблема с прилипанием к стенке при зажатой кнопке, но была исправлена благодаря Physics Material 2D с значением Friction = 0

    public bool OnGround { get; private set; } // нужна для определения находится ли персонаж на земле
    public Transform GroundCheck; // в ней будет хранится ссылка на созднанный объект Ground Check
    private float checkRadius = 0.5f;
    public LayerMask Ground; // для хранения слоя которой будет считаться землёй 

    void CheckingGround() // Проверяет наличие земли
    {
        OnGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground); // Переменной OnGround присвается истина или ложь если круглый коллайдер будет пересекать
                                                                                       // слой который указан в переменной Ground
        anim.SetBool("OnGround", OnGround);
    }

    public bool blockMoveX;
    public Transform DopPosition; // Переменные для LedgeGoAuto
    public float dopRadius = 0.04f;

    private void OnDrawGizmos() // Отрисовка направляющей 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(DopPosition.position, dopRadius);
    }


    void LedgeGoAuto() // Автозалаз. Координаты куда будет телепортирован игрок после взятия за уступ
    {
        transform.position = new Vector3(DopPosition.position.x, DopPosition.position.y, transform.position.z);
    }

    public void StartAnimLeadge() // Старт анимации автозалаза
    {
        lockjump = true;
        Invoke("JumpLock", 0.25f);
        blockMoveX = true;
        rb.velocity = Vector2.zero;
        anim.Play("ledgeClimbPlatform");
    }

    public int LungeImpulse = 5000;
    public void Lunge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !locklunge && ((moveVector.x > 0.35) || (moveVector.x < -0.35))) // Если нажата кнопка И кувырок не заблокирован И скорость движения игрока больше или меньше +-0.35
        {
            lockjump = true; // Во время кувырка прыжок заблокирован
            Invoke("JumpLock", 0.45f); // Время спустя которое можно будет прыгать, чтобы не было некрасивого перехода между анимации 
            locklunge = true; // Блокирование повторно нажатия кувырка 
            Invoke("LungeLock", 0.65f); // Время спустя которое снова можно нажать кнопку, чтобы сделать кувырок
            anim.StopPlayback(); // Остановка всех передыдущих анимации
            anim.Play("lunge"); // Проиграть анимацию кувырка
            rb.velocity = new Vector2(0, 0); // Отключение внешних действующих сил
            if (!FaceRight) { rb.AddForce(Vector2.left * LungeImpulse); } // Если персонаж повёрнут влево тогда кувыркнуться влево
            else { rb.AddForce(Vector2.right * LungeImpulse); } // Иначе вправо
            Physics2D.IgnoreLayerCollision(7, 13, true); // Игнорирование слоя ловушек
            Physics2D.IgnoreLayerCollision(7, 11, true); // Игнорирования слоя врагов 
            Invoke("IgnoreLayerTraps", 0.7f); // Время сколько будет действовать кувырок

        }
    }
    void IgnoreLayerTraps() // Создание метода IgnoreLayerTraps для Lunge
    {
        Physics2D.IgnoreLayerCollision(7, 13, false); // для 7 и 13 слоёв отключаем игнорирование столкновений (false - означает что отключено)
        Physics2D.IgnoreLayerCollision(7, 11, false);
    }

    private bool locklunge = false;
    void LungeLock() // Метод для задержки кувырка (Чтобы игрок не спамил кнопку)
    {
        locklunge = false;
    }

    public bool lockjump = false;
    void JumpLock() // Метод для грамотного перехода между анимацией кувырка и прыжка
    {
        lockjump = false;
    }

    public void StartAnimDialoge() // Метод для старта анимации idle при начале диалога 
    {
        anim.SetBool("startdialog", true);
    }


}