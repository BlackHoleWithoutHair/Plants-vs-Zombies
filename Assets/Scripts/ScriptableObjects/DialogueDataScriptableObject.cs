using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData", order = 5)]
public class DialogueDataScriptableObject : ScriptableObject
{
    public List<Dialogue> dialogues;
}
[System.Serializable]
public class Dialogue
{
    public int StageId;
    public List<DialogueData> Datas;
}
[System.Serializable]
public class DialogueData
{
    public int index;
    public string name;
    [TextArea]
    public string text;
    public List<int> nexts;
}

