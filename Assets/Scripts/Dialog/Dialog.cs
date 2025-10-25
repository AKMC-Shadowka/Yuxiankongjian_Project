using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New_Dialog", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    public List<D_E> Dialog_Ele;
}

[System.Serializable]
public class  D_E
{
    public string Name;
    //原本这里的Img要换成单独的背景（Background）和人物立绘(Human)
    public Sprite Img;
    public Sprite Human;
    public string Content;
    public AudioClip Sound;
}
