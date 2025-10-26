using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_2_Controller : Level_Controller
{
    public GameObject Current_Character;


    private bool Start_Chasing;

    public Chase_Controller Current_Chase_Controller;

    public void Start()
    {
        Start_Chasing = false;
    }


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

        SceneManager.UnloadScene("Level_2");
    }



    //关于关卡结束后应该做的事情
    public override void End_Event()
    {

        //Debug.Log("Enter Level Tutorial End Event");
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();
        UI_Controller UI_C=GameObject.Find("Canvas").GetComponent<UI_Controller>();
        UI_C.Chase_Show = false;
        UI_C.Blood_Show = false;
        UI_C.UI_Refresh();


        //对于Level_Tutorial而言，需要把场景切换成Level_1，然后Global_Controller需要把自己的Current_Level_Num设置为1，代表到达了下一个关卡
        StartCoroutine(Enter_Level_3());

    }


    private IEnumerator Enter_Level_3()
    {

        //配合黑幕演出，最黑的时候切换场景
        yield return new WaitForSeconds(2.5f);


        AsyncOperation AO = SceneManager.LoadSceneAsync("Level_3", LoadSceneMode.Additive);

        GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num = 3;

        GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();

        while (!AO.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level_3"));

        SceneManager.UnloadScene("Level_2");
    }

    public override void End_Dialog()
    {

        GameObject.Find("Character").GetComponent<Player>().Effective_Move = true;
        if(Start_Chasing==false)
        {

            Current_Chase_Controller.Start_Perform();
            return;
        }
    }

}
