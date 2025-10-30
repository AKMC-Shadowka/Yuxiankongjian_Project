using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Level_3_Controller : Level_Controller
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

        SceneManager.UnloadScene("Level_3");
    }



    //关于关卡结束后应该做的事情
    public override void End_Event()
    {

        //Debug.Log("Enter Level Tutorial End Event");
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();

        //对于Level_Tutorial而言，需要把场景切换成Level_1，然后Global_Controller需要把自己的Current_Level_Num设置为1，代表到达了下一个关卡
        StartCoroutine(Enter_Level_4());

    }


    private IEnumerator Enter_Level_4()
    {

        //配合黑幕演出，最黑的时候切换场景
        yield return new WaitForSeconds(2.5f);


        AsyncOperation AO = SceneManager.LoadSceneAsync("Level_4", LoadSceneMode.Additive);

        GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num = 4;

        GameObject.Find("Canvas").GetComponent<UI_Controller>().UI_Refresh();

        while (!AO.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level_4"));

        SceneManager.UnloadScene("Level_3");
    }


    public override void Dialog_Button_Click(int index)
    {
        if(Shawty_Dialog==true&&index==8)
        {
            GameObject.Find("Dialog_Frontend").transform.GetChild(0).gameObject.GetComponent<Image>().material = Glitch_Material;
        }
        if(Shawty_Dialog==true&&index==10)
        {
            GameObject.Find("Dialog_Frontend").transform.GetChild(0).gameObject.GetComponent<Image>().material = null;
        }
    }

    public override void End_Dialog()
    {

        GameObject.Find("Character").GetComponent<Player>().Effective_Move = true;

        if(Shawty_Dialog==true)
        {
            Shawty_Dialog = false;
            Shawty.SetActive(false);
        }

        if(Poison_Dialog_On==true)
        {
            Poison_Dialog_On = false;
            Destroy_All_Poison_Dialog();
        }

        //结束第一个镜子对话
        if(Mirror_Dialog_1==true)
        {
            Mirror_Dialog_1 = false;
            Mirror_Dialog_2 = true;
            Curtain_Dialog.GetComponent<Activity_Dialog>().Start_Curtain_Dialog();
            return;
        }

        //结束第二个镜子对话
        if(Mirror_Dialog_2==true)
        {
            Mirror_Dialog_2 = false;
            GameObject canvas = GameObject.Find("Canvas");

            canvas.GetComponent<UI_Controller>().Curtain_Show = false;
            canvas.GetComponent<UI_Controller>().UI_Refresh();

            Normal_Dialog.GetComponent<Activity_Dialog>().Start_Dialog();

            Cliff_1.SetActive(false);
            Cliff_2.SetActive(false);
            return;
        }


    }

    public GameObject Hidden_Road;
    public GameObject Unused_Wall;

    public void Set_Road_Active()
    {
        Hidden_Road.SetActive(true);
        Unused_Wall.SetActive(false);
    }

    [Header("关于鸡人的变量")]
    public GameObject Shawty;//鸡人
    public bool Shawty_Dialog;//是否进入最后的鸡人对话

    public void Enter_Shawty_Dialog()
    {
        Shawty_Dialog = true;
    }
    [Header("混沌特效")]
    public Material Glitch_Material;


    [Header("关于毒区的相关设定")]
    public bool Poison_Dialog_On;
    public List<GameObject> Poison_Dialog_Zone;

    public void Enter_Poison_Dialog()
    {
        Poison_Dialog_On = true;
    }
    public void Destroy_All_Poison_Dialog()
    {
        for(int i=0;i<Poison_Dialog_Zone.Count;i++)
        {
            Poison_Dialog_Zone[i].SetActive(false);
        }
    }

    [Header("关于镜子的相关设定")]
    public bool Mirror_Dialog_1;
    public bool Mirror_Dialog_2;
    public GameObject Curtain_Dialog;//黑幕对话
    public GameObject Normal_Dialog;//黑幕对话后的回复对话
    public void Enter_Mirror_Dialog_1()
    {
        Mirror_Dialog_1 = true;
    }
    public void Enter_Mirror_Dialog_2()
    {
        Mirror_Dialog_2 = true;
    }

    [Header("关于最后空气墙的相关障碍")]
    public GameObject Cliff_1;
    public GameObject Cliff_2;


    public override void Start()
    {
        GameObject.Find("Canvas").GetComponent<Audio_Player>().Play(4);
    }

}
