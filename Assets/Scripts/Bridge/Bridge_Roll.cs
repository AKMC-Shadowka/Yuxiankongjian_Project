using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_Roll : MonoBehaviour
{
    //Bridge Roll���ŵ�ҡ�ӣ��൱�ڸı��ŵ�λ�õ�С��ť


    [Header("��GameObject")]
    public GameObject Bridge;

    public GameObject Tips;



    [Header("��Ϸ����ر���")]
    public bool Roll_Permission;//ֻ���ڴ������ڲſ��Խ���Roll����

    public string Direction;//"X"��ʾ�ƶ�X����ţ���Y��ͬ��

    public float Max_Value;

    public float Min_Value;//���ƶ���������СX/Yֵ

    public float Move_Speed=0.01f;//����ŵ��ƶ��ٶ�

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
