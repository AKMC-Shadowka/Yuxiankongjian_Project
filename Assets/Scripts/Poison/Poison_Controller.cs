using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Poison_Controller : MonoBehaviour
{
    //�������������������ƽ�ɫ�ڶ������Ƿ�

    public float Poison_Time;


    public float Current_Time=0f;

    public bool Poison_On = false;


    //����UnityEvent��ʹ��
   [System.Serializable]
    public class Death_Event : UnityEvent { }
    
    public Death_Event Current_Death_Event;

    public void FixedUpdate()
    {
        if(Poison_On==false)
        {
            Current_Time = 0f;
            return;
        }

        Poison_Countdown();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //���붾����

        if(other.gameObject.GetComponent<Player>()!=null)
        {

            Poison_On = true;
            //��ʼ������������ʱ
        }
    }

    public void Poison_Countdown()
    {
        Current_Time = Current_Time + Time.deltaTime;

        if(Current_Time>=Poison_Time)
        {
            //�������

            Poison_On = false;

            Current_Death_Event?.Invoke();
        }


    }


    public void OnTriggerExit2D(Collider2D other)
    {
        Poison_On = false;
    }

    


}
