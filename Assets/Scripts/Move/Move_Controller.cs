using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Controller : MonoBehaviour
{


    public GameObject Current_Character;


    public Transform Current_Transform ;

    public Vector3 Post_Position;

    public bool Effective_Move;//判断当前移动是否有效

    public bool Effective_W;
    public bool Effective_A;
    public bool Effective_S;
    public bool Effective_D;
    public bool Gravity_On;//是否使用重力
    // Start is called before the first frame update

    public float Accelerate = 0f;
    public float Speed = 0f;

    public bool On_Base;//是否有地面作为支持






    void Start()
    {
        Effective_W = true;
        Effective_A=true;
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
        Move();
        Shift_Mode();
    }

    public void Move()
    {

        Current_Transform.eulerAngles = new Vector3(0f, 0f, 0f);

        /*
        if(Effective_Move==false)
        {
            Current_Transform.position = Post_Position;
            
            return;
        }
        */
        Post_Position = new Vector3(Current_Transform.position.x, Current_Transform.position.y, Current_Transform.position.z);



        if (Input.GetKey(KeyCode.W)&&Effective_W)
        {

            float New_X = Current_Transform.position.x;
            float New_Y = Current_Transform.position.y+0.05f;
            float New_Z = Current_Transform.position.z;

            Current_Transform.position = new Vector3(New_X, New_Y,New_Z);
        }

        if (Input.GetKey(KeyCode.S)&&Effective_S)
        {

            float New_X = Current_Transform.position.x;
            float New_Y = Current_Transform.position.y - 0.05f;
            float New_Z = Current_Transform.position.z;

            Current_Transform.position = new Vector3(New_X, New_Y, New_Z);
        }

        if (Input.GetKey(KeyCode.A)&&Effective_A)
        {

            float New_X = Current_Transform.position.x- 0.05f;
            float New_Y = Current_Transform.position.y ;
            float New_Z = Current_Transform.position.z;

            Current_Transform.position = new Vector3(New_X, New_Y, New_Z);
        }

        if (Input.GetKey(KeyCode.D)&&Effective_D)
        {

            float New_X = Current_Transform.position.x + 0.05f;
            float New_Y = Current_Transform.position.y;
            float New_Z = Current_Transform.position.z;

            Current_Transform.position = new Vector3(New_X, New_Y, New_Z);
        }

        

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"触发器触发！与 {other.gameObject.name} 碰撞");

        if(Input.GetKey(KeyCode.W))
        {
            Effective_W = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Effective_A = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Effective_S = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Effective_D = false;
        }

        //Effective_Move = false;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(Effective_W==false)
        {
            Effective_W = true;
            //return;
        }

        if (Effective_A == false)
        {
            Effective_A = true;
            //return;
        }

        if (Effective_S == false)
        {
            Effective_S = true;
            //return;
        }

        if (Effective_D == false)
        {
            Effective_D = true;
            //return;
        }
    }

    public void Shift_Mode()
    {

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(rb.IsAwake())
            {
                rb.Sleep();
                //Debug.Log("物体已进入休眠状态");
                //Gravity_On = true;
                //rb.gravityScale = 1f;
            }
            else
            {
                rb.WakeUp();
                //Gravity_On = false;
                //rb.gravityScale = 0f;
            }
        }
    }

}
