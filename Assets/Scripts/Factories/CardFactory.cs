using System.Collections.Generic;
using UnityEngine;

public class CardFactory
{
    public static GameObject GetCard()
    {
        int stageId = ArchiveCommand.Instance.StageId;
        GameObject obj = ResourcesFactory.GetCard(StageUnlockPlantCommand.Instance.GetUnlockPlantAfterWin(stageId).ToString());
        if(obj!=null)
        {
            return Object.Instantiate(obj);
        }
        return null;
    }
    public static string GetCardDescribe()
    {
        int stageId = ArchiveCommand.Instance.StageId;
        Stage stage = AttributeFactory.Instance.GetStage(stageId);
        return AttributeFactory.Instance.GetCardDescribe(stage.AwardCard).describe;
    }
}
