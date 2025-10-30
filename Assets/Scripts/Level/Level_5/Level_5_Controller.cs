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

    public override void End_Dialog()
    {

        GameObject.Find("Character").GetComponent<Player>().Effective_Move = true;
        
        if(Enter_Diary_1==true)
        {
            Enter_Diary_1 = false;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().Curtain_Show = false;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();
            Comment_1.GetComponent<Activity_Dialog>().Start_Dialog();
            return;
        }

        if (Enter_Diary_2 == true)
        {
            Enter_Diary_2 = false;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().Curtain_Show = false;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();
            Comment_2.GetComponent<Activity_Dialog>().Start_Dialog();
            return;
        }

        if (Enter_Diary_3 == true)
        {
            Enter_Diary_3= false;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().Curtain_Show = false;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();
            Comment_3.GetComponent<Activity_Dialog>().Start_Dialog();
            return;
        }

        if(Final_Choice==true)
        {
            Final_Choice = false;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().Final_Choice_Show = true;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();
            GameObject.Find("Character").GetComponent<Player>().Effective_Move = false;
            return;
        }



    }


    [Header("关于最后三个结局玩家能看到多少")]
    public GameObject Diary_1;
    public GameObject Diary_2;
    public GameObject Diary_3;


    public Bag Current_Bag;

    public void Set_Diary_Show()
    {
        //不仅要确认哪里的日记显示，哪里的不显示，还要做一件事，那就是修改FinalChoice下面的三个选项是否显示

        GameObject Choice_1 = GameObject.Find("Canvas").GetComponent<UI_Controller>().Final_Choice_UI.transform.GetChild(0).gameObject;
        GameObject Choice_2 = GameObject.Find("Canvas").GetComponent<UI_Controller>().Final_Choice_UI.transform.GetChild(1).gameObject;
        GameObject Choice_3 = GameObject.Find("Canvas").GetComponent<UI_Controller>().Final_Choice_UI.transform.GetChild(2).gameObject;

        if (
            Current_Bag.Find("封魂罐")
            &&
            Current_Bag.Find("桃木楔")
            &&
            Current_Bag.Find("剪刀")
            )
        {
            Diary_1.SetActive(true);
            Choice_1.SetActive(true);
        }
        else
        {
            Diary_1.SetActive(false);
            Choice_1.SetActive(false);
        }

        if (
            Current_Bag.Find("嫁衣")
            &&
            Current_Bag.Find("定魂锁")
            &&
            Current_Bag.Find("罗盘")
            )
        {
            Diary_2.SetActive(true);
            Choice_2.SetActive(true);
        }
        else
        {
            Diary_2.SetActive(false);
            Choice_2.SetActive(false);
        }

        if (
            Current_Bag.Find("绝笔信")
            &&
            Current_Bag.Find("手帕")
            &&
            Current_Bag.Find("玉簪")
            )
        {
            Diary_3.SetActive(true);
            Choice_3.SetActive(true);
        }
        else
        {
            Diary_3.SetActive(false);
            Choice_3.SetActive(false);
        }


    }

    public override void Start()
    {
       Set_Diary_Show();

        GameObject.Find("Canvas").GetComponent<Audio_Player>().Play(4);
    }

    [Header("一二三对话的相关变量")]
    public bool Enter_Diary_1;
    public bool Enter_Diary_2;
    public bool Enter_Diary_3;

    public GameObject Comment_1;
    public GameObject Comment_2;
    public GameObject Comment_3;

    public void Enter_Diary_Dialog_1()
    {
        Enter_Diary_1 = true;
    }

    public void Enter_Diary_Dialog_2()
    {
        Enter_Diary_2 = true;
    }

    public void Enter_Diary_Dialog_3()
    {
        Enter_Diary_3 = true;
    }

    [Header("最后一条结局相关变量")]
    public Dialog End_Dialog_1;
    public Dialog End_Dialog_2;
    public Dialog End_Dialog_3;

    public bool Final_Choice;//是否进行最终选择

    public Activity_Dialog Final_Activity_Dialog;

    public void Enter_Final_Choice()
    {
        Final_Choice = true;
    }

    public void Set_Final_Dialog(int index)
    {
        if(index==1)
        {
            Final_Activity_Dialog.Current_Dialog = End_Dialog_1;
        }

        if (index == 2)
        {
            Final_Activity_Dialog.Current_Dialog = End_Dialog_2;
        }

        if (index == 3)
        {
            Final_Activity_Dialog.Current_Dialog = End_Dialog_3;
        }
    }

    [Header("关于结局的相关动画")]
    public bool Final_Dialog;
    public void Enter_Final_Dialog()
    {

    }


}
