using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fall : MonoBehaviour
{
    //Man! What can I say? Manba Out!
    //这段代码是管理坠亡的脚本

    [System.Serializable]
    public class Death_Event : UnityEvent { }

    public Death_Event Current_Death_Event;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Current_Death_Event.Invoke();
    }


}
