using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Controller : MonoBehaviour
{

    //这个脚本是Scriptable_Object,用来惯例每一个关卡的任务，一般来讲对于每一个关卡的Level_Controller，都会有一个相应的Task_Controller与之对应
    //用于处理所有与任务相关的内容，关于任务的开放，获取信息，完成，刷新完成情况，显示，完成等等都会在这个类当中进行
    //如果说我能在这里用注释把所有该标出来的函数都标出来，那倒也不是很坏

    public Task_List C_Task_List;//当前Task_Controller所掌管的Task_List

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
