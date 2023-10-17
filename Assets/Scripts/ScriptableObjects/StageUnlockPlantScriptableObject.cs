using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UnlockPlantData
{
    public int StageId;
    public PlantType PlantType;
}
[CreateAssetMenu(fileName ="StageUnlockPlantData",menuName ="ScriptableObjects/StageUnlockPlantData")]
public class StageUnlockPlantScriptableObject : ScriptableObject
{
    public TextAsset textAsset;
    public List<UnlockPlantData> unlockPlantDatas=new List<UnlockPlantData>();
    private void OnValidate()
    {
        UnityTool.Instance.WriteDataToListFromTextAsset(unlockPlantDatas, textAsset);
    }
}
