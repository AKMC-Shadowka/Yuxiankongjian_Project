using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Ground : MonoBehaviour
{
    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 这里实现的地面检测逻辑
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1f);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                // 遍历每一个被击中的物体
                //Debug.Log($"击中了物体：{hit.collider.gameObject.name}，距离是：{hit.distance}");
                if (hit.collider.gameObject != gameObject)
                {
                    grounded = true;
                    break;
                }
                grounded = false;
            }
        }
    }
}
