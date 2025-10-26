using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Main_Menu_Controller : MonoBehaviour
{

    //主菜单UI控制器

    public Global_Controller Global_Controller_Component;
    public UI_Controller UI_Controller_Component;


    public void Start_Button_Click()
    {

        if(Global_Controller_Component.Current_Level_Num == -1)
        {
            //防止2次连点造成Bug
            return;
        }


        //此处应该先进入教学关卡，所以应该把Current_Level_Num=-1
        Global_Controller_Component.Current_Level_Num = -1;
        


        gameObject.GetComponent<Black_UI>().Start_Perform();
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {

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
        SceneManager.UnloadSceneAsync("Main_Menu");
    }

    public void Quit_Button_Click()
    {

        Application.Quit();

        // 在编辑器中停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

}
