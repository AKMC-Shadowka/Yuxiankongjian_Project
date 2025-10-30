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


        switch (Current_Level_Num)
        {
            case 0:
                SceneManager.LoadScene("Main_Menu", LoadSceneMode.Additive);
                break;
            case -2:
                SceneManager.LoadScene("Level_-2", LoadSceneMode.Additive);
                break;
            case -1:
                SceneManager.LoadScene("Level_Tutorial", LoadSceneMode.Additive);
                break;
            case 1:
                SceneManager.LoadScene("Level_1", LoadSceneMode.Additive);
                break;
            case 2:
                SceneManager.LoadScene("Level_2", LoadSceneMode.Additive);
                break;
            case 3:
                SceneManager.LoadScene("Level_3", LoadSceneMode.Additive);
                break;
            case 4:
                SceneManager.LoadScene("Level_4", LoadSceneMode.Additive);
                break;
            case 5:
                SceneManager.LoadScene("Level_5", LoadSceneMode.Additive);
                break;
            case 6:
                SceneManager.LoadScene("End", LoadSceneMode.Additive);
                break;

        }



        
    }

    public int End_Num=0;

    //输出结局的序号
    public int Calculate_End()
    {
        //经过一通计算
        return End_Num;
    }

    public void Remove_Other_Scene()
    {
        if(Current_Level_Num!=0)
        {
            SceneManager.UnloadScene("Main_Menu");
        }
    }
    [Header("关于触发结局的变量")]
    public bool Tao_Mu_Xie;
    public bool Jian_Dao;
    public bool Feng_Hun_Guan;
    public bool Jia_Yi;
    public bool Ding_Hun_Suo;
    public bool Luo_Pan;
    public bool Jue_Bi_Xin;
    public bool Shou_Pa;
    public bool Yu_Zan;

    public void Set_Bag_Item(int index)
    {
        switch (index)
        {
            case 1:
                Tao_Mu_Xie = true;
                break;
            case 2:
                Jian_Dao = true;
                break;
            case 3:
                Feng_Hun_Guan = true;
                break;
            case 4:
                Jia_Yi = true;
                break;
            case 5:
                Ding_Hun_Suo = true;
                break;
            case 6:
                Luo_Pan = true;
                break;
            case 7:
                Jue_Bi_Xin = true;
                break;
            case 8:
                Shou_Pa = true;
                break;
            case 9:
                Yu_Zan = true;
                break;
        }

    }

    public void Refresh_Global()
    {
      Tao_Mu_Xie = false;
      Jian_Dao = false;
      Feng_Hun_Guan = false;
      Jia_Yi = false;
      Ding_Hun_Suo = false;
      Luo_Pan = false;
      Jue_Bi_Xin = false;
      Shou_Pa = false;
      Yu_Zan = false;
}

}
