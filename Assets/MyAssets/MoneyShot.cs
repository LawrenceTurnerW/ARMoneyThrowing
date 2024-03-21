using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Logging;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject lTarget = null;
    [SerializeField] private GameObject rTarget = null;

    [SerializeField] private GameObject cTargetl_1 = null;
    [SerializeField] private GameObject cTargetl_2 = null;
    [SerializeField] private GameObject cTargetr_1 = null;
    [SerializeField] private GameObject cTargetr_2 = null;

    // 発射点
    [SerializeField] private Transform m_lunchPos = null;

    // 発射するプレハブ
    [SerializeField] private GameObject m_moneyPrefab = null;

    // 発射するプレハブ2
    [SerializeField] private GameObject m_moneyPrefab_2 = null;

    private bool canShot = true;

    private GameObject nowBullet;
    bool changeing = false;

    void Start()
    {
        nowBullet = m_moneyPrefab;
    }

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
                GameObject money = Instantiate(nowBullet);

                // 位置と発射方向を発射点に合わせる
                money.transform.position = m_lunchPos.position;
                money.transform.rotation = m_lunchPos.rotation;

                Rigidbody rigidbody = money.GetComponent<Rigidbody>();
                rigidbody.AddFor​​ce(money.transform.right * getForce());
                rigidbody.AddFor​​ce(money.transform.up * Random.Range(-5, 6));

                //再発射不可能にして、0.05秒後に再発射可能にする
                canShot = false;
                StartCoroutine(CanBeShot());
            }
        }

        if (!changeing)
        {
            // 人差し指同士のポジションを取得
            Vector3 cTargetl_1Pos = cTargetl_1.transform.position;
            Vector3 cTargetr_1Pos = cTargetr_1.transform.position;
            // 親指同士のポジションを取得
            Vector3 cTargetl_2Pos = cTargetl_2.transform.position;
            Vector3 cTargetr_2Pos = cTargetr_2.transform.position;
            // 距離を取得
            float dis_1 = Vector3.Distance(cTargetl_1Pos, cTargetr_1Pos);
            float dis_2 = Vector3.Distance(cTargetl_2Pos, cTargetr_2Pos);

            if (dis_1 <= 0.04 && dis_2 < 0.04)
            {
                // 3秒後にも同じ状態であれば
                StartCoroutine(CanChenge());
            }
        }
    }

    // コルーチン
    private int getForce()
    {
        if (nowBullet == m_moneyPrefab)
        {
            return -18;
        }
        return -200;
    }

    // コルーチン
    private IEnumerator CanBeShot()
    {
        yield return new WaitForSeconds(0.05f);
        canShot = true;
    }

    // コルーチン
    private IEnumerator CanChenge()
    {
        changeing = true;
        yield return new WaitForSeconds(3.0f);
        changeing = false;
        // 人差し指同士のポジションを取得
        Vector3 cTargetl_1Pos = cTargetl_1.transform.position;
        Vector3 cTargetr_1Pos = cTargetr_1.transform.position;
        // 親指同士のポジションを取得
        Vector3 cTargetl_2Pos = cTargetl_2.transform.position;
        Vector3 cTargetr_2Pos = cTargetr_2.transform.position;
        // 距離を取得
        float dis_1 = Vector3.Distance(cTargetl_1Pos, cTargetr_1Pos);
        float dis_2 = Vector3.Distance(cTargetl_2Pos, cTargetr_2Pos);

        if (dis_1 <= 0.04 && dis_2 < 0.04)
        {
            if (nowBullet == m_moneyPrefab)
            {
                nowBullet = m_moneyPrefab_2;
                GameObject money = Instantiate(nowBullet);

                // 位置と発射方向を発射点に合わせる
                money.transform.position = m_lunchPos.position;
                money.transform.rotation = m_lunchPos.rotation;
            }
            else
            {
                nowBullet = m_moneyPrefab;
                GameObject money = Instantiate(nowBullet);

                // 位置と発射方向を発射点に合わせる
                money.transform.position = m_lunchPos.position;
                money.transform.rotation = m_lunchPos.rotation;
            }
        }
    }
}
