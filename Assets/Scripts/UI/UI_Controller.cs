using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    // 这个类用于控制各个UI的显示与否，主要函数是一个叫做UI_Refresh()的函数
    // 挂载在Global场景下的Canvas下，可以调度其下所有的UI组件
    public Global_Controller Global_Controller_Component;

    public bool Task_Terminal_Show;
    public bool Death_Show;
    public bool Bag_Show; // 统一管理背包显示状态

    public bool Blood_Show;

    public bool End_Show;//管理结束走马灯结局的脚本是否播放

    public GameObject Exit_Button;
    public GameObject Main_Menu_Button;
    public GameObject Task_Button;
    public GameObject Task_Terminal;
    public GameObject Bag_Button;
    public GameObject Death_UI;

    public GameObject Blood_UI;
    public GameObject End_UI;


    // === 背包控制器引用 ===
    [Header("背包控制器")]
    public Bag_Controller bagController;

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

        //死亡终端显示
        Death_UI_Refresh();

        //背包按钮显示
        Bag_Button_Refresh();

        //背包UI显示
        Bag_UI_Refresh();

        //血丝UI显示
        Blood_UI_Show();

        //结局走马灯UI显示
        End_UI_Show();
    }

    private void Exit_Button_Refresh()
    {
        if(Global_Controller_Component.Current_Level_Num==0
            ||
            Global_Controller_Component.Current_Level_Num == 6)
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
            ||
            Bag_Show // 使用统一的背包显示状态
            ||
            Global_Controller_Component.Current_Level_Num == 6
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

    private void Death_UI_Refresh()
    {
        Death_UI.SetActive(Death_Show);
    }

    // 背包按钮刷新方法
    private void Bag_Button_Refresh()
    {
        // 在关卡0或者背包已经打开时隐藏背包按钮
        if (Global_Controller_Component.Current_Level_Num == 0
            ||
            Bag_Show
            ||
            Global_Controller_Component.Current_Level_Num == 6)
        {
            Bag_Button.SetActive(false);
        }
        else
        {
            Bag_Button.SetActive(true);
        }
    }

    // 新增：背包UI刷新方法
    private void Bag_UI_Refresh()
    {
        if (bagController != null && bagController.Bag_UI != null)
        {
            bagController.Bag_UI.SetActive(Bag_Show);
            if (Bag_Show)
            {
                bagController.RefreshBagDisplay();
            }
            else
            {
                bagController.HideItemDetail(); // 关闭背包时隐藏详情面板
            }
        }
    }

    public void Set_Death_Show(bool B)
    {
        Death_Show = B;
        UI_Refresh();
    }

    // 设置背包显示状态的方法
    public void Set_Bag_Show(bool B)
    {
        Bag_Show = B;
        UI_Refresh();
    }

    // 切换背包显示状态（用于按钮点击）
    public void Toggle_Bag_Show()
    {
        Bag_Show = !Bag_Show;
        UI_Refresh();
    }

    // 关闭所有UI界面（用于游戏场景中）
    public void Close_All_UI()
    {
        Task_Terminal_Show = false;
        Bag_Show = false;
        UI_Refresh();
    }

    // 获取当前背包显示状态
    public bool Get_Bag_Show_State()
    {
        return Bag_Show;
    }

    /// 背包关闭按钮点击事件
    public void Bag_Close_Button_Click()
    {
        Set_Bag_Show(false);
    }

    public void Blood_UI_Show()
    {
        Blood_UI.SetActive(Blood_Show);
    }

    public void End_UI_Show()
    {
        End_UI.SetActive(End_Show);
    }


    // Start is called before the first frame update
    void Start()
    {
        // 初始化背包显示状态
        Bag_Show = false;
        UI_Refresh();
    }
}