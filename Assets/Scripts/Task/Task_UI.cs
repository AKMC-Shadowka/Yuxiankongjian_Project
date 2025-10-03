using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Task_UI : MonoBehaviour
{
    //这个类是关于Task如何进行UI显示的，实际上对于UI来讲，可能出现的UI都应该独立于关卡，所以我要将所有的UI都放Global场景中，然后再考虑场景功能的问题。

    //UI控制器，From Canvas
    public UI_Controller UI_Controller_Component;


    //任务容纳器 From Task
    public Task_Container Task_Container_Component;

    public GameObject List_UI_Preset;//关于查看任务列表UI的预制件，用于在Task_List_Upadte中批量生成（Instantiate）

    private List<GameObject> Current_List_UI;//当前任务列表侧克隆出来的UI组件


    public void Start()
    {
        
    }




    //通用UI的任务按钮点击
    public void Task_Button_Click()
    {
        UI_Controller_Component.Task_Terminal_Show = true;

        UI_Controller_Component.UI_Refresh();

        Task_UI_Update();//本页面相关任务信息信息刷新


    }

    //任务UI界面内部的退出按钮更新
    public void Task_Exit_Button_Click()
    {
        UI_Controller_Component.Task_Terminal_Show = false;

        UI_Controller_Component.UI_Refresh();


        //记得清除之前生成的组件
        for(int i=0;i<Current_List_UI.Count;i++)
        {
            Destroy(Current_List_UI[i]);
            //Debug.Log("Destroy " + i);
        }
    }

    //关于打开界面后Task_UI是如何更新的
    public void Task_UI_Update()
    {

        //UI左侧的任务列表更新
        Task_List_Upadte();

        //任务的详细信息更新
        Task_Message_Update(0);

    }


    public void Task_List_Upadte()
    {
        List<Task> Current_Task_List = Task_Container_Component.Get_Available_Task_List();

        Current_List_UI = new List<GameObject>();


        for (int i=0 ; i< Current_Task_List.Count; i++)
        {
            GameObject List_Preset = Instantiate(List_UI_Preset);
            //新生成的预制件定位
            List_Preset.transform.SetParent(gameObject.transform.GetChild(0).GetChild(0).GetChild(0));

            Current_List_UI.Add(List_Preset);

            //信息赋予
            Text Task_Title = List_Preset.transform.GetChild(0).GetComponent<Text>();
            Task_Title.text = Current_Task_List[i].Task_Name;

            //Destroy(List_Preset);
        }
    }


    public void Task_Message_Update(int index)
    {
        List<Task> Current_Task_List = Task_Container_Component.Get_Available_Task_List();

        Text Task_Info = gameObject.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>();

        Task_Info.text = Current_Task_List[index].Task_Description;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
