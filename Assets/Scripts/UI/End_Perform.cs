using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Perform : MonoBehaviour
{
    //这是个用于掌管结局滚动的脚本，应该挂载在Canvas下方

    public float Start_Y;//Y值起点

    public float End_Y;//Y值终点

    public float Perform_Time;//表演时间

    public float Current_Time;//现在表演的时间

    public bool Start_Perform;//是否开始表演

    public GameObject End_Perform_UI;//装载结局用于表演的UI

    public Global_Controller Current_Global_Controller;//当前的全局控制器

    public void Start()
    {
        Current_Time = 0f;
    }

    public void Update()
    {
        if(Start_Perform==false)
        {
            return;
        }

        if(Current_Time>=Perform_Time)
        {
            Current_Time = 0f;
            Start_Perform = false;

            gameObject.GetComponent<UI_Controller>().End_Show = false;
            gameObject.GetComponent<UI_Controller>().UI_Refresh();
            return;
        }

        End_Start_Perform();
    }

    public void Start_Performance()
    {
        Start_Perform = true;
    }

    public void End_Start_Perform()
    {
        //在这里面进行位置的移动操作
        Current_Time += Time.deltaTime;

        float Current_Y = Start_Y + (End_Y - Start_Y) * Current_Time / Perform_Time;

        //应该寻找准备表演的下属，下面应该询问Global_Controller应该播放哪个结局
        int End_Num = Current_Global_Controller.Calculate_End();

        GameObject Perform_Object = End_Perform_UI.transform.GetChild(End_Num).gameObject;

        //然后去调整Object的Y值

        float New_X = Perform_Object.GetComponent<RectTransform>().position.x;


        Perform_Object.GetComponent<RectTransform>().position = new Vector2(New_X,Current_Y);


    }

}
