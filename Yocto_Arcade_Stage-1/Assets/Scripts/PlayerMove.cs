using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    //������ ���������� ���������� ����� ����� �� ����� ���� �������� � ����������
    public Rigidbody2D rb;
    public Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //������������ ���������� rb ������ �� ��������� Rigidbody2D
        anim = GetComponent<Animator>(); //������������ ���������� anim ������ �� ��������� Animator
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

    private void FixedUpdate() //��� ����� ������������ ������������, � ��������� ���� ������� ��� ����������� ����� ������ ���� ��� � 0.02 �������
                               //������� ������� Cheking ��������� �����
    {
        CheckingGround();
        //CheckingWall();
        //ChekingLeadge();
    }

    public Vector2 moveVector;
    public float speed = 2f; // ���������� ��� ��������� �������� ���������  

    void walk()
    {
        if (!blockMoveX)
        {
            moveVector.x = Input.GetAxis("Horizontal");                     // � �� ������������ x moveVector, ������ �� �������������� ��� Input.GetAxis("Horizontal")
            anim.SetFloat("MoveX", Mathf.Abs(moveVector.x));                // ������������ MoveX �� Animator �������� x �� moveVector. �������� ����� �� ������
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y); // ���������� � ���������� RigidBody2d, � �������� velocity � ����� ����� ������
                                                                            // �� ��������� �� ���������� ���������, � "y" ��������� ����������
        }


    //rb.AddForce(moveVector * speed)   -   ��� ���� �� �������� �������� ���������, �� ��� � �������������� ������. ��� ������������� ������� �� ����
    //����� ������������ �� ������
}


    public bool FaceRight = true;

    void Reflect()
    {

        if (!blockMoveX)
        {
            if ((moveVector.x > 0 && !FaceRight) || (moveVector.x < 0 && FaceRight)) //���� ������ �������� �� ��� x ������ 0 (�.�. �������� �������� � ������ �������)
                                                                                     //� �� �� ������� � �����
                                                                                     //��� 
                                                                                     //������ �������� �� ��� x ������ 0 (�.�. �������� �������� � ����� �������)
            {
                transform.localScale *= new Vector2(-1, 1);                         //�� ����� ���� ������� ������� � ������ �������� �� x �� ������������� 
                FaceRight = !FaceRight;                                              //� ������ �������� FaceRight �� ���������������
            }
        }
    }

    public float jumpforce = 7f;
    public float timedown = 0.15f;
    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.S)))
        {
            Physics2D.IgnoreLayerCollision(7, 8, true); // ��������� � ���������� Physics2D � ������ ������������� ������������ ����. ��������� ��� ��� 7 � 8 ����
                                                        // ��� ���� �������� true
            Invoke("IgnoreLayerOFF", timedown);         //Invoke - ����� ����������� ������� �������. ����� ��� �������� ��� ����� ��-� IgnoreLayerOFF
                                                        //����� ��������� ����� x ������
        }


        if (Input.GetKeyDown(KeyCode.Space) && OnGround) //���� ����� space � �������� ����� �� ����� ����� ������� ������������ ����� ���������� �� ���� ������
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpforce);               //���� �� ��������, �� � ������������ velocity �.�. ������������ ������� ��������

            //������� � ��� ��� velocity ����� ������� �������� ��������, � � AddForce 
            //�������� �������� ������������ ��� Unity �������� �� (�����, ����������, ������)
            //�.�. AddForce �������� ����� ����������������, �� � ������������� ��� � ���������� 
            //���������� ����� ������ ������� (������� ������������� ��������� ����� � ���������)

            rb.velocity = new Vector2(rb.velocity.x, 0);                         //��������� ������� ��������� ���������
            rb.AddForce(Vector2.up * jumpforce);                                 //������� ������������ ����� ���������� �� ���� ������
        }
    }

    void IgnoreLayerOFF() // �������� ������ IgnoreLayerOFF ��� Jump
    {
        Physics2D.IgnoreLayerCollision(7, 8, false); // ��� 7 � 8 ���� ��������� ������������� ������������ 
    }


    //����������� ������������

    //������������ �������� � ����������� � ������ ��� ������� ������, �� ���� ���������� ��������� Physics Material 2D � ��������� Friction = 0

    public bool OnGround; // ����� ��� ����������� ��������� �� �������� �� �����
    public Transform GroundCheck; // � ��� ����� �������� ������ �� ���������� ������ Ground Check
    private float checkRadius = 0.5f;
    public LayerMask Ground; // ��� �������� ���� ������� ����� ��������� ����� 

    void CheckingGround() // ��������� ������� �����
    {
        OnGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground); // ���������� OnGround ���������� ������ ��� ���� ���� ������� ��������� ����� ����������
                                                                                       // ���� ������� ������ � ���������� Ground
        anim.SetBool("OnGround", OnGround);
    }

    public bool blockMoveX;
    public Transform DopPosition; // ���������� ��� LedgeGoAuto
    public float dopRadius = 0.04f;

    private void OnDrawGizmos() // ��������� ������������ 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(DopPosition.position, dopRadius);
    }



    //public bool OnWall;
    //public bool OnWallUp;
    //public bool OnWallDown;
    //public LayerMask Wall;
    //public Transform WallCheckUP; // � ��� ����� �������� ������ �� ���������� ������ Wall Check
    //public Transform WallCheckDOWN;
    //public float WallcheckRayDistance = 1f;
    //private float WallcheckRadiusDOWN;
    //public bool onLedge;
    //public float ledgeRayCorrectY = 0.5f;



    //void CheckingWall() // ��������� ������� �����
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

    //    if (onLedge && Input.GetAxisRaw("Vertical") != -1) // ���� �� �� ����� ����� ����� ������ ���������� ������ ����� ������� ����� � �����, � ��� ����������� �������������
    //    {
    //        rb.gravityScale = 0; // ��������� ���������� ��� ������������ �� ����� ������ ��� ��������� ��������
    //        rb.velocity = new Vector2(0, 0);
    //        offsetCalculateAndCorrect();
    //    }
    //}

    //public float minCorrectDistance = 0.01f; // ��������� ������� ������������ � transform.position.y - offsetY ��� ��������� ���������� ������ OnLedge
    //public float offsetY; //���������� ��� �������� �������� �������� �� ���������
    //void offsetCalculateAndCorrect() //����� ���������� 3-�� ���� 
    //{
    //    offsetY = Physics2D.Raycast
    //    (
    //        new Vector2(WallCheckUP.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUP.position.y + ledgeRayCorrectY),
    //        Vector2.down,
    //        ledgeRayCorrectY,
    //        Ground
    //    ).distance;

    //    if (offsetY > minCorrectDistance * 1.5f) // ����� ���� ������� � ������� �������� �� ��������� ��������� �������
    //                                             // "�������� ��������� ����� ����������� ������ ����� ����� ���������� �� ����� ������ � 1.5 ����"
    //                                             // � ��� ��� ������� �� �������� ��������
    //        transform.position = new Vector3(transform.position.x, transform.position.y - offsetY + minCorrectDistance, transform.position.z); // Vector3 ������ ��� ������������ 3 ����������, ����� �������� ������� � ���� z
    //}


    //void LedgeGo() // ������������� �������� ������� ������� �����
    //{
    //    if (onLedge && Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        anim.Play("wallLeadgeClimb");
    //    }
    //}

    //public Transform FinishLeadgePosition;
    //void FinishLeadge() //������� ��� ����������� ��������� �� ����������� ����� ������������ �������� �������
    //{
    //    transform.position = new Vector3(FinishLeadgePosition.position.x, FinishLeadgePosition.position.y, FinishLeadgePosition.position.z);
    //}


    void LedgeGoAuto() // ���������. ���������� ���� ����� �������������� ����� ����� ������ �� �����
    {
        transform.position = new Vector3(DopPosition.position.x, DopPosition.position.y, transform.position.z);
    }


    public void StartAnimLeadge() // ����� �������� ����������
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

    //    private void OnDrawGizmos() // ����������� ����� 
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