using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_1_Controller : Level_Controller
{


    public GameObject Current_Character;

    //��������˳�������淵��Main_Menu
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



    //���ڹؿ�������Ӧ����������
    public override void End_Event()
    {

        Debug.Log("Enter Level Controller 1 End Event");
        GameObject.Find("Canvas").GetComponent<Black_UI>().Start_Perform();
    }

}
