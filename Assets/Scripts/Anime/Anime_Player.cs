using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime_Player : MonoBehaviour
{
    //动画播放器，挂载在Character上面
    public GameObject Current_Anime_Object;

    private int Current_Direction = 1;//S1,W2,D3,A4

    public void Start()
    {
        Current_Anime_Object = gameObject.transform.GetChild(1).gameObject;
        Current_Anime_Object.SetActive(true);
        Current_Anime_Object.GetComponent<Animator>().speed = 0f ;
    }

    public void Update()
    {

        Debug.Log("Current_Animator= " + Current_Anime_Object.name + "  Anime _ON= " + Current_Anime_Object.GetComponent<Animator>().enabled);
        Anime_Refresh();

    }

    public void Anime_Refresh()
    {
        if(!Input.GetKey(KeyCode.W)
            &&
            !Input.GetKey(KeyCode.A)
            &&
            !Input.GetKey(KeyCode.S)
            &&
            !Input.GetKey(KeyCode.D)
            )
        {
            Current_Anime_Object.GetComponent<Animator>().speed = 0f ;
            return;
        }

        if(Current_Direction!=Get_Direction())
        {
            Current_Direction = Get_Direction();
            Current_Anime_Object.SetActive(false);
            Current_Anime_Object = gameObject.transform.GetChild(Current_Direction).gameObject;
            Current_Anime_Object.SetActive(true);
        }


        bool Gravity_On = gameObject.GetComponent<Player>().Gravity_On;

        if(Gravity_On==false)
        {
            if(Input.GetKey(KeyCode.W))
            {
                Current_Anime_Object.GetComponent<Animator>().speed=1f;
                return;
            }

            if (Input.GetKey(KeyCode.S))
            {
              
                Current_Anime_Object.GetComponent<Animator>().speed=1f;
                return;
            }



        }

        if (Input.GetKey(KeyCode.A))
        {
           
            Current_Anime_Object.GetComponent<Animator>().speed=1f;
            return;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Current_Anime_Object.GetComponent<Animator>().speed=1f;
            return;
        }
    }

    public int Get_Direction()
    {
        if(Input.GetKey(KeyCode.W))
        {
            return 2;
        }

        if(Input.GetKey(KeyCode.S))
        {
            return 1;
        }

        if(Input.GetKey(KeyCode.A))
        {
            return 4;
        }

        if(Input.GetKey(KeyCode.D))
        {
            return 3;
        }

        return Current_Direction;

    }

}
