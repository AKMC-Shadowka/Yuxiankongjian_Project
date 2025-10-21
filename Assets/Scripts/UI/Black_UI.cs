using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black_UI : MonoBehaviour
{

    //用来进行黑幕表演的脚本

    //透明度
    private float Alpha_Index;

    public Image Black_UI_Image;

    [Header("当前是否需要进行表演")]
    public bool Perform_On;

    [Header("需要进行黑幕表演的时间")]
    public float Perform_Time;

    private float Current_Time;//当前正在进行的时间





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

    //供外部调取演出
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

        //透明度运算
        if(Current_Time<=Perform_Time/2)
        {
            Alpha_Index = -2f / Perform_Time * Current_Time + 1f;
        }
        else
        {
            Alpha_Index = 2f / Perform_Time * Current_Time - 1f;
        }

        Alpha_Index = 1 - Alpha_Index;

        //透明度赋值
        Color Current_Color = Black_UI_Image.color;

        Current_Color.a = Alpha_Index;

        Black_UI_Image.color = Current_Color;


        Current_Time += Time.deltaTime;
    }
}
