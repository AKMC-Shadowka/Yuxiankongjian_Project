using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class Dialog_Player : MonoBehaviour
{
    //用来储存当前的Dialog数据源
    public Dialog Current_Dialog;

    //用来储存当前的Dialog背景和文字
    public GameObject Dialog_Object;

    public GameObject Dialog_Text_Object;

    public int  Current_Dialog_Index;//这个变量用来确认当前的Dialog播放到哪里了

    public Image Dialog_Background_Image;

    public Text Dialog_Text_Text;

    public AudioSource Dialog_Audio_Component;//发出声音的音轨绑定，已经做好了

    public int Current_Text_Refresh_Index;//用于文字刷新的int变量

    public bool Is_Finished;//判断这个对话是否已经结束啦

    public Component Current_Level_Controller;//这个其实就是用来装载当前的Level_Controller脚本的,为什么写成Component?因为收藏室也要用！！！


    //那么在这里重新梳理一下每一次Dialog_Refresh需要更改的东西都有什么吧
    //1，人物立绘
    //2，人物姓名
    //3，对话内容（Dialog_Text）

    public Image Dialog_Human_Image;
    public Text Dialog_Name_Text;

    //用来给上述三个变量赋值
    public void Set_Dialog_Player_Attribute(Dialog d,GameObject Dialog_Background,GameObject Dialog_Text)
    {

        Is_Finished = false;
        Current_Dialog_Index = 0;
        this.Current_Dialog = d;
        this.Dialog_Object = Dialog_Background;
        this.Dialog_Text_Object = Dialog_Text;

        Dialog_Background_Image = Dialog_Object.GetComponent<Image>();

        Dialog_Text_Text = Dialog_Text_Object.GetComponent<Text>();

        Dialog_Human_Image = Dialog_Background.transform.GetChild(0).gameObject.GetComponent<Image>();

        //Debug.Log("Dialog_Background=\" " + Dialog_Background.name + " \" text=" + Dialog_Background.name);
        Dialog_Name_Text = Dialog_Background.transform.GetChild(2).gameObject.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dialog_Initialize(Dialog d)
    {
        Current_Dialog_Index = 0;

        UI_Controller UI_C = gameObject.GetComponent<UI_Controller>();
        UI_C.Dialog_Show = true;
        UI_C.UI_Refresh();

        GameObject Dialog_Background = gameObject.transform.GetChild(10).gameObject;
        GameObject Dialog_Text = Dialog_Background.transform.GetChild(3).gameObject;
        Set_Dialog_Player_Attribute(d, Dialog_Background, Dialog_Text);

        //Dialog_Object.SetActive(true);
        Dialog_Refresh();

    }


    public void Dialog_Initialize(Dialog d, GameObject Dialog_Background, GameObject Dialog_Text)
    {
        Current_Dialog_Index = 0;
        Set_Dialog_Player_Attribute(d,Dialog_Background,Dialog_Text);
        Dialog_Object.SetActive(true);
        Dialog_Refresh();
    }

    public void Dialog_Button_Click()
    {

        CancelInvoke("Dialog_Text_Refresh");
        Current_Dialog_Index++;
        //Debug.Log("Current_Dialog_Index=" + Current_Dialog_Index + " Count=" + current_Dialog.Dialog_Ele.Count);
        if (Current_Dialog_Index < Current_Dialog.Dialog_Ele.Count)
        {
            Dialog_Refresh();
        }
        else
        {
            Dialog_Finish();
        }
    }

    public void Dialog_Refresh()
    {
        //这个函数负责每点击一下的刷新工作
        Current_Text_Refresh_Index = 0;
        Dialog_Human_Image.sprite = Current_Dialog.Dialog_Ele[Current_Dialog_Index].Human;
        Dialog_Name_Text.text = Current_Dialog.Dialog_Ele[Current_Dialog_Index].Name;


        //Dialog_AudioSource赋值操作
        if(Dialog_Audio_Component!=null)
        {
            Dialog_Audio_Component.clip = Current_Dialog.Dialog_Ele[Current_Dialog_Index].Sound;
            Dialog_Audio_Component.Play();//播放声音
        }
       
        InvokeRepeating("Dialog_Text_Refresh", 0f, 0.1f);
    }

    public void Dialog_Text_Refresh()
    {
        if (Current_Dialog == null)
        {
            return;
        }
        if (Current_Dialog.Dialog_Ele[Current_Dialog_Index] != null && Current_Text_Refresh_Index < Current_Dialog.Dialog_Ele[Current_Dialog_Index].Content.Length && Dialog_Text_Object.activeSelf)
        {
            Current_Text_Refresh_Index++;
            Dialog_Text_Text.text = Current_Dialog.Dialog_Ele[Current_Dialog_Index].Content.Substring(0, Current_Text_Refresh_Index);
        }

        if (Current_Text_Refresh_Index == Current_Dialog.Dialog_Ele[Current_Dialog_Index].Content.Length)
        {
            CancelInvoke("Dialog_Text_Refresh");
        }
    }

    public void Dialog_Finish()
    {
        Is_Finished = true;
        //this.Current_Level_Controller.Set_Hero_Controller(true);


        //以后要执行通用的函数，就这么办，这条注释很重要哦
        //其实我写了这么多C#脚本，核心点就在于多写注释，不然代码是干啥的真的会忘，而且对于一个项目来说，项目越大，动工的成本就越高

        //对于每一个Dialog_Player结束的时候来说，默认会将当前的Level_Controller的Hero_Controller设置为True，也就是解锁
        //如果有特殊的需求，比如说需要在结尾进入Quiz，或者其他什么需求，由于Dialog_Player，也就是本脚本的函数Dialog_Button_Click()对于Level_Controller而言是嵌套在Level_Controller的
        //Dialog_Button_Click()中进行使用的，并且Level_Controller能够直观地反映出当前玩家的游戏进度，也就能够对包含Hero_Controller在内的一系列变量做出合理的安排

        /*

         MethodInfo methodInfo = Current_Level_Controller.GetType().GetMethod("Set_Hero_Controller");
         if(methodInfo!=null)
         {
             methodInfo.Invoke(Current_Level_Controller, new object[] { true });
         }
         Dialog_Object.SetActive(false);

         */


        //用完了一个预制件，需要把预制件删掉
        //DestroyImmediate(Dialog_Object.transform.parent.gameObject);

       UI_Controller UI_C= gameObject.GetComponent<UI_Controller>();
        UI_C.Dialog_Show = false;
        UI_C.UI_Refresh();

    }


}
