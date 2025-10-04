using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{

    public BoxCollider2D River;


    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("User Enter the Bridge");
        River.enabled = false;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("User Exit the Bridge");
        River.enabled = true;
    }

}
