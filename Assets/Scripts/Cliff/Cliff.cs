using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour
{
    //有可能成为悬崖的地方，这个地方的某个触发器会随着Gravity_On的变化而变化
    //当俯视视角，也就是Gravity_On=false的时候，悬崖被设计为无法通过，也就是BoxCollider2D.isTrigger=false;
    //当横板卷轴，此时悬崖的阻碍被破除，BoxCollider2D.isTrigger=true，然后这个时候玩家就可以通过了

    //一般来讲作为Cliff的BoxCollider要独立出来，这样可以直接被GetComponent<BoxCollider2D>()所指向，也更方便功能的编写

    public Player Player_Component;

    public BoxCollider2D Current_Collider;

    public void Start()
    {
        Current_Collider =  gameObject.GetComponent<BoxCollider2D>();
    }
    public void Update()
    {
        Check_State();
    }

    public void Check_State()
    {
        if(Player_Component.Gravity_On==true)
        {
            //此时需要Trigger=true了
           Current_Collider.isTrigger = true;
        }
        else
        {
            Current_Collider.isTrigger =false;
        }
    }
    
}
