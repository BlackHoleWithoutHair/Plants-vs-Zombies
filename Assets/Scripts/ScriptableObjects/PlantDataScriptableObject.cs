using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlantData", order = 1)]
public class PlantDataScriptableObject : ScriptableObject
{
    public TextAsset textAsset;
    public List<PlantShareAttr> PlantShareAttrs=new List<PlantShareAttr>();
    private void OnValidate()
    {
        UnityTool.Instance.WriteDataToListFromTextAsset(PlantShareAttrs, textAsset);
    }
}
