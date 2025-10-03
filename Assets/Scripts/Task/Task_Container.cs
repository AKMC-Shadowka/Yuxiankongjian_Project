using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Container : MonoBehaviour
{
    //这个类用于存储两样重要的东西：
    //一个是可用的任务
    //一个是对于游戏来讲总的任务线

    //总的任务线
    public Task_List Total_Task_List;

    //可供选择的任务列表
    public Task_List Available_Task_List;

    public List<Task> Get_Available_Task_List()
    {
        return Available_Task_List.Current_Task_List;
    }


}
