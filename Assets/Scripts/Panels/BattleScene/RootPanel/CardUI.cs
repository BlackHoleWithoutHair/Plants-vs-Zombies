using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace BattleScene
{
    public class CardUI
    {
        protected Button m_Button;
        protected Image m_Image;
        private GameObject gameObject;
        private PlantType type;
        private float CumulativeTime;
        private bool WantPlant;
        private Vector3 ClickPosition;
        private Vector3 MousePosition;
        private int spend;
        private float coolTime;
        private GameObject PlantImage;
        public CardUI(GameObject obj,PlantType type)
        {
            gameObject = obj;
            this.type = type;
        }
        public virtual void GameStart()
        {
            m_Button = gameObject.GetComponent<Button>();
            if(ArchiveCommand.Instance.StageId<7)
            {
                m_Button.GetComponent<Image>().color = new Color(180f / 255f, 180f / 255f, 180f / 255f, 180f / 255f);
            }
            else
            {
                m_Button.GetComponent<Image>().DOColor(new Color(180f / 255f, 180f / 255f, 180f / 255f, 180f / 255f), 0.3f);
            }
            EventCenter.Instance.RegisterObserver(EventType.OnGameStart, () =>
            {
                m_Button.enabled = true;
                m_Button.GetComponent<Image>().DOColor(Color.white, 0.3f);
            });
            m_Image = gameObject.transform.Find("Image").GetComponent<Image>();
            m_Image.fillAmount = 0;
            spend = (int)AttributeFactory.Instance.GetPlantAttribute(type).ShareAttr.Spend;
            coolTime = AttributeFactory.Instance.GetPlantAttribute(type).ShareAttr.PlantCoolTime;
            m_Button.onClick.RemoveAllListeners();
            m_Button.onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("seedlift");
                WantPlant = true;
                PlantImage = CharacterFactory.GetPlantImage(type);
                m_Button.interactable = false;
            });
            m_Button.enabled = false;
        }
        public virtual void GameUpdate()
        {
            if (UIModelCommand.Instance.SunNum < spend || CumulativeTime != 0f)
            {
                m_Button.interactable = false;
            }
            else if (WantPlant == false)
            {
                m_Button.interactable = true;
            }
            if (WantPlant == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    WantPlant = false;
                    AudioUtility.Instance.PlayOneShot("tap2");
                    Object.Destroy(PlantImage);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    ClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    ClickPosition.Set(ClickPosition.x, ClickPosition.y, 0);
                    if (Mediator.Instance.GetSystem<GroundSystem>().isWorldPositionInPlantArea(ClickPosition))
                    {
                        if (Mediator.Instance.GetSystem<GroundSystem>().GetIsHavingPlant(ClickPosition) == false)
                        {
                            Mediator.Instance.GetController<PlantController>().Plant(type, Mediator.Instance.GetSystem<GroundSystem>().WorldPositionToGroundWorldPosition(ClickPosition));
                            UIModelCommand.Instance.SpendSun(spend);
                            CumulativeTime = coolTime;
                            WantPlant = false;
                            Object.Destroy(PlantImage);
                        }
                    }
                }
                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MousePosition.Set(MousePosition.x, MousePosition.y, 0);
                if (Mediator.Instance.GetSystem<GroundSystem>().isWorldPositionInPlantArea(MousePosition))
                {
                    PlantImage.transform.position = Mediator.Instance.GetSystem<GroundSystem>().WorldPositionToGroundWorldPosition(MousePosition);
                }
                else
                {
                    PlantImage.transform.position = MousePosition;
                }
            }
            if (CumulativeTime != 0f)
            {
                CumulativeTime -= Time.deltaTime;
                CumulativeTime = Mathf.Clamp(CumulativeTime, 0f, 100f);
                m_Image.fillAmount = CumulativeTime / coolTime;
            }
        }
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public bool GetIsWantPlant()
        {
            return WantPlant;
        }
        public void SetButtonInteractable()
        {
            m_Button.interactable = false;
        }
    }
}

