using System.Collections.Generic;
using UnityEngine;
public enum PlantType
{
    None,
    SunFlower,
    PeaShooter,
    CherryBomb,
    WallNut,
    PotatoMine,
}
public enum ZombieType
{
    Zombie,
    FlagZombie,
}
public class AttributeFactory
{
    private PlantDataScriptableObject PlantData;
    private ZombieDataScriptableObject ZombieData;
    private StageDataScriptableObject StageData;
    private CardDescribeScriptableObject CardDescribe;
    private DialogueDataScriptableObject DialogueData;
    public AttributeFactory()
    {
        PlantData = ResourcesFactory.GetData<PlantDataScriptableObject>();
        ZombieData = ResourcesFactory.GetData<ZombieDataScriptableObject>();
        StageData = ResourcesFactory.GetData<StageDataScriptableObject>();
        CardDescribe = ResourcesFactory.GetData<CardDescribeScriptableObject>();
        DialogueData = ResourcesFactory.GetData<DialogueDataScriptableObject>();
    }
    private static AttributeFactory instance;
    public static AttributeFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AttributeFactory();
                return instance;
            }
            else
            {
                return instance;
            }
        }
    }

    public Stage GetStage(int id)
    {
        foreach (Stage stage in StageData.Stages)
        {
            if (stage.StageId == id)
            {
                return stage;
            }
        }
        Debug.Log("AttrFactory GetStage return null");
        return null;
    }
    private PlantShareAttr GetPlantProperty(PlantType type)
    {

        foreach (PlantShareAttr property in PlantData.PlantShareAttrs)
        {
            if (property.Type == type)
            {
                return property;
            }
        }
        Debug.Log("AttrFactory GetPlantProperty return null");
        return null;
    }
    private ZombieShareAttr GetZombieProperty(ZombieType type)
    {
        foreach (ZombieShareAttr property in ZombieData.ZombieShareAttrs)
        {
            if (property.Type == type)
            {
                return property;
            }
        }
        Debug.Log("AttrFactory GetZombieProperty return null");
        return null;
    }
    public List<DialogueData> GetDialogue(int StageId)
    {
        foreach (Dialogue dialogue in DialogueData.dialogues)
        {
            if (dialogue.StageId == StageId)
            {
                return dialogue.Datas;
            }
        }
        Debug.Log("AttributeFactory GetDialogue return null");
        return null;
    }
    public PlantAttribute GetPlantAttribute(PlantType type)
    {
        return new PlantAttribute(GetPlantProperty(type));
    }
    public ZombieAttribute GetZombieAttribute(ZombieType type)
    {
        return new ZombieAttribute(GetZombieProperty(type));
    }
    public CardDescribe GetCardDescribe(PlantType type)
    {
        foreach (CardDescribe describe in CardDescribe.cardDescribes)
        {
            if (describe.plantType == type)
            {
                return describe;
            }
        }
        Debug.Log("AttrbuteFactory GetCardDescribe return null");
        return null;
    }
}
