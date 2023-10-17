using System.Collections.Generic;
using UnityEngine;
public class CardInfo
{
    public GameObject gameObject;
    public PlantType plantType;
}
public class SelectCardManager
{
    private Transform SelectCardContainer;
    private List<SelectCardUI> m_OptionalCards = new List<SelectCardUI>();
    private List<CardInfo> m_SelectPlants=new List<CardInfo>();
    public int SelectedCardNum = 0;
    public SelectCardManager()
    {
        SelectCardContainer = GameObject.Find("DivSelectCards").transform.Find("CardContainer");
    }
    public void GameStart()
    {
        if (ArchiveCommand.Instance.StageId >= 7)
        {

            foreach (PlantType type in StageUnlockPlantCommand.Instance.GetPlantTypes(ArchiveCommand.Instance.StageId))
            {
                GameObject obj = Object.Instantiate(ResourcesFactory.GetCard(type.ToString()), SelectCardContainer);
                m_OptionalCards.Add(new SelectCardUI(this, obj,type));
            }
            foreach (SelectCardUI card in m_OptionalCards)
            {
                card.GameStart();
            }

        }
    }
    public void GameUpdate()
    {
        if (ArchiveCommand.Instance.StageId >= 7)
        {
            if (!BattleScene.Mediator.Instance.isGameStart)
            {
                foreach (SelectCardUI card in m_OptionalCards)
                {
                    card.GameUpdate();
                }
            }
        }
    }
    public List<CardInfo> GetSelctPlants()
    { 
        return m_SelectPlants;
    }
    public void AddSelectPlant(GameObject obj,PlantType type)
    {
        CardInfo info=new CardInfo();
        info.plantType = type;
        info.gameObject = obj;
        m_SelectPlants.Add(info);
    }
    public void RemoveSelectPlant(PlantType type)
    {
        for(int i=0;i<m_SelectPlants.Count;i++)
        {
            if (m_SelectPlants[i].plantType==type)
            {
                m_SelectPlants.RemoveAt(i);
                break;
            }
        }
    }
}
