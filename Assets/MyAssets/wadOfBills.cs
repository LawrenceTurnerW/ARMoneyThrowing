using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wadOfBills : MonoBehaviour
{
    void Start()
    {
        // 何があっても7秒後に削除
        Destroy(this.gameObject, 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
