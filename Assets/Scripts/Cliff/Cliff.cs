using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour
{
    //�п��ܳ�Ϊ���µĵط�������ط���ĳ��������������Gravity_On�ı仯���仯
    //�������ӽǣ�Ҳ����Gravity_On=false��ʱ�����±����Ϊ�޷�ͨ����Ҳ����BoxCollider2D.isTrigger=false;
    //�������ᣬ��ʱ���µ��谭���Ƴ���BoxCollider2D.isTrigger=true��Ȼ�����ʱ����ҾͿ���ͨ����

    //һ��������ΪCliff��BoxColliderҪ������������������ֱ�ӱ�GetComponent<BoxCollider2D>()��ָ��Ҳ�����㹦�ܵı�д

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
            //��ʱ��ҪTrigger=true��
           Current_Collider.isTrigger = true;
        }
        else
        {
            Current_Collider.isTrigger =false;
        }
    }
    
}
