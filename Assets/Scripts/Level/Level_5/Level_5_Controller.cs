using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_5_Controller : Level_Controller
{
    public GameObject Current_Character;

    //关于如何退出这个界面返回Main_Menu
    public void Exit_Button_Click()
    {
        StartCoroutine(Back_To_Main_Menu());
    }

    public IEnumerator Back_To_Main_Menu()
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Additive);

        while (!AO.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main_Menu"));

        SceneManager.UnloadScene("Level_5");
    }



    //关于关卡结束后应该做的事情
    public override void End_Event()
    {

        //Debug.Log("Enter Level Tutorial End Event");
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();

        

        //对于Level_Tutorial而言，需要把场景切换成Level_1，然后Global_Controller需要把自己的Current_Level_Num设置为1，代表到达了下一个关卡
        StartCoroutine(Enter_Level_End());

    }


    private IEnumerator Enter_Level_End()
    {

        float Half_Time = GameObject.Find("Canvas").GetComponent<Black_UI>().Perform_Time / 2f;
        //配合黑幕演出，最黑的时候切换场景
        yield return new WaitForSeconds(Half_Time);


        End_Perform();


        AsyncOperation AO = SceneManager.LoadSceneAsync("End", LoadSceneMode.Additive);

        GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num = 6;

        GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();

        while (!AO.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("End"));

        

        SceneManager.UnloadScene("Level_5");
        //场景变化结束之后，需要在走马灯结束前Half_Time的时间里调用一次UI，但是这次要等多久呢？

        float End_Perform_Time = GameObject.Find("Canvas").GetComponent<End_Perform>().Perform_Time;
        yield return new WaitForSeconds(End_Perform_Time-2*Half_Time);
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();
        //再次进行场景变化之后

        //配合黑幕演出，最黑的时候切换场景
        yield return new WaitForSeconds(Half_Time);

        AsyncOperation AO1 = SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Additive);

        while(!AO1.isDone)
        {
            yield return null;
        }

        GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num = 0;
        GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));

        SceneManager.UnloadScene("End");

    }

    public void End_Perform()
    {
        //加载完场景之后开始播放走马灯结局
        GameObject.Find("Canvas").GetComponent<UI_Controller>().End_Show = true;
        GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();
        GameObject.Find("Canvas").GetComponent<End_Perform>().Start_Performance();
    }

}
