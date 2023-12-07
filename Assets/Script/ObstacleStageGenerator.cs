using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStageGenerator : MonoBehaviour
{
    const float StageChipSize = 128.55f;
    int currentChipIndex;

    public Transform character;//ターゲットキャラ設定
    public GameObject[] stageChips;//ステージチッププレハブ配列
    public int startChipIndex;//自動生成開始インデックス
    public int preInstantiate;//生成先読み個数
    public List<GameObject> generatedStageList = new List<GameObject>();//生成済みステージチップ保持リスト

    // Start is called before the first frame update
    void Start()
    {
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);

    }

    void Update()
    {
        // キャラの位置から現在のステージチップのインデックスを計算
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        // 次のステージチップ入ったらステージの更新処理を行う
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);

        }

    }

    // 指定のIndexまでのステージチップを生成、管理下に置く
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;

        // 指定のステージチップまでを作成
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            // 生成したステージチップを管理リストに追加
            generatedStageList.Add(stageObject);

        }
        // ステージ保持上限内になるまで古いステージ削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();
        currentChipIndex = toChipIndex;
    }

    // 指定インデックス位置にStageオブジェクトをランダム生成
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageOject = (GameObject)Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity);
        return stageOject;
    }

    // 一番古いステージ削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
