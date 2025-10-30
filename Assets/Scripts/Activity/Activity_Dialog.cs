using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity_Dialog : MonoBehaviour
{

    public Dialog Current_Dialog;//需要播放的对话

    public void Start_Dialog()
    {
        if(GameObject.Find("Character")!=null)
        {
            GameObject.Find("Character").GetComponent<Player>().Effective_Move = false;
        }
        

        bool Dialog_Show = GameObject.Find("Canvas").GetComponent<UI_Controller>().Dialog_Show;

        if (Dialog_Show == true)
        {
            return;
        }


        //触发对话
        GameObject.Find("Canvas").GetComponent<Dialog_Player>().Dialog_Initialize(Current_Dialog);

    }


    public void Start_Curtain_Dialog()
    {
        StartCoroutine(Start_Dialog_With_Curtain());
    }

    private IEnumerator Start_Dialog_With_Curtain()
    {
        //带黑色幕布演出的对话
        GameObject canvas = GameObject.Find("Canvas");
        //记得关控制
        GameObject.Find("Character").GetComponent<Player>().Effective_Move = false;

        canvas.GetComponent<Black_UI>().Start_Perform();
        yield return new WaitForSeconds(2.5f);

        Debug.Log("进入了Start_Dialog_With_Curtain");
        canvas.GetComponent<UI_Controller>().Curtain_Show = true;
        canvas.GetComponent<UI_Controller>().UI_Refresh();

        //其余的都和Start_Dialog一样了
        Start_Dialog();
    }


    




}
