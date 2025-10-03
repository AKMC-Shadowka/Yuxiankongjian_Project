using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List_Button_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void List_Button_Click()
    {

        
        int index = transform.GetSiblingIndex();
        
        Task_UI T_U = gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<Task_UI>();

        if(T_U==null)
        {
            return;
        }

        T_U.Task_Message_Update(index);
    }
}
