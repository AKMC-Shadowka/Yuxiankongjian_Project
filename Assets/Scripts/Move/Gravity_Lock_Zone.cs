using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Lock_Zone : MonoBehaviour
{
    // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Player>()!=null)
        {
            other.gameObject.GetComponent<Player>().Gravity_Lock = true;
        }
        else
        {
            return;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().Gravity_Lock = false;
        }
        else
        {
            return;
        }
    }
}
