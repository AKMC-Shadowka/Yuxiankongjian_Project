using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black_UI : MonoBehaviour
{

    //�������к�Ļ���ݵĽű�

    //͸����
    private float Alpha_Index;

    public Image Black_UI_Image;

    [Header("��ǰ�Ƿ���Ҫ���б���")]
    public bool Perform_On;

    [Header("��Ҫ���к�Ļ���ݵ�ʱ��")]
    public float Perform_Time;

    private float Current_Time;//��ǰ���ڽ��е�ʱ��





    void Start()
    {
        Alpha_Index = 0f;
        Color Current_Color = Black_UI_Image.color;

        Current_Color.a = Alpha_Index;

        Black_UI_Image.color = Current_Color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Perform_On==true)
        {
            Black_UI_Perform();
        }
    }

    //���ⲿ��ȡ�ݳ�
    public void Start_Perform()
    {
        Perform_On = true;
    }

    private void Black_UI_Perform()
    {
        if(Current_Time>=Perform_Time)
        {
            Perform_On = false;
            Current_Time = 0f;
            return;
        }

        //͸��������
        if(Current_Time<=Perform_Time/2)
        {
            Alpha_Index = -2f / Perform_Time * Current_Time + 1f;
        }
        else
        {
            Alpha_Index = 2f / Perform_Time * Current_Time - 1f;
        }

        Alpha_Index = 1 - Alpha_Index;

        //͸���ȸ�ֵ
        Color Current_Color = Black_UI_Image.color;

        Current_Color.a = Alpha_Index;

        Black_UI_Image.color = Current_Color;


        Current_Time += Time.deltaTime;
    }
}
