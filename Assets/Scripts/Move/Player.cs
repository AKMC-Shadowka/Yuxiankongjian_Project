using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Current_Character;


    public Transform Current_Transform;

    public Vector3 Post_Position;

    public bool Effective_Move;//�жϵ�ǰ�ƶ��Ƿ���Ч

    public bool Effective_W;
    public bool Effective_A;
    public bool Effective_S;
    public bool Effective_D;
    public bool Gravity_On;//�Ƿ�ʹ������

    // Start is called before the first frame update

    public float Accelerate = 0f;
    public float Speed = 0f;

    public bool On_Ground;//�Ƿ��е�����Ϊ֧��

    public float jumpForce = 0.1f;//��Ծϵ��
    public bool isJumping;
    public float jumpTimeCounter;

    private float Move_Speed=0.03f;//�ƶ��ٶ�ϵ��

    private float Gravity_Scale = 2.25f;//����ϵ��

    void Start()
    {
        Effective_W = true;
        Effective_A = true;
        Effective_S = true;
        Effective_D = true;
        Debug.Log("Start from Move_Controller.cs");
        Current_Character = gameObject;
        Current_Transform = Current_Character.GetComponent<Transform>();
        Post_Position = new Vector3(Current_Transform.position.x, Current_Transform.position.y, Current_Transform.position.z);


        Effective_Move = true;
        Gravity_On = false;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        CheckGrounded();
        if (Gravity_On)
        {
            Jump(rb);
        }
        Move(rb);
        Shift_Mode(rb);
        
    }

    public void Move(Rigidbody2D rb)
    {

        //Current_Transform.eulerAngles = new Vector3(0f, 0f, 0f);

        /*
        if(Effective_Move==false)
        {
            Current_Transform.position = Post_Position;
            
            return;
        }
        */
        Post_Position = new Vector2(Current_Transform.position.x, Current_Transform.position.y);

        Vector2 New_Pos=new Vector2(Post_Position.x,Post_Position.y);


        float New_X=0f;
        float New_Y=0f;

        if (!Gravity_On)
        {
            if (Input.GetKey(KeyCode.W) && Effective_W)
            {
                New_Y += Move_Speed;
            }

            if (Input.GetKey(KeyCode.S) && Effective_S)
            {
                New_Y -= Move_Speed;
            }
        }

        if (Input.GetKey(KeyCode.A) && Effective_A)
        {
            New_X -= Move_Speed;
        }

        if (Input.GetKey(KeyCode.D) && Effective_D)
        {
            New_X += Move_Speed;
        }
        New_Pos = new Vector2(rb.position.x + New_X, rb.position.y + New_Y);

        rb.position=new Vector2(New_Pos.x,New_Pos.y);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"�������������� {other.gameObject.name} ��ײ");

        
        //Effective_Move = false;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
      
    }

    public void Shift_Mode(Rigidbody2D rb)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!Gravity_On)
            {
                Gravity_On = true;
                rb.gravityScale =Gravity_Scale;

                
            }
            else
            {
                Gravity_On = false;
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, 0f);

               
            }
        }
    }

    void CheckGrounded()
    {
        // ����ʵ�ֵĵ������߼�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        if (hit.collider != null)
        {
            On_Ground = true;
        }
        else 
        { 
            On_Ground = false;
        }
    }

    void Jump(Rigidbody2D rb)
    {
        float jumpTime = 0.08f;//��Ծ�����������ʱ��
 
        // ������Ծ��
        if (Input.GetKeyDown(KeyCode.W) && On_Ground)
        {
            jumpTimeCounter = jumpTime;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // ��ס��Ծ��
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // �ɿ���Ծ��
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }
}
