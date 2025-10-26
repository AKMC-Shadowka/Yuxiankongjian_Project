using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Activity_Controller : MonoBehaviour
{

//这是一个关于各种通用活动（包括但不限于对话，剧情滚动之类的）控制的相关类，这些活动有一个共同的特点，那就是需要通过触发器进行，那么这个通用的类只会进行两件事：
//1，触发器的检测
//2，触发指定的活动
//就这么简单了

    [System.Serializable]
    public class Activity_Event : UnityEvent { }


    public Activity_Event Current_Activity_Event;


    public void OnTriggerEnter2D(Collider2D other)
    {
        //触发指定活动

        Current_Activity_Event.Invoke();

        StartCoroutine(Destroy_Self());
    }

    public IEnumerator Destroy_Self()
    {
        yield return new WaitForSeconds(5.1f);
        gameObject.SetActive(false);
    }
}
