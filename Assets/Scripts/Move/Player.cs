using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Current_Character;


    public Transform Current_Transform;

    public Vector3 Post_Position;

    public bool Effective_Move;//判断当前移动是否有效



    public bool Effective_W;
    public bool Effective_A;
    public bool Effective_S;
    public bool Effective_D;
    public bool Gravity_On;//是否使用重力

    // Start is called before the first frame update

    public bool On_Ground;//是否有地面作为支持

    public float jumpForce;//跳跃系数
    public bool isJumping;
    public float jumpTimeCounter;

    private float Move_Speed=0.1f;//移动速度系数

    private float Gravity_Scale = 2.25f;//重力系数

    private float jumpTime = 0.5f;//跳跃按键持续最大时间

    public Vector2 Saved_Position;

    public bool Dead;

    public bool Gravity_Lock;//是否处于只能以重力状态行走的区域

    void Start()
    {

        Saved_Position = gameObject.transform.position;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
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

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public float Current_Time=0f;

    public void Calculate_Effective_Move()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject death = canvas.transform.GetChild(8).gameObject;
        //Debug.Log("Death.name=" + death.name);
        GameObject dialog = canvas.transform.GetChild(12).gameObject;
        //Debug.Log("dialog.name=" + dialog.name);
        GameObject choice = canvas.transform.GetChild(13).gameObject;
        if (death.activeInHierarchy)
        {
            Effective_Move = false;
            return;
        }
        if(dialog.activeInHierarchy)
        {
            Effective_Move = false;
            return;
        }

        if(canvas.GetComponent<Black_UI>().Perform_On==true)
        {
            Effective_Move = false;
            return;
        }

        if(choice.activeInHierarchy)
        {
            Effective_Move = false;
            return;
        }

        Effective_Move = true;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        Calculate_Effective_Move();
        //Debug.Log("Current_Time="+Current_Time+"Effective_Move=" + Effective_Move);

        // 强制编辑器刷新
#if UNITY_EDITOR
    UnityEditor.EditorUtility.SetDirty(this);
#endif

        Current_Time += Time.deltaTime;
        //Debug.Log("Time= " + Current_Time + " s, Effective_Move=" + Effective_Move);



        if(Effective_Move==false)
        {
            return;
        }
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

        New_X *= Time.deltaTime * 60f*2f;
        New_Y *= Time.deltaTime * 60f*2f;//速度太慢了，在这里*2吧
        New_Pos = new Vector2(rb.position.x + New_X, rb.position.y + New_Y);

        rb.position=new Vector2(New_Pos.x,New_Pos.y);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"触发器触发！与 {other.gameObject.name} 碰撞");

        
        //Effective_Move = false;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
      
    }

    public void Shift_Mode(Rigidbody2D rb)
    {

        //此处是剧情需要的判定
        if (
            GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num == 2
            &&
            GameObject.Find("Level_Controller").GetComponent<Level_2_Controller>().Shift_On == true
            &&
            Input.GetKeyDown(KeyCode.F)
            )
        {
            Level_2_Shift();
        }
        
        //剩下的是ShiftMode的功能
        if(On_Ground==false&&Gravity_On==true)
        {
            return;
        }

        if(Gravity_Lock==true&&Gravity_On==true)
        {
            return;
        }

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

    public void Level_2_Shift()
    {
        GameObject.Find("Level_Controller").GetComponent<Level_2_Controller>().Shift_Dialog.Start_Dialog();
        GameObject.Find("Level_Controller").GetComponent<Level_2_Controller>().Shift_Dialog_Over = true;
    }

    void CheckGrounded()
    {
        // 这里实现的地面检测逻辑
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 2f);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                // 遍历每一个被击中的物体
                //Debug.Log($"击中了物体：{hit.collider.gameObject.name}，距离是：{hit.distance}");
                if (hit.collider.gameObject == gameObject)
                {
                    continue;
                }
                if(hit.collider.isTrigger==false)
                {
                    On_Ground = true;
                    return;
                }
                
                
            }
        }
        On_Ground = false;
    }

    void Jump(Rigidbody2D rb)
    { 
        // 按下跳跃键
        if (Input.GetKeyDown(KeyCode.W) && On_Ground)
        {
            jumpTimeCounter = jumpTime;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // 按住跳跃键
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

        // 松开跳跃键
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }

    public void Death_Event()
    {
        Debug.Log("Player Is Dead");
        //重力也要锁
        Dead = true;
        Gravity_On = false;
        Effective_Move = false;
        
        UI_Controller UI_C = GameObject.Find("Canvas").GetComponent<UI_Controller>();
        UI_C.Blood_Show = false;
        UI_C.Chase_Show = false;
        UI_C.UI_Refresh();

        
        GameObject.Find("Canvas").GetComponent<UI_Controller>().Set_Death_Show(true);

        
    }

    public void Set_Dead()
    {
        Dead = true;
    }

}
