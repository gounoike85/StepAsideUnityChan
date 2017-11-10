using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// *******************************************************************************
// ※アイテムの生成について
// アイテム生成ポイントをUnityChanが通過すると、80～120m先にアイテムを生成する
// アイテム生成ポイントは40mごとに設定
// 最初のアイテム生成ポイントはstartPos変数で設定
// 最後のアイテム生成ポイントはgoalPos変数で設定(ゴール手前40mまで生成する)
// *******************************************************************************

public class ItemGenerator : MonoBehaviour {

    // 各Prefabを入れる
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;

    // スタート地点とゴール地点
    private int startPos = -240;
    private int goalPos = 120;

    // アイテムを出すX軸方向の範囲
    private float posRange = 3.4f;

    // UnityChanの座標
    private float unitychanPos = 0;
    private int unitychanPosZ = 0;

    // アイテムを生成する座標
    private int makeItemsPoint = 0;

    // Use this for initialization
    void Start () {

        // 最初のアイテム生成ポイント
        makeItemsPoint = startPos;

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 unitychanPos = GameObject.Find("unitychan").transform.position;
        unitychanPosZ = Mathf.RoundToInt(unitychanPos.z);

        // ゴール手前40m前までアイテムを生成する
        if (unitychanPosZ == makeItemsPoint && unitychanPosZ < goalPos-80)
        {
            // アイテム生成ポイント確認用
            //Debug.Log(unitychanPosZ);
            //Debug.Log(makeItemsPoint);

            // 80～120m先にアイテムを生成する
            makeItems(unitychanPosZ+80, unitychanPosZ+120);

            // 40m先を次の生成ポイントに設定
            makeItemsPoint = unitychanPosZ + 40;
        }
    }

    // アイテムを生成する関数
    void makeItems(int startRange, int endRange)
    {
        // 一定の距離ごとにアイテムを生成
        for (int i = startRange; i < endRange; i += 20)
        {
            // どのアイテムを出すのかをランダムに設定
            int num = Random.Range(0, 10);

            if (num <= 1)
            {
                // コーンをX軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j,
                                                           cone.transform.position.y,
                                                           i);
                }
            }
            else
            {
                // レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    // アイテムの種類を決める
                    int item = Random.Range(1, 11);

                    // アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);

                    // 60%コイン配置 30%車配置 10%無し
                    if (1 <= item && item <= 6)
                    {
                        // コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j,
                                                               carPrefab.transform.position.y,
                                                               i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        // 車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j,
                                                              car.transform.position.y,
                                                              i + offsetZ);
                    }
                }
            }
        }
    }

}
