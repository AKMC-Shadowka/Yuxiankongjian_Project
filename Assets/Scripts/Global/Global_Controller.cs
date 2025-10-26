using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Global_Controller : MonoBehaviour
{
    //这个类是老朋友了，全局控制器，用来储存当前的相关游戏状态，而且有极大的可能作为存档功能的基类

    public int Current_Level_Num;

    public void Start()
    {
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Additive);
    }


    //输出结局的序号
    public int Calculate_End()
    {
        //经过一通计算
        return 0;
    }
}
