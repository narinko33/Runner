using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 40;

    int currentChipIndex;

    public Transform character;  //ターゲットキャラクターの指定
    public GameObject[] stageChips;  //ステージチッププレハブ配列
    public List<GameObject> generatedStageList = new List<GameObject>();  //生成済みステージチップ保持リスト
    public int startChipIndex;  //自動生成開始インデックス
    public int preInstantiate;  //生成先読み個数
    

    // Start is called before the first frame update
    void Start()  //初期化処理
    {
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()  //ステージ更新タイミングの監視
    {
        //キャラクター位置から現在のステージチップのインデックス計算
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        //次のステージチップに入ったらステージの更新処理を行う
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    //指定のIndexまでのステージチップを生成して、管理下に置く
    void UpdateStage(int toChipIndex)  //ステージの更新処理
    {
        if (toChipIndex <= currentChipIndex) return;

        //指定のステージチップまでを作成
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            //生成したステージチップを管理リストに追加
            generatedStageList.Add(stageObject);
        }

        //ステージ保持上限になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentChipIndex = toChipIndex;
    }

    //指定のIndex位置にStageオブジェクトをランダムに生成
    GameObject GenerateStage(int chipIndex)  //ステージの生成処理
    {
        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject = (GameObject)Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity
        );


        return stageObject;
    }

    //一番古いステージを削除
    void DestroyOldestStage()  //ステージの削除処理
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);

    }
}
