using BattleScene;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager
{
    private Transform CardContainer;
    private List<CardUI> m_Cards = new List<CardUI>();
    private CardUI WantPlantCard;
    public CardManager() { }
    public void GameStart()
    {
        CardContainer = GameObject.Find("DivCards").transform.Find("CardContainer");
        if (ArchiveCommand.Instance.StageId < 7)
        {
            foreach (PlantType type in StageUnlockPlantCommand.Instance.GetPlantTypes(ArchiveCommand.Instance.StageId))
            {
                GameObject obj = Object.Instantiate(ResourcesFactory.GetCard(type.ToString()),CardContainer);
                obj.name=type.ToString()+"Card";
                CardUI card = new CardUI(obj, type);
                m_Cards.Add(card);
            }
        }
    }
    public void GameUpdate()
    {
        if (Mediator.Instance.isGameStart)
        {
            if(WantPlantCard==null)
            {
                foreach (CardUI card in m_Cards)
                {
                    card.GameUpdate();
                    if (card.GetIsWantPlant())
                    {
                        WantPlantCard = card;
                        foreach (CardUI other in m_Cards)
                        {
                            if (other != card)
                            {
                                other.SetButtonInteractable();
                            }
                        }
                        break;
                    }

                }
            }
            else if (WantPlantCard.GetIsWantPlant() == false)
            {
                WantPlantCard = null;
            }
            WantPlantCard?.GameUpdate();
        }
    }
    public void AddCardBySelectedCards(List<CardInfo> list)
    {
        foreach(CardInfo card in list)
        {
            CardUI ui = new CardUI(card.gameObject, card.plantType);
            ui.GameStart();
            m_Cards.Add(ui);

        }
    }
}
