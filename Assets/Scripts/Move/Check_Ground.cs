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
        // ����ʵ�ֵĵ������߼�
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1f);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                // ����ÿһ�������е�����
                //Debug.Log($"���������壺{hit.collider.gameObject.name}�������ǣ�{hit.distance}");
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
