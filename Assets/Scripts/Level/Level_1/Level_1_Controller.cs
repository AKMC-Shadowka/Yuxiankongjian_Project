using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_1_Controller : Level_Controller
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

        while(!AO.isDone)
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
        StartCoroutine(Enter_Level_2());

    }


    private IEnumerator Enter_Level_2()
    {

        //配合黑幕演出，最黑的时候切换场景
        yield return new WaitForSeconds(2.5f);


        AsyncOperation AO = SceneManager.LoadSceneAsync("Level_2", LoadSceneMode.Additive);

        GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num = 2;

        GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();

        while (!AO.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level_2"));

        SceneManager.UnloadScene("Level_1");
    }

    public override void End_Dialog()
    {
        GameObject.Find("Character").GetComponent<Player>().Effective_Move = true;

        if(Shawty_Dialog==true)
        {
            Shawty_Dialog = false;
            Shawty.SetActive(false);
            return;
        }

        if (Shawty_Dialog_2 == true)
        {
            Shawty_Dialog_2 = false;
            Shawty_2.SetActive(false);
            return;
        }

        if(Jar_Dialog==true)
        {
            Jar_Dialog = false;
            Jar_Object.SetActive(false);
            return;
        }


    }


    [Header("鸡鬼")]
    public GameObject Ghost;
    public void Set_Ghost_Active()
    {
        Ghost.SetActive(true);
    }

    [Header("关于鸡人的变量")]
    public GameObject Shawty;//鸡人
    public bool Shawty_Dialog;//是否进入最后的鸡人对话

    public void Enter_Shawty_Dialog()
    {
        Shawty_Dialog = true;
    }

    [Header("关于第二次鸡人的变量")]

    public GameObject Shawty_2;
    public bool Shawty_Dialog_2;

    public void Enter_Shawty_Dialog_2()
    {
        Shawty_Dialog_2 = true;
    }

    [Header("关于封魂罐的变量")]

    public bool Jar_Dialog;//进入封魂罐
    public GameObject Jar_Object;
    public void Enter_Jar_Dialog()
    {
        Jar_Dialog = true;
    }

    public override void Start()
    {

        GameObject.Find("Global").GetComponent<Global_Controller>().Remove_Other_Scene();
        GameObject.Find("Canvas").GetComponent<Audio_Player>().Play(4);
    }


}
