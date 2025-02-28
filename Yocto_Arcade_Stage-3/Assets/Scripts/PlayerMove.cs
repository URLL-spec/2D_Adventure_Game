using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    //������ ���������� ���������� ����� ����� �� ����� ���� �������� � ����������
    public Rigidbody2D rb; //������������ ��� ����������� � ������� ���������.
    public Animator anim; //������������ ��� ���������� ���������� ���������.


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //������������ ���������� rb ������ �� ��������� Rigidbody2D
        anim = GetComponent<Animator>(); //������������ ���������� anim ������ �� ��������� Animator
        checkRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }

    void Update()
    {
        walk();
        Reflect();
        Jump();
        Lunge();

    }

    private void FixedUpdate() //��� ����� ������������ ������������, � ��������� ���� ������� ��� ����������� ����� ������ ���� ��� � 0.02 �������
                               //������� ������� Cheking ��������� �����
    {
        CheckingGround();
    }

    public Vector2 moveVector; //������������ ��� ����������� � ������������ �������� ���������.
    public float speed = 2f; // ���������� ��� ��������� �������� ���������  

    public void walk()
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

    void Reflect() //�������� ���������, ���� �������� �������� ����������� ��������.
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
    public float timedown = 0.20f; //����� � ������� �������� ���������� ����������� ��� �������� 

    private bool lockdown = false; // ��� �������� ����� �� ������� ������ 
    void DownLock() // ��� �������� ����� �� ������� ������ 
    {
        lockdown = false;
    }

    void Jump()
    {

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !lockdown)
        {
            lockdown = true; // ��� �������� ����� �� ������� ������ 
            Invoke("DownLock", 0.2f); // ��� �������� ����� �� ������� ������ 
            Physics2D.IgnoreLayerCollision(7, 8, true); // ��������� � ���������� Physics2D � ������ ������������� ������������ ����. ��������� ��� ��� 7 � 8 ����
                                                        // ��� ���� �������� true
            Invoke("IgnoreLayerOFF", timedown);         //Invoke - ����� ����������� ������� �������. ����� ��� �������� ��� ����� ��-� IgnoreLayerOFF
                                                        //����� ��������� ����� x ������
        }


        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && OnGround && !lockjump) //���� ����� space � �������� ����� �� ����� ����� ������� ������������ ����� ���������� �� ���� ������
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpforce);               //���� �� ��������, �� � ������������ velocity �.�. ������������ ������� ��������

            //������� � ��� ��� velocity ����� ������� �������� ��������, � � AddForce 
            //�������� �������� ������������ ��� Unity �������� �� (�����, ����������, ������)
            //�.�. AddForce �������� ����� ����������������, �� � ������������� ��� � ���������� 
            //���������� ����� ������ ������� (������� ������������� ��������� ����� � ���������)
            anim.StopPlayback();
            rb.velocity = new Vector2(rb.velocity.x, 0);                         //��������� ������� ��������� ���������
            rb.AddForce(Vector2.up * jumpforce);                                 //������� ������������ ����� ���������� �� ���� ������
        }
    }

    void IgnoreLayerOFF() // �������� ������ IgnoreLayerOFF ��� Jump
    {
        Physics2D.IgnoreLayerCollision(7, 8, false); // ��� 7 � 8 ���� ��������� ������������� ������������ (false - �������� ��� ���������)
    }


    //����������� ������������

    //������������ �������� � ����������� � ������ ��� ������� ������, �� ���� ���������� ��������� Physics Material 2D � ��������� Friction = 0

    public bool OnGround { get; private set; } // ����� ��� ����������� ��������� �� �������� �� �����
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


    void LedgeGoAuto() // ���������. ���������� ���� ����� �������������� ����� ����� ������ �� �����
    {
        transform.position = new Vector3(DopPosition.position.x, DopPosition.position.y, transform.position.z);
    }

    public void StartAnimLeadge() // ����� �������� ����������
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && !locklunge && ((moveVector.x > 0.35) || (moveVector.x < -0.35))) // ���� ������ ������ � ������� �� ������������ � �������� �������� ������ ������ ��� ������ +-0.35
        {
            lockjump = true; // �� ����� ������� ������ ������������
            Invoke("JumpLock", 0.45f); // ����� ������ ������� ����� ����� �������, ����� �� ���� ����������� �������� ����� �������� 
            locklunge = true; // ������������ �������� ������� ������� 
            Invoke("LungeLock", 0.65f); // ����� ������ ������� ����� ����� ������ ������, ����� ������� �������
            anim.StopPlayback(); // ��������� ���� ����������� ��������
            anim.Play("lunge"); // ��������� �������� �������
            rb.velocity = new Vector2(0, 0); // ���������� ������� ����������� ���
            if (!FaceRight) { rb.AddForce(Vector2.left * LungeImpulse); } // ���� �������� ������� ����� ����� ������������ �����
            else { rb.AddForce(Vector2.right * LungeImpulse); } // ����� ������
            Physics2D.IgnoreLayerCollision(7, 13, true); // ������������� ���� �������
            Physics2D.IgnoreLayerCollision(7, 11, true); // ������������� ���� ������ 
            Invoke("IgnoreLayerTraps", 0.7f); // ����� ������� ����� ����������� �������

        }
    }
    void IgnoreLayerTraps() // �������� ������ IgnoreLayerTraps ��� Lunge
    {
        Physics2D.IgnoreLayerCollision(7, 13, false); // ��� 7 � 13 ���� ��������� ������������� ������������ (false - �������� ��� ���������)
        Physics2D.IgnoreLayerCollision(7, 11, false);
    }

    private bool locklunge = false;
    void LungeLock() // ����� ��� �������� ������� (����� ����� �� ������ ������)
    {
        locklunge = false;
    }

    public bool lockjump = false;
    void JumpLock() // ����� ��� ���������� �������� ����� ��������� ������� � ������
    {
        lockjump = false;
    }

    public void StartAnimDialoge() // ����� ��� ������ �������� idle ��� ������ ������� 
    {
        anim.SetBool("startdialog", true);
    }


}