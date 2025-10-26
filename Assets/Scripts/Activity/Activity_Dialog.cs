using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity_Dialog : MonoBehaviour
{

    public Dialog Current_Dialog;//需要播放的对话

    public void Start_Dialog()
    {
        GameObject.Find("Character").GetComponent<Player>().Effective_Move = false;

        bool Dialog_Show = GameObject.Find("Canvas").GetComponent<UI_Controller>().Dialog_Show;

        if (Dialog_Show == true)
        {
            return;
        }


        //触发对话
        GameObject.Find("Canvas").GetComponent<Dialog_Player>().Dialog_Initialize(Current_Dialog);

    }

    




}
