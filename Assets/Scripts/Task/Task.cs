using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Task", menuName = "Game/Task", order = 1)]
public class Task : ScriptableObject
{


    public int Task_ID;//����ID

    public List<int> Task_Unlock_Condition;//�����������

    public string Task_Name;//��������

    [TextArea]
    public string Task_Description;//��������

    [TextArea]
    public List<string> Task_Requirements;//����Ҫ��
    public List<bool> Task_Completed;//����������

    public bool Task_Finished;//��ǰ�����Ƿ��Ѿ����

    public  bool Task_Unlock;//�ĵ�ǰ�����Ƿ����
    


}
