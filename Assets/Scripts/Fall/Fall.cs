using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fall : MonoBehaviour
{
    //Man! What can I say? Manba Out!
    //��δ����ǹ���׹���Ľű�

    [System.Serializable]
    public class Death_Event : UnityEvent { }

    public Death_Event Current_Death_Event;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Current_Death_Event.Invoke();
    }


}
