using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{

    //这个类用于控制各个UI的显示与否，主要函数是一个叫做UI_Refresh()的函数
    //挂载在Global场景下的Canvas下，可以调度其下所有的UI组件
    public Global_Controller Global_Controller_Component;

    public bool Task_Terminal_Show;

    public GameObject Exit_Button;
    public GameObject Main_Menu_Button;
    public GameObject Task_Button;
    public GameObject Task_Terminal;


    public void UI_Refresh()
    {
        //关于Exit_Button的显示问题
        Exit_Button_Refresh();

        //关于其他按钮的显示问题
        Main_Menu_Button_Refresh();

        //Task按钮的显示
        Task_Button_Refresh();

        //任务终端的显示
        Task_Terminal_Refresh();
    }

    private void Exit_Button_Refresh()
    {
        if(Global_Controller_Component.Current_Level_Num==0)
        {
            Exit_Button.SetActive(false);
        }
        else
        {
            Exit_Button.SetActive(true);
        }
    }

    private void Task_Button_Refresh()
    {
        if (
            Global_Controller_Component.Current_Level_Num == 0
            ||
            Task_Terminal_Show==true
            )
        {
            Task_Button.SetActive(false);
        }
        else
        {
            Task_Button.SetActive(true);
        }
    }

    private void Main_Menu_Button_Refresh()
    {
        if (Global_Controller_Component.Current_Level_Num != 0)
        {
            Main_Menu_Button.SetActive(false);
        }
        else
        {
            Main_Menu_Button.SetActive(true);
        }
    }

    private void Task_Terminal_Refresh()
    {
        Task_Terminal.SetActive(Task_Terminal_Show);
    }


    // Start is called before the first frame update
    void Start()
    {
        UI_Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
