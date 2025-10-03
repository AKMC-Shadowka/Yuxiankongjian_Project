using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Task", menuName = "Game/Task", order = 1)]
public class Task : ScriptableObject
{


    public int Task_ID;//任务ID

    public List<int> Task_Unlock_Condition;//任务解锁条件

    public string Task_Name;//任务名字

    [TextArea]
    public string Task_Description;//任务描述

    [TextArea]
    public List<string> Task_Requirements;//任务要求
    public List<bool> Task_Completed;//任务完成情况

    public bool Task_Finished;//当前任务是否已经完成

    public  bool Task_Unlock;//的当前任务是否解锁
    


}
