using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Container : MonoBehaviour
{
    //��������ڴ洢������Ҫ�Ķ�����
    //һ���ǿ��õ�����
    //һ���Ƕ�����Ϸ�����ܵ�������

    //�ܵ�������
    public Task_List Total_Task_List;

    //�ɹ�ѡ��������б�
    public Task_List Available_Task_List;

    public List<Task> Get_Available_Task_List()
    {
        return Available_Task_List.Current_Task_List;
    }


}
