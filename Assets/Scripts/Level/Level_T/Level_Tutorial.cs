using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_Tutorial : Level_Controller
{
    public GameObject Current_Character;

    public Dialog Start_Dialog;//开始关卡的对话

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

        SceneManager.UnloadScene("Level_1");
    }



    //关于关卡结束后应该做的事情
    public override void End_Event()
    {

        //Debug.Log("Enter Level Tutorial End Event");
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();

        //对于Level_Tutorial而言，需要把场景切换成Level_1，然后Global_Controller需要把自己的Current_Level_Num设置为1，代表到达了下一个关卡
        StartCoroutine(Enter_Level_1());
        
    }


    private IEnumerator Enter_Level_1()
    {

        //配合黑幕演出，最黑的时候切换场景
        yield return new WaitForSeconds(2.5f);


        AsyncOperation AO = SceneManager.LoadSceneAsync("Level_1", LoadSceneMode.Additive);

        GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num = 1;

        GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();

        while (!AO.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level_1"));

        SceneManager.UnloadScene("Level_Tutorial");
    }



    [Header("需要在活动中被激活的墙")]
    public GameObject Active_Wall;

    public GameObject Disactive_Wall;


    public void Set_Road_Active()
    {
        //对于Tutorial关卡中的Road3-1实行激活操作
        Active_Wall.SetActive(true);
        Disactive_Wall.SetActive(false);
    }

    public override void End_Dialog()
    {
        Debug.Log("End Dialog");
        GameObject.Find("Character").GetComponent<Player>().Effective_Move = true;
        Debug.Log("In End Dialog Effect_Move="+ GameObject.Find("Character").GetComponent<Player>().Effective_Move);
        
    }

    public IEnumerator End_Dialog_2()
    {
        //对于第一件事没有办完的重新补办
        yield return new WaitForSeconds(0.03f);
        GameObject.Find("Character").GetComponent<Player>().Effective_Move = true;

    }

    public override void Start()
    {
        //新的初始化工作，对于Level_Tutorial而言，需要触发新的对话，然后怎么怎么样
        gameObject.GetComponent<Activity_Dialog>().Current_Dialog = Start_Dialog;
        gameObject.GetComponent<Activity_Dialog>().Start_Dialog();


        GameObject.Find("Canvas").GetComponent<Audio_Player>().Play(5);
    }

}
