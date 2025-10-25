using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Dialog_Frontend_Controller : MonoBehaviour
{


    //由于Dialog_Frontend_Controller下方存在需要互动的按钮，所以这个脚本用于承载需要按下按钮触发功能的函数

    public GameObject Global_Controller_Object;
    public Global_Controller Global_Controller_Component;

    public Component Current_Level_Controller;//当前的关卡控制器
    public void Dialog_Button_Click()
    {
        //直接寻找Canvas刷新当前状态
        gameObject.transform.parent.gameObject.GetComponent<Dialog_Player>().Dialog_Button_Click();



    }


    // Start is called before the first frame update
    void Start()
    {
        


        /*
         * 
         * 
         Global_Controller_Object = GameObject.Find("Global_Controller");
         Global_Controller_Component = Global_Controller_Object.GetComponent<Global_Controller>();

        GameObject Current_Canvas = gameObject.transform.parent.gameObject;//主管当前预制件的Canvas
        switch (Global_Controller_Component.Level_Select_Num)
        {
            case 1:
                Current_Level_Controller = Current_Canvas.GetComponent<Level_1_Controller>();
                break;
            case -1:
                //收藏室
                Current_Level_Controller = Current_Canvas.GetComponent<Collect_Controller>();
                break;
            case 2:
                //第二关
                Current_Level_Controller = Current_Canvas.GetComponent<Level_2_Controller>();
                break;
        }
         */
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
