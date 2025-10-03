using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Exit_Controller : MonoBehaviour
{
    //这个类用于挂载在公用Canvas之上，用来确认退出按钮的作用

    public Global_Controller Global_Controller_Component;

    public UI_Controller UI_Controller_Component;

    public void Exit_Button_Click()
    {
        switch (Global_Controller_Component.Current_Level_Num)
        {
            case 0:
                return;
            case 1:
                Exit_Level_1();
                break;

        }




    }

    public void Exit_Level_1()
    {
        //将关卡序号设置为0
        Global_Controller_Component.Current_Level_Num = 0;
        //UI刷新
        UI_Controller_Component.UI_Refresh();

        StartCoroutine(Back_To_Main_Menu("Level_1"));
    }

    public IEnumerator Back_To_Main_Menu(string Level_Name)
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Additive);

        while (!AO.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main_Menu"));

        SceneManager.UnloadScene(Level_Name);
    }

}
