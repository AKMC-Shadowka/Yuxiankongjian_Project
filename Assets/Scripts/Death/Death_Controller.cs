using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Death_Controller : MonoBehaviour
{


    public void Restart_Button_Click()
    {

        StartCoroutine(Reload_And_Refresh());


        /*
         
         GameObject Canvas_Object = gameObject.transform.parent.gameObject;

        UI_Controller U_C = Canvas_Object.GetComponent<UI_Controller>();

        U_C.Set_Death_Show(false);

        U_C.Blood_Show = false;//把血渍去掉
        U_C.UI_Refresh();
        //UI的事情做完了，现在要重新复位玩家了

        GameObject Character_Object = GameObject.Find("Character");

        Player Player_Component = Character_Object.GetComponent<Player>();

        Character_Object.transform.position = Player_Component.Saved_Position;

        Player_Component.Effective_Move = true;

        gameObject.SetActive(false);

         */
    }

    public IEnumerator Reload_And_Refresh()
    {
        yield return StartCoroutine(Reload_Level());

        yield return StartCoroutine(Other_Things());
    }


    public IEnumerator Other_Things()
    {

        Debug.Log("Death Test 1");
        GameObject Canvas_Object = gameObject.transform.parent.gameObject;

        UI_Controller U_C = Canvas_Object.GetComponent<UI_Controller>();

        U_C.Set_Death_Show(false);

        U_C.Blood_Show = false;//把血渍去掉
        U_C.UI_Refresh();
        //UI的事情做完了，现在要重新复位玩家了

        GameObject Character_Object = GameObject.Find("Character");

        Player Player_Component = Character_Object.GetComponent<Player>();

        Character_Object.transform.position = Player_Component.Saved_Position;

        Player_Component.Effective_Move = true;

        gameObject.SetActive(false);

        yield return null;
    }

    public IEnumerator Reload_Level()
    {
        string Level_Name= "";

        switch (GameObject.Find("Global").GetComponent<Global_Controller>().Current_Level_Num)
        {
            case 0:
                Level_Name = "Main_Menu";
                break;
            case -1:
                Level_Name = "Level_Tutorial";
                break;
            case -2:
                Level_Name = "Level_Tutorial_2";
                break;
            case 1:
                Level_Name = "Level_1";
                break;
            case 2:
                Level_Name = "Level_2";
                break;
            case 3:
                Level_Name = "Level_3";
                break;
            case 4:
                Level_Name = "Level_4";
                break;
            case 5:
                Level_Name = "Level_5";
                break;


        }

        AsyncOperation AO = SceneManager.UnloadSceneAsync(Level_Name);
        while(!AO.isDone)
        {
            yield return null;
        }

        AsyncOperation AO1= SceneManager.LoadSceneAsync(Level_Name, LoadSceneMode.Additive);
        while(!AO1.isDone)
        {
            yield return null;
        }
        //Other_Things();
       


    }
}
