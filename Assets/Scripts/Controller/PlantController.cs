using BattleScene;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlantController : AbstractController
{
    LawnCleanerManager m_LawnCleanerManager;
    List<IPlant> plants = new List<IPlant>();
    int i;
    public PlantController()
    {
        m_LawnCleanerManager = new LawnCleanerManager(this);
    }
    protected override void Init()
    {
        base.Init();
        PlantsStart();
    }
    protected override void OnAfterRunUpdate()
    {
        base.OnAfterRunUpdate();
        PlantsUpdate();
    }
    private void PlantsStart()
    {
        m_LawnCleanerManager.GameStart();
    }
    private void PlantsUpdate()
    {
        m_LawnCleanerManager.GameUpdate();
        for (i = 0; i < plants.Count; i++)
        {
            plants[i].GameUpdate();
            if (plants[i].ShouldBeRemove)
            {
                Object.Destroy(plants[i].gameObject);
                plants.RemoveAt(i);
            }
        }
    }

    public void Plant(PlantType type, Vector3 position)
    {
        IPlant plant = CharacterFactory.Instance.GetPlant(type, position);
        plants.Add(plant);
    }
}
