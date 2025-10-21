using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Point : MonoBehaviour
{
    //当玩家进入到End_Point之后，就需要进行场景的转移和演出动画，目前主要做黑屏的演出动画

    [Header("当前的关卡控制器")]
    public Level_Controller Current_Level_Controller;

    public void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("Enter End_Point");
        //到这里就出发了关卡的结束事件了
        Current_Level_Controller.End_Event();
    }
}
