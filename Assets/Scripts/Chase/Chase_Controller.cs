using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Controller : MonoBehaviour
{

    //追逐控制器，特供第二关进行追逐的关卡特效操作，这个应该挂载在一个专门的Chase物体上，虽然可以挂载在Level_Controller物体上，但还是作为区分这样就可以了

    public float Chase_Time;

    public float Current_Time;

    public bool Chase_On;//是否开始正式开始追逐战

    public Player Current_Character;//当前的玩家控制

    public Material Chase_Material;//ShaderGraph引用

    public Material Blood_Material;//血迹UI的材质引用

    public void Start()
    {
        Chase_On = false;
        Current_Time = 0f;
    }

    public void Update()
    {
        if(Chase_On==false)
        {
            return;
        }

        if(Current_Time>=Chase_Time)
        {

            UI_Controller UI_C = GameObject.Find("Canvas").GetComponent<UI_Controller>();
            UI_C.Blood_Show = false;
            UI_C.Chase_Show = false;
            UI_C.UI_Refresh();


            //播放死亡事件
            Current_Character.Death_Event();
            return;
        }

        Current_Time += Time.deltaTime;

        Chase_Perform();
        


    }

    public void Start_Perform()
    {
        Chase_On = true;


        UI_Controller UI_C = GameObject.Find("Canvas").GetComponent<UI_Controller>();
        UI_C.Blood_Show = true;
        UI_C.Chase_Show = true;
        UI_C.UI_Refresh();
    }


    public void Chase_Perform()
    {
        //关于追逐的部分表演都在这里了
        Chase_Material.SetFloat("_Scale_Index", (1-Current_Time / Chase_Time)*0.7f);


        Blood_Material.SetFloat("_Alpha_Scale", Current_Time / Chase_Time);
    }


}
