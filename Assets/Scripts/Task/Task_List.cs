using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName="Task_List",menuName="Game/Task_List",order=1)]
public class Task_List : ScriptableObject
{
    public List<Task> Current_Task_List;
}
