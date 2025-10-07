using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_Roll : MonoBehaviour
{
    //Bridge Roll，桥的摇子，相当于改变桥的位置的小按钮


    [Header("绑定GameObject")]
    public GameObject Bridge;

    public GameObject Tips;



    [Header("游戏内相关表现")]
    public bool Roll_Permission;//只有在触发器内才可以进行Roll操作

    public string Direction;//"X"表示移动X轴的桥，“Y”同理

    public float Max_Value;

    public float Min_Value;//桥移动的最大和最小X/Y值

    public float Move_Speed=0.01f;//耄耋桥的移动速度

    public void Start()
    {
        Tips = gameObject.transform.GetChild(0).gameObject;

        Tips.SetActive(false);
        Roll_Permission = false;

    }

    public void FixedUpdate()
    {
        RollBridge();
    }

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        Tips.SetActive(true);
        Roll_Permission = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Tips.SetActive(false);
        Roll_Permission = false;
    }


    public void RollBridge()
    {

        if(!Roll_Permission)
        {
            return;
        }

        if(!Input.GetKey(KeyCode.UpArrow)&&!Input.GetKey(KeyCode.DownArrow))
        {
            return;
        }

        Vector2 Move_Vector = (Direction=="X") ? new Vector2(Move_Speed,0f) :new Vector2(0f,Move_Speed) ;

        Rigidbody2D rb = Bridge.GetComponent<Rigidbody2D>();

        float New_X=rb.position.x, New_Y=rb.position.y;
        

        if(Input.GetKey(KeyCode.UpArrow)&&Roll_Permission)
        {
            New_X = rb.position.x + Move_Vector.x;
            New_Y = rb.position.y + Move_Vector.y;

        }

        if (Input.GetKey(KeyCode.DownArrow) && Roll_Permission)
        {
            New_X = rb.position.x - Move_Vector.x;
            New_Y = rb.position.y - Move_Vector.y;
        }

        if (Direction == "X")
        {
            New_X =Mathf.Min(New_X, Max_Value);
            New_X = Mathf.Max(New_X, Min_Value);
        }
        else
        {
            New_Y = Mathf.Min(New_Y, Max_Value);
            New_Y = Mathf.Max(New_Y, Min_Value);
        }

        Debug.Log("Bridge.Position=("+ rb.position.x+ " , "+ rb.position.y+ ").");
        rb.position = new Vector2(New_X, New_Y);

    }

}
