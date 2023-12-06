using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FunGenerator : MonoBehaviour
{
    const int StageChipSize = 130;
    int currentChipIndex;

    public Transform character;
    public GameObject[] stageChips;
    public int StartChipIndex;
    public int preInstantiate;
    public List<GameObject> generatedStageList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        currentChipIndex = StartChipIndex - 1;
        UpdateStage(preInstantiate);

    }

    // Update is called once per frame
    void Update()
    {
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }

    }

    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            generatedStageList.Add(stageObject);
        }

        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();
        currentChipIndex = toChipIndex;
    }

    GameObject GenerateStage(int ChipIndex)
    {
        int nextStageChip = Random.Range(0, stageChips.Length);
        GameObject stageObject = (GameObject)Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, ChipIndex * StageChipSize),
            Quaternion.identity
        );
        return stageObject;
    }

    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
