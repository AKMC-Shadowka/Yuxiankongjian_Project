using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Choice_Controller : MonoBehaviour
{
    //这个类应该挂载在Canvas上，负责最终选择的一些表演，需要调动到当前Level_Controller的某个Dialog文件，这就可以了

    public Dialog Final_Dialog_1;
    public Dialog Final_Dialog_2;
    public Dialog Final_Dialog_3;

    public Activity_Dialog Current_Activity_Dialog;
    //播放结局（非鸡鬼）对话，以及确认最后的对话

    //刷新对话之后，那么最后的结局走向就应该明了了，也就是要修改Global_Controller.End_Num;
    public void Choice_1_Click()
    {
        Current_Activity_Dialog.Current_Dialog = Final_Dialog_1;
        Current_Activity_Dialog.Start_Dialog();
        gameObject.GetComponent<UI_Controller>().Final_Choice_Show = false;
        gameObject.GetComponent<UI_Controller>().UI_Refresh();

        GameObject.Find("Global").GetComponent<Global_Controller>().End_Num = 0;

        GameObject.Find("Level_Controller").GetComponent<Level_5_Controller>().Set_Final_Dialog(1);
    }

    public void Choice_2_Click()
    {
        Current_Activity_Dialog.Current_Dialog = Final_Dialog_2;
        Current_Activity_Dialog.Start_Dialog();

        gameObject.GetComponent<UI_Controller>().Final_Choice_Show = false;
        gameObject.GetComponent<UI_Controller>().UI_Refresh();

        GameObject.Find("Global").GetComponent<Global_Controller>().End_Num = 1;

        GameObject.Find("Level_Controller").GetComponent<Level_5_Controller>().Set_Final_Dialog(2);
    }

    public void Choice_3_Click()
    {
        Current_Activity_Dialog.Current_Dialog = Final_Dialog_3;
        Current_Activity_Dialog.Start_Dialog();

        gameObject.GetComponent<UI_Controller>().Final_Choice_Show = false;
        gameObject.GetComponent<UI_Controller>().UI_Refresh();

        GameObject.Find("Global").GetComponent<Global_Controller>().End_Num = 2;

        GameObject.Find("Level_Controller").GetComponent<Level_5_Controller>().Set_Final_Dialog(3);
    }
}
