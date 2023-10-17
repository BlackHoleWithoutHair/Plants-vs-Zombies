using System.Collections.Generic;

public class ModelContainer : Singleton<ModelContainer>
{
    private List<AbstractModel> models = new List<AbstractModel>();
    private ModelContainer()
    {
        models.Add(new SceneModel());
        models.Add(new ArchiveModel());
        models.Add(new StageUnlockPlantModel());
        models.Add(new UIModel());
    }
    public T GetModel<T>() where T : AbstractModel
    {
        foreach (AbstractModel model in models)
        {
            if (model is T)
            {
                return model as T;
            }
        }
        return null;
    }
}
