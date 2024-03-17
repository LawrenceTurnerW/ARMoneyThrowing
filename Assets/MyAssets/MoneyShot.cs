using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Logging;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject lTarget = null;
    [SerializeField] private GameObject rTarget = null;

    // 発射点
    [SerializeField] private Transform m_lunchPos = null;

    // 発射するプレハブ
    [SerializeField] private GameObject m_moneyPrefab = null;

    private bool canShot = true;

    void Start() { }

    void Update()
    {
        // 発射可能なときのみポジションを取得する
        if (canShot)
        {
            // 右手のポジションを取得
            Vector3 lTargetPos = lTarget.transform.position;
            // 左手ポジションを取得
            Vector3 rTargetPos = rTarget.transform.position;
            // 距離を取得
            float dis = Vector3.Distance(lTargetPos, rTargetPos);

            // もしAとBが5cm以下にあれば発射判定
            if (dis <= 0.05)
            {
                // 左手の中指の正の方向に対して発射する
                GameObject money = Instantiate(m_moneyPrefab);

                // 位置と発射方向を発射点に合わせる
                money.transform.position = m_lunchPos.position;
                money.transform.rotation = m_lunchPos.rotation;

                Rigidbody rigidbody = money.GetComponent<Rigidbody>();
                rigidbody.AddFor​​ce(money.transform.right * -180);

                //再発射不可能にして、0.5秒後に再発射可能にする
                canShot = false;
                StartCoroutine(CanBeShot());
            }
        }
    }

    // コルーチン
    private IEnumerator CanBeShot()
    {
        yield return new WaitForSeconds(0.3f);
        canShot = true;
    }
}
