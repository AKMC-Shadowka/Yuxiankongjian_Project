using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Poison_Controller : MonoBehaviour
{
    //毒区控制器，用来控制角色在毒区中是否

    public float Poison_Time;


    public float Current_Time=0f;

    public bool Poison_On = false;


    //关于UnityEvent的使用
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
        //进入毒区了

        if(other.gameObject.GetComponent<Player>()!=null)
        {

            Poison_On = true;
            //开始进入死亡倒计时
        }
    }

    public void Poison_Countdown()
    {
        Current_Time = Current_Time + Time.deltaTime;

        if(Current_Time>=Poison_Time)
        {
            //这就死了

            Poison_On = false;

            Current_Death_Event?.Invoke();
        }


    }


    public void OnTriggerExit2D(Collider2D other)
    {
        Poison_On = false;
    }

    


}
