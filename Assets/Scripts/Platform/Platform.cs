using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;




public class Platform : MonoBehaviour
{
    //这个类用来作为电梯操作，按理来讲应该成为预制件的标配，先不急，等整合之后再说

    [Header("高度数量")]
    public int Platform_Num;//一共有几个档次的高度可供选择，就像电梯一样

    [Header("标准高度")]
    [SerializeField]
    public List<float> Standard_Height;//可供选择的标准高度


    public int Current_Platform_Num;//如果Platform_Num=3，那么Current_Platform_Num的值可以为0-2


    public float Change_Time;//从某一层到另一层的变化时间

    private float Current_Change_Time = 0f;//已经进行的时间


    public bool Updown_Permission=false;//是否允许升降

    public bool Is_Moving = false;//是否正在升降

    private GameObject Current_Player;//当前需要被移动的GameObject

    private float Current_Height = 0f;//当前高度

    private float Goal_Height = 0f;//目标高度

    public void Update()
    {
        Updown_Detect();

        Updown_Move();
    }
    

    //关于平台当前是否有机会进行上下挡位的移动
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>()!=null)
        {
            Updown_Permission = true;
            Current_Player = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Updown_Permission = false;
            Current_Player = null;
        }
    }



    public void Updown_Detect()
    {

        Rigidbody2D rb1 = gameObject.GetComponent<Rigidbody2D>();
        //如果没有权限，或者正在上下，那就什么都不做
        if (Updown_Permission==false||Is_Moving==true)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(Current_Platform_Num>=Platform_Num-1)
            {
                return;
            }
            Debug.Log("Up Permission");
            //进行上升操作

            Is_Moving = true;

            

            Current_Height = rb1.position.y;
            Goal_Height = Standard_Height[Current_Platform_Num + 1];
            Current_Platform_Num += 1;


            Debug.Log("Current_Height=" + Current_Height + " Goal= " + Goal_Height);


        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(Current_Platform_Num<=0)
            {
                return;
            }

            //进行下降操作
            Is_Moving = true;

            Current_Height = rb1.position.y;
            Goal_Height = Standard_Height[Current_Platform_Num -1];

            Current_Platform_Num -= 1;
            
        }

    }

    public void Updown_Move()
    {
        //只有在Updown的时候才会进行执行
        if(Is_Moving==false)
        {
            return;
        }

        if(Current_Change_Time>=Change_Time)
        {
            Is_Moving = false;
            Current_Change_Time = 0f;
            return;
        }

        Debug.Log("Enter Updown_Move()");
        //这个时候要考虑时间，移动功能，采用一个正弦的移动，当前高度，目标高度，以及怎么怎么样
        //首先获取当前高度和需要移动到的高度
        float Height_1 = Get_Height(Current_Change_Time / Change_Time);
        
        Current_Change_Time += Time.deltaTime;

        if(Current_Change_Time>=Change_Time)
        {
            Current_Change_Time = Change_Time;
        }

        float Height_2 = Get_Height((Current_Change_Time) / Change_Time);



        Rigidbody2D rb1 = gameObject.GetComponent<Rigidbody2D>();
        rb1.position = new Vector2(rb1.position.x, rb1.position.y + (Height_2 - Height_1));


        if (Updown_Permission==true)
        {
            Rigidbody2D rb2 = Current_Player.GetComponent<Rigidbody2D>();
            rb2.position = new Vector2(rb2.position.x, rb2.position.y + (Height_2 - Height_1));
        }

    }

    public float Get_Height(float index)
    {
        //那么我们默认index在0-1区间，然后取得高度
        float a = Current_Height;
        float b = Goal_Height;
        float result = (b - a) / 2 * Mathf.Sin(Mathf.PI * index - Mathf.PI * 0.5f) + (a + b) / 2;

        return result;

    }


}
