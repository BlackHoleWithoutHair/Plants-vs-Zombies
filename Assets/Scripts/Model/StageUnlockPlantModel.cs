using System.Collections.Generic;

public class StageUnlockPlantModel:AbstractModel
{
    public List<UnlockPlantData> data;
    protected override void OnInit()
    {
        base.OnInit();
        data = ResourcesFactory.GetData<StageUnlockPlantScriptableObject>().unlockPlantDatas;
    }
    
}
