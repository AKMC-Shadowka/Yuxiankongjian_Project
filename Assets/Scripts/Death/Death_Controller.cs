using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Controller : MonoBehaviour
{


    public void Restart_Button_Click()
    {
        GameObject Canvas_Object = gameObject.transform.parent.gameObject;

        UI_Controller U_C = Canvas_Object.GetComponent<UI_Controller>();

        U_C.Set_Death_Show(false);

        //UI的事情做完了，现在要重新复位玩家了

        GameObject Character_Object = GameObject.Find("Character");

        Player Player_Component = Character_Object.GetComponent<Player>();

        Character_Object.transform.position = Player_Component.Saved_Position;

        Player_Component.Effective_Move = true;

    }
}
