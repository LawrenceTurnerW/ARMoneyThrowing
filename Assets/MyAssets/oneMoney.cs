using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneMoney : MonoBehaviour
{
    bool touchTheGround = false;
    float rnd;
    int inversion;

    float rot = 0;
    void Start()
    {
        inversion = (Random.Range(0, 2) == 0) ? 1 : -1;
        rnd = Random.Range(90.0f, 180.0f);
        // 何があっても5秒後に削除
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!touchTheGround)
        {
            this.transform.Rotate(0, 0, rnd * inversion * Time.deltaTime);
            rot += rnd * Time.deltaTime;
            if (rot >= rnd)
            {
                inversion = inversion * -1;
                rot = 0;
                // 重力を少量変化させる
                float rndomGravity = Random.Range(-10.0f, -20.0f);
                Physics.gravity = new Vector3(0, rndomGravity, 0);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
            touchTheGround = true;
        }
    }
    public static bool RandomBool()
    {
        return Random.Range(0, 2) == 0;
    }
}
