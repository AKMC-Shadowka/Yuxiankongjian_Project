using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Task_UI : MonoBehaviour
{
    //������ǹ���Task��ν���UI��ʾ�ģ�ʵ���϶���UI���������ܳ��ֵ�UI��Ӧ�ö����ڹؿ���������Ҫ�����е�UI����Global�����У�Ȼ���ٿ��ǳ������ܵ����⡣

    //UI��������From Canvas
    public UI_Controller UI_Controller_Component;


    //���������� From Task
    public Task_Container Task_Container_Component;

    public GameObject List_UI_Preset;//���ڲ鿴�����б�UI��Ԥ�Ƽ���������Task_List_Upadte���������ɣ�Instantiate��

    private List<GameObject> Current_List_UI;//��ǰ�����б���¡������UI���


    public void Start()
    {
        
    }




    //ͨ��UI������ť���
    public void Task_Button_Click()
    {
        UI_Controller_Component.Task_Terminal_Show = true;

        UI_Controller_Component.UI_Refresh();

        Task_UI_Update();//��ҳ�����������Ϣ��Ϣˢ��


    }

    //����UI�����ڲ����˳���ť����
    public void Task_Exit_Button_Click()
    {
        UI_Controller_Component.Task_Terminal_Show = false;

        UI_Controller_Component.UI_Refresh();


        //�ǵ����֮ǰ���ɵ����
        for(int i=0;i<Current_List_UI.Count;i++)
        {
            Destroy(Current_List_UI[i]);
            //Debug.Log("Destroy " + i);
        }
    }

    //���ڴ򿪽����Task_UI����θ��µ�
    public void Task_UI_Update()
    {

        //UI���������б����
        Task_List_Upadte();

        //�������ϸ��Ϣ����
        Task_Message_Update(0);

    }


    public void Task_List_Upadte()
    {
        List<Task> Current_Task_List = Task_Container_Component.Get_Available_Task_List();

        Current_List_UI = new List<GameObject>();


        for (int i=0 ; i< Current_Task_List.Count; i++)
        {
            GameObject List_Preset = Instantiate(List_UI_Preset);
            //�����ɵ�Ԥ�Ƽ���λ
            List_Preset.transform.SetParent(gameObject.transform.GetChild(0).GetChild(0).GetChild(0));

            Current_List_UI.Add(List_Preset);

            //��Ϣ����
            Text Task_Title = List_Preset.transform.GetChild(0).GetComponent<Text>();
            Task_Title.text = Current_Task_List[i].Task_Name;

            //Destroy(List_Preset);
        }
    }


    public void Task_Message_Update(int index)
    {
        List<Task> Current_Task_List = Task_Container_Component.Get_Available_Task_List();

        Text Task_Info = gameObject.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>();

        Task_Info.text = Current_Task_List[index].Task_Description;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
