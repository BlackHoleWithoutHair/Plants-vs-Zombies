using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StateData", order = 3)]
public class StageDataScriptableObject : ScriptableObject
{
    public List<Stage> Stages;
}
[System.Serializable]
public class Stage
{
    public int StageId;
    public PlantType AwardCard;
    public List<StageProperty> StageProperties;
}
[System.Serializable]
public class StageProperty
{
    public ZombieType Type;
    public int ProcessId;
    public float CreateTime;
    [NonSerialized]
    private bool isAlreadyCreate = false;
    public void SetIsCreate()
    {
        isAlreadyCreate = true;
    }
    public bool GetIsCreate()
    {
        return isAlreadyCreate;
    }
}
