using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardDescribe
{
    public PlantType plantType;
    [TextArea]
    public string describe;
}
[CreateAssetMenu(fileName = "CardDescribe", menuName = "ScriptableObjects/CardDescribe", order = 4)]
public class CardDescribeScriptableObject : ScriptableObject
{
    public List<CardDescribe> cardDescribes = new List<CardDescribe>();
}
