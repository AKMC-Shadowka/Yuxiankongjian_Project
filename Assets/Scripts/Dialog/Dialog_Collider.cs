using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_Collider : MonoBehaviour
{

    private bool Dialog_Permission=false;

    public Dialog Current_Dialog;//需要播放的对话

    public void Update()
    {
        if(Dialog_Permission==false)
        {
            return;
        }


        bool Dialog_Show = GameObject.Find("Canvas").GetComponent<UI_Controller>().Dialog_Show;

        if(Dialog_Show==true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //触发对话
            GameObject.Find("Canvas").GetComponent<Dialog_Player>().Dialog_Initialize(Current_Dialog);
        }
    }

//这里是对话触发器，掌管2D Collider
public void OnTriggerEnter2D(Collider2D other)
    {

        Dialog_Permission = true;

        if(other.GetComponent<Player>()==null)
        {
            return;
        }

        //gameObject.transform.GetChild(0).gameObject.SetActive(true);

    }

    public void OnTriggerExit2D(Collider2D other)
    {

        Dialog_Permission = false;

        if (other.GetComponent<Player>() == null)
        {
            return;
        }

        gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }
}
