using System.Collections.Generic;
public class StageUnlockPlantCommand:Singleton<StageUnlockPlantCommand>
{
    private StageUnlockPlantModel model;
    private StageUnlockPlantCommand()
    {
        model = ModelContainer.Instance.GetModel<StageUnlockPlantModel>();
    }
    public List<PlantType> GetPlantTypes(int StageId)
    {
        List<PlantType> list = new List<PlantType>();
        foreach (UnlockPlantData data in model.data)
        {
            if (data.StageId <= StageId&&data.PlantType!=PlantType.None)
            {
                list.Add(data.PlantType);
            }
        }
        return list;
    }
    public PlantType GetUnlockPlantAfterWin(int StageId)
    {
        foreach (UnlockPlantData data in model.data)
        {
            if (data.StageId == StageId + 1)
            {
                return data.PlantType;
            }
        }
        return PlantType.None;
    }
}
