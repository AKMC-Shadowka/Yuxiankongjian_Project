using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_4_Controller : Level_Controller
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

        SceneManager.UnloadScene("Level_4");
    }



    //关于关卡结束后应该做的事情
    public override void End_Event()
    {

        //Debug.Log("Enter Level Tutorial End Event");
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();

        //对于Level_Tutorial而言，需要把场景切换成Level_1，然后Global_Controller需要把自己的Current_Level_Num设置为1，代表到达了下一个关卡
        StartCoroutine(Enter_Level_5());

    }


    private IEnumerator Enter_Level_5()
    {

        //配合黑幕演出，最黑的时候切换场景
        yield return new WaitForSeconds(2.5f);


        AsyncOperation AO = SceneManager.LoadSceneAsync("Level_5", LoadSceneMode.Additive);

        GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num = 5;

        GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();

        while (!AO.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level_5"));

        SceneManager.UnloadScene("Level_4");
    }


    public override void End_Dialog()
    {

        GameObject.Find("Character").GetComponent<Player>().Effective_Move = true;


        if(Story_Dialog_1==true)
        {
            Story_Dialog_1 = false;
            Story_Dialog_2 = true;
            Curtain_Dialog.GetComponent<Activity_Dialog>().Start_Curtain_Dialog();
            return;
        }

        if(Story_Dialog_2==true)
        {
            Story_Dialog_2 = false;
            GameObject canvas = GameObject.Find("Canvas");

            canvas.GetComponent<UI_Controller>().Curtain_Show = false;
            canvas.GetComponent<UI_Controller>().UI_Refresh();

            Normal_Dialog.GetComponent<Activity_Dialog>().Start_Dialog();
            return;
        }
    }


    [Header("关于生平的相关设定")]
    public bool Story_Dialog_1;
    public bool Story_Dialog_2;
    public GameObject Curtain_Dialog;//黑幕对话
    public GameObject Normal_Dialog;//黑幕对话后的回复对话
    public void Enter_Story_Dialog_1()
    {
        Story_Dialog_1 = true;
    }
    public void Enter_Story_Dialog_2()
    {
        Story_Dialog_2 = true;
    }

    public override void Start()
    {
        GameObject.Find("Canvas").GetComponent<Audio_Player>().Play(5);
    }
}
