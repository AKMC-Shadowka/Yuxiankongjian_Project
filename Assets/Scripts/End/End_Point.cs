using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Point : MonoBehaviour
{
    //����ҽ��뵽End_Point֮�󣬾���Ҫ���г�����ת�ƺ��ݳ�������Ŀǰ��Ҫ���������ݳ�����

    [Header("��ǰ�Ĺؿ�������")]
    public Level_Controller Current_Level_Controller;

    public void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("Enter End_Point");
        //������ͳ����˹ؿ��Ľ����¼���
        Current_Level_Controller.End_Event();
    }
}
