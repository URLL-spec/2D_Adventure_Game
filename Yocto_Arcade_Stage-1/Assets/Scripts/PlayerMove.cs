using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    //Делать переменные публичными нужно чтобы их можно было изменять в Инспекторе
    public Rigidbody2D rb;
    public Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //присваивание переменной rb ссылку на компонент Rigidbody2D
        anim = GetComponent<Animator>(); //присваивание переменной anim ссылку на компонент Animator
        checkRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
        //WallcheckRadiusDOWN = WallCheckDOWN.GetComponent<CircleCollider2D>().radius;
        //gravityDef = rb.gravityScale;
    }

    void Update()
    {
        walk();
        Reflect();
        Jump();
        //MoveOnWall();
        //WallJump();
        //LedgeGo();
    }

    private void FixedUpdate() //Нам нужна стабильность срабатывания, а благодаря этой функции она гарантирует вызов внутри себя раз в 0.02 секунды
                               //Поэтому функции Cheking находятся здесь
    {
        CheckingGround();
        //CheckingWall();
        //ChekingLeadge();
    }

    public Vector2 moveVector;
    public float speed = 2f; // переменная для настройки скорости персонажа  

    void walk()
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

    void Reflect()
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
    public float timedown = 0.15f;
    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.S)))
        {
            Physics2D.IgnoreLayerCollision(7, 8, true); // Обращение к компоненту Physics2D к методу Игнорировании столкновения слоёв. Указываем что для 7 и 8 слоя
                                                        // его надо включить true
            Invoke("IgnoreLayerOFF", timedown);         //Invoke - метод отложенного запуска функции. Задаём ему указание что некую фу-ю IgnoreLayerOFF
                                                        //нужно запустить через x секунд
        }


        if (Input.GetKeyDown(KeyCode.Space) && OnGround) //Если нажат space и персонаж стоит на земле тогда импульс направленный вверх умноженный на силу прыжка
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpforce);               //Один из способов, но с ипользование velocity т.е. манипуляцией физикой напрямую

            //Разница в том что velocity задаёт объекту конечную скорость, а с AddForce 
            //конечную скорость рассчитывает сам Unity учитывая всё (массу, гравитацию, трение)
            //т.е. AddForce является более ресурсозатратным, но с использование его в дальнейшей 
            //разработке будет меньше проблем (наприме рассчитывание попаданий стрел в персонажа)

            rb.velocity = new Vector2(rb.velocity.x, 0);                         //Обнуление вектора ускорения персонажа
            rb.AddForce(Vector2.up * jumpforce);                                 //Импульс направленный вверх умноженный на силу прыжка
        }
    }

    void IgnoreLayerOFF() // Создание метода IgnoreLayerOFF для Jump
    {
        Physics2D.IgnoreLayerCollision(7, 8, false); // для 7 и 8 слоёв отключаем игнорирование столкновений 
    }


    //Исправление мультипрыжка

    //Существовала проблема с прилипанием к стенке при зажатой кнопке, но была исправлена благодаря Physics Material 2D с значением Friction = 0

    public bool OnGround; // нужна для определения находится ли персонаж на земле
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



    //public bool OnWall;
    //public bool OnWallUp;
    //public bool OnWallDown;
    //public LayerMask Wall;
    //public Transform WallCheckUP; // в ней будет хранится ссылка на созднанный объект Wall Check
    //public Transform WallCheckDOWN;
    //public float WallcheckRayDistance = 1f;
    //private float WallcheckRadiusDOWN;
    //public bool onLedge;
    //public float ledgeRayCorrectY = 0.5f;



    //void CheckingWall() // Проверяет наличие стены
    //{
    //    OnWallUp = Physics2D.Raycast(WallCheckUP.position, new Vector2(transform.localScale.x, 0), WallcheckRayDistance, Wall);
    //    OnWallDown = Physics2D.OverlapCircle(WallCheckDOWN.position, WallcheckRadiusDOWN, Wall);
    //    OnWall = (OnWallUp && OnWallDown);
    //    anim.SetBool("OnWall", OnWall);
    //}

    //void ChekingLeadge()
    //{
    //    if (OnWallUp)
    //    {
    //        onLedge = !Physics2D.Raycast
    //        (
    //            new Vector2(WallCheckUP.position.x, WallCheckUP.position.y + ledgeRayCorrectY),
    //            new Vector2(transform.localScale.x, 0),
    //            WallcheckRayDistance,
    //            Wall
    //        );
    //    }
    //    else { onLedge = false; }

    //    anim.SetBool("OnLeadge", onLedge);

    //    if (onLedge && Input.GetAxisRaw("Vertical") != -1) // Если мы на земле тогда вызов методы вычисления зазора между красным лучём и землёй, и его последующие корректировки
    //    {
    //        rb.gravityScale = 0; // Обнуление гравитации при запрыгивании на уступ сверху для избежания провалов
    //        rb.velocity = new Vector2(0, 0);
    //        offsetCalculateAndCorrect();
    //    }
    //}

    //public float minCorrectDistance = 0.01f; // Переменна которая прибавляется к transform.position.y - offsetY для избежания отсутствия флажка OnLedge
    //public float offsetY; //Переменная для хранения значения смещения по вертекали
    //void offsetCalculateAndCorrect() //Метод построения 3-го луча 
    //{
    //    offsetY = Physics2D.Raycast
    //    (
    //        new Vector2(WallCheckUP.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUP.position.y + ledgeRayCorrectY),
    //        Vector2.down,
    //        ledgeRayCorrectY,
    //        Ground
    //    ).distance;

    //    if (offsetY > minCorrectDistance * 1.5f) // Чтобы было красиво и снизить нагрузку на процессор добавляем условие
    //                                             // "Смещение персонажа будет происходить только тогда когда расстояние до земли больше в 1.5 раза"
    //                                             // И ещё это избавит от дрожаний анимации
    //        transform.position = new Vector3(transform.position.x, transform.position.y - offsetY + minCorrectDistance, transform.position.z); // Vector3 потому что используются 3 переменные, чтобы избежать проблем с осью z
    //}


    //void LedgeGo() // Прикручивание анимации подъёма Обычный залаз
    //{
    //    if (onLedge && Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        anim.Play("wallLeadgeClimb");
    //    }
    //}

    //public Transform FinishLeadgePosition;
    //void FinishLeadge() //Функция для перемещения персонажа по координатам после проигрывания анимацмм подъёма
    //{
    //    transform.position = new Vector3(FinishLeadgePosition.position.x, FinishLeadgePosition.position.y, FinishLeadgePosition.position.z);
    //}


    void LedgeGoAuto() // Автозалаз. Координаты куда будет телепортирован игрок после взятия за уступ
    {
        transform.position = new Vector3(DopPosition.position.x, DopPosition.position.y, transform.position.z);
    }


    public void StartAnimLeadge() // Старт анимации автозалаза
    {
        blockMoveX = true;
        rb.velocity = Vector2.zero;
        anim.Play("ledgeClimbPlatform");
    }



    //public float upDownSpeed = 4f;
    //public float slidespeed = 0;
    //private float gravityDef;
    //void MoveOnWall()
    //{
    //    if (OnWall && !OnGround)
    //    {
    //        moveVector.y = Input.GetAxisRaw("Vertical");
    //        anim.SetFloat("UpDown", moveVector.y);

    //        if(!blockMoveX && moveVector.y == 0)
    //        {
    //            rb.gravityScale = 0;
    //            rb.velocity = new Vector2(0, slidespeed);
    //        }

    //        if (moveVector.y > 0)
    //        {
    //            rb.velocity = new Vector2(rb.velocity.x, moveVector.y * upDownSpeed / 2);
    //        }
    //        else if (moveVector.y < 0)
    //        {
    //            rb.velocity = new Vector2(rb.velocity.x, moveVector.y * upDownSpeed);
    //        }

    //    }
    //    else if (!OnGround && !OnWall) { rb.gravityScale = gravityDef; }
    //}

    //private bool blockMoveX;
    //public float jumpWallTime = 0.5f;
    //private float timerJumpWall;
    //public Vector2 jumpAngle = new Vector2(3.5f, 10);
    //void WallJump()
    //{
    //    if (OnWall && !OnGround && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        blockMoveX = true;



    //        transform.localScale *= new Vector2(-1, 1);
    //        FaceRight = !FaceRight;

    //        rb.velocity = new Vector2(transform.localScale.x * jumpAngle.x, jumpAngle.y);
    //    }
    //    if (blockMoveX && (timerJumpWall += Time.deltaTime) >= jumpWallTime)
    //    {
    //        if (OnWall || OnGround || Input.GetAxisRaw("Horizontal") != 0)
    //        {
    //            blockMoveX = false;
    //            timerJumpWall = 0;
    //        }
            
    //    }
    //}

    //    private void OnDrawGizmos() // Отоброжение лучей 
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawLine(WallCheckUP.position, new Vector2(WallCheckUP.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUP.position.y));

    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine
    //            (
    //            new Vector2(WallCheckUP.position.x, WallCheckUP.position.y + ledgeRayCorrectY),
    //            new Vector2(WallCheckUP.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUP.position.y + ledgeRayCorrectY)
    //            );

    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine
    //        (
    //        new Vector2(WallCheckUP.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUP.position.y + ledgeRayCorrectY),
    //        new Vector2(WallCheckUP.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUP.position.y)
    //        );
    //    }

}