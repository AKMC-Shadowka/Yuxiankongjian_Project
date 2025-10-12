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

        Debug.Log("Enter Level Controller 1 End Event");
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();
    }

}
