using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity_Fall : MonoBehaviour
{
    //活动摔倒，与坠亡区分开

    [Header("被剥夺控制权的时间")]
    public float Fall_Time;

    [Header("当前的玩家控制器")]
    public Player Current_Player;//当前的玩家控制器

    private float Current_Fall_Time=0f;

    private bool Fall_On;//是否开始剥夺玩家的控制权 
    
    public void Update()
    {
        if(Fall_On==false)
        {
            return;
        }

        Current_Fall_Time += Time.deltaTime;

        if(Current_Fall_Time>=Fall_Time)
        {
            Fall_On = false;
            Current_Player.Effective_Move = true;
            Current_Fall_Time = 0f;
        }


    }




    public void  Fall()
    {
        //当触发时就会锁定玩家的控制权限，同时调用黑屏处理
        Current_Player.Effective_Move = false;
        Fall_On = true;
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();
    }



}
