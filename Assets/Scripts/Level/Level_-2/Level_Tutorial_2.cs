using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Tutorial_2 : Level_Controller
{


    public override void Start()
    {
        //等待2.5秒开始引入对话
        GameObject.Find("Global").GetComponent<Global_Controller>().Remove_Other_Scene();

        gameObject.GetComponent<Activity_Dialog>().Start_Dialog();

        GameObject.Find("Canvas").GetComponent<Audio_Player>().Play(4);
    }

    public int Dialog_Num;//对话数量

    public List<Dialog> Current_Dialog_List;//储存的对话


    public override void End_Event()
    {
        //进入Level_Tutorial

        //此处应该先进入教学关卡，所以应该把Current_Level_Num=-1
        Global_Controller Global_Controller_Component = GameObject.Find("Global").GetComponent<Global_Controller>();

        Global_Controller_Component.Current_Level_Num = -1;



        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();
        StartCoroutine(LoadLevel());

    }


    private IEnumerator LoadLevel()
    {

        UI_Controller UI_Controller_Component=GameObject.Find("Canvas").GetComponent<UI_Controller>();

        //将1黑幕表演的时间让出来
        yield return new WaitForSeconds(2.5f);

        UI_Controller_Component.UI_Refresh();
        // 异步加载新场景
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Level_Tutorial", LoadSceneMode.Additive);

        // 等待加载完成
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // 设置新加载的场景为活动场景
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level_Tutorial"));

        // 现在可以安全地卸载主菜单场景
        SceneManager.UnloadSceneAsync("Level_-2");
    }

    public override void End_Dialog()
    {

        Dialog_Num++;
        if(Dialog_Num==Current_Dialog_List.Count)
        {
            End_Event();
            return;
        }
        else
        {
            StartCoroutine(Update_Dialog());
        }

        
    }

    public IEnumerator Update_Dialog()
    {
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();
        yield return new WaitForSeconds(2.5f);

        gameObject.GetComponent<Activity_Dialog>().Current_Dialog = Current_Dialog_List[Dialog_Num];
        gameObject.GetComponent<Activity_Dialog>().Start_Dialog();

    }


}
