using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{

    public GameObject River_Object;

    public BoxCollider2D River;

    public void Start()
    {
        StartCoroutine(SetBoxActive());
    }

    public IEnumerator SetBoxActive()
    {
        yield return null;

        River.enabled = true;

    }

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
