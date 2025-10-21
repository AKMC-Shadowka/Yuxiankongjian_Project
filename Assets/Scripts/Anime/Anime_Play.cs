using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime_Play : MonoBehaviour
{

    private Animator Current_Animator;
    // Start is called before the first frame update
    void Start()
    {
        Current_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Current_Animator.enabled=true;
        }
        else
        {
            Current_Animator.enabled = false;
        }
    }
}
