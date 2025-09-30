using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Controller : MonoBehaviour
{


    public GameObject Current_Character;


    public Transform Current_Transform ;

    public Vector3 Post_Position;

    public bool Effective_Move;//�жϵ�ǰ�ƶ��Ƿ���Ч

    public bool Gravity_On;//�Ƿ�ʹ������
    // Start is called before the first frame update
    void Start()
    {

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

        if(Effective_Move==false)
        {
            Current_Transform.position = Post_Position;
            
            return;
        }

        Post_Position = new Vector3(Current_Transform.position.x, Current_Transform.position.y, Current_Transform.position.z);



        if (Input.GetKey(KeyCode.W))
        {

            float New_X = Current_Transform.position.x;
            float New_Y = Current_Transform.position.y+0.05f;
            float New_Z = Current_Transform.position.z;

            Current_Transform.position = new Vector3(New_X, New_Y,New_Z);
        }

        if (Input.GetKey(KeyCode.S))
        {

            float New_X = Current_Transform.position.x;
            float New_Y = Current_Transform.position.y - 0.05f;
            float New_Z = Current_Transform.position.z;

            Current_Transform.position = new Vector3(New_X, New_Y, New_Z);
        }

        if (Input.GetKey(KeyCode.A))
        {

            float New_X = Current_Transform.position.x- 0.05f;
            float New_Y = Current_Transform.position.y ;
            float New_Z = Current_Transform.position.z;

            Current_Transform.position = new Vector3(New_X, New_Y, New_Z);
        }

        if (Input.GetKey(KeyCode.D))
        {

            float New_X = Current_Transform.position.x + 0.05f;
            float New_Y = Current_Transform.position.y;
            float New_Z = Current_Transform.position.z;

            Current_Transform.position = new Vector3(New_X, New_Y, New_Z);
        }

        

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"�������������� {other.gameObject.name} ��ײ");
        Effective_Move = false;
    }

    public void Shift_Mode()
    {

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(Gravity_On==false)
            {
                Gravity_On = true;
                rb.gravityScale = 1f;
            }
            else
            {
                Gravity_On = false;
                rb.gravityScale = 0f;
            }
        }
    }

}
