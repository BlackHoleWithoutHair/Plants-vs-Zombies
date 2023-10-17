using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BattleScene;

public class SelectCardUI
{
    private PlantType m_PlantType;
    private SelectCardManager manager;
    private GameObject VirtualCardContainer;
    private GameObject CardContainer;
    private GameObject gameObject;
    private GameObject newCard;
    public SelectCardUI(SelectCardManager manager,GameObject obj,PlantType type)
    {
        this.manager = manager;
        m_PlantType = type;
        gameObject = obj;
    }
    public void GameStart()
    {
        VirtualCardContainer = UnityTool.Instance.GetGameObjectFromCanvas("VirtualCardContainer");
        CardContainer = UnityTool.Instance.GetGameObjectInChild(UnityTool.Instance.GetGameObjectFromCanvas("DivCards"), "CardContainer");
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioUtility.Instance.PlayOneShot("tap");
            manager.SelectedCardNum += 1;
            Vector3 taget = VirtualCardContainer.transform.GetChild(manager.SelectedCardNum-1).position;
            newCard = Object.Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            newCard.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, gameObject.GetComponent<RectTransform>().sizeDelta.y);
            newCard.transform.DOMove(taget, 0.3f).OnComplete(() =>
            {
                manager.AddSelectPlant(newCard,m_PlantType);
                newCard.transform.SetParent(CardContainer.transform);
                newCard.GetComponent<Button>().onClick.AddListener(() =>
                {
                    AudioUtility.Instance.PlayOneShot("tap");
                    newCard.GetComponent<Button>().enabled = false;
                    manager.SelectedCardNum -= 1;
                    GameObject newBackCard = Object.Instantiate(newCard, newCard.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
                    newCard.GetComponent<Image>().color=new Color(0,0,0,0);
                    newCard.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 0), 0.2f).OnComplete(() =>
                    {
                        Object.Destroy(newCard);
                    });
                    newBackCard.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, gameObject.GetComponent<RectTransform>().sizeDelta.y);
                    CoroutinePool.Instance.StartCoroutine(WaitForSetButtonInteractable());
                    newBackCard.transform.DOMove(gameObject.transform.position, 0.3f).OnComplete(() =>
                    {
                        manager.RemoveSelectPlant(m_PlantType);
                        gameObject.GetComponent<Button>().interactable = true;
                        Object.Destroy(newBackCard);
                    });
                });
            });
            gameObject.GetComponent<Button>().interactable = false;
        });
    }
    public void GameUpdate() { }
    private IEnumerator WaitForSetButtonInteractable()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Button>().interactable = true;
    }
}
