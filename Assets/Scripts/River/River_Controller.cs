using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River_Controller : MonoBehaviour
{
    //这个类用来圈定当前ParticleSystem的遮罩由某个Box Collider 2D控制

    [Header("当前粒子系统")]
    public ParticleSystem Current_Particle_System;

    [Header("当前触发器")]
    public BoxCollider2D Current_Trigger_Collider;

    public BoxCollider2D Current_Box_Collider;

    [Header("当前Player控制器")]
    public Player Current_Player;

    [Header("当前桥（GameObject）")]
    public GameObject Current_Bridge;

    public Vector2 Classic_Offset = new Vector2(1.09f, 0.10f);

    public Vector2 Classic_Size = new Vector2(4.50f, 11.92f);


    public void Start()
    {
        Current_Particle_System = GetComponent<ParticleSystem>();

        Current_Trigger_Collider = GetComponents<BoxCollider2D>()[1];

        Current_Box_Collider = GetComponents<BoxCollider2D>()[0];

    }

    public void Update()
    {

        if(Current_Player.Gravity_On==false)
        {
            Current_Trigger_Collider.offset = Classic_Offset;

            Current_Trigger_Collider.size = Classic_Size;

            Current_Box_Collider.offset = Classic_Offset;

            Current_Box_Collider.size = Classic_Size;
            return;
        }
        Reset_Collider_Position();
        Remove_Out_Particle();
    }

    public void Reset_Collider_Position()
    {
        //首先，X不需要变，只需要计算Y的变动就可以了，其中包括BoxCollider2D.offset.y,BoxCollider2D.size.y
        //首先需要计算新的BoxCollider的的size.y的宽度是多少

        float Upper_Y = gameObject.transform.position.y + Current_Trigger_Collider.offset.y + Current_Trigger_Collider.size.y * 0.5f;

        float Lower_Y = Current_Bridge.transform.position.y;

        

        float New_Y_Size = Upper_Y - Lower_Y;

        Debug.Log("New=" + New_Y_Size);

        Current_Trigger_Collider.offset = new Vector2(Current_Trigger_Collider.offset.x, (Upper_Y + Lower_Y) / 2f);

        Current_Trigger_Collider.size = new Vector2(Current_Trigger_Collider.size.x, New_Y_Size);


        Current_Box_Collider.offset = new Vector2(Current_Trigger_Collider.offset.x, (Upper_Y + Lower_Y) / 2f);

        Current_Box_Collider.size = new Vector2(Current_Trigger_Collider.size.x, New_Y_Size);

    }






    public void Remove_Out_Particle()
    {
        //字面意思，移除触发器以外的Particle

        bool particlesChanged = false;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[Current_Particle_System.main.maxParticles];//当前粒子

        int numParticles = Current_Particle_System.GetParticles(particles);

        for (int i = 0; i < numParticles; i++)
        {
            // 将粒子位置转换为世界坐标
            Vector3 particleWorldPos = Current_Particle_System.transform.TransformPoint(particles[i].position);

            // 检查粒子是否在碰撞器内
            bool isInside = Current_Trigger_Collider.OverlapPoint(particleWorldPos);

            if(!isInside)
            {
                // 不在碰撞器内，将剩余生命周期设为0
                particles[i].remainingLifetime = 0f;
                particlesChanged = true;
            }
        }

        if (particlesChanged)
        {
            Current_Particle_System.SetParticles(particles, numParticles);
        }



    }

}
