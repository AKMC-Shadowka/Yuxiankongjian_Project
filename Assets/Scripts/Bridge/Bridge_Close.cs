using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_Close : MonoBehaviour
{
    public BoxCollider2D River;

    public void OnTriggerEnter2D(Collider2D other)
    {
        River.enabled =false;
    }
}
