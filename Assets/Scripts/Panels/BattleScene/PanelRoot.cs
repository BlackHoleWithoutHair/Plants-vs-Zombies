using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace BattleScene
{
    public class PanelRoot : IPanel
    {
        private bool isGameStart;
        private bool isCardClick;
        private bool isFirstZombieAppear;
        private TextMeshProUGUI TextSun;
        private RectTransform DivCards;
        private RectTransform DivSelectCards;
        private RectTransform ButtonMenu;
        private Slider sliderProcess;
        private MiddleImageUI m_MiddleImageUI;
        private DialogueUI m_DialogueUI;
        private SelectCardManager m_SelectCardManager;
        private CardManager m_CardManager;
        public PanelRoot() : base(null)
        {
            name = "PanelRoot";
            m_GameObject = m_Canvas.Find(name).gameObject;
            sliderProcess = m_GameObject.transform.Find("SliderProgress").GetComponent<Slider>();
            children.Add(new PanelMenu(this));
            children.Add(new PanelAward(this));
            children.Add(new PanelFail(this));
        }
        protected override void OnInit()
        {
            base.OnInit();
            OnResume();
            TextSun = UnityTool.Instance.GetComponentFromChild<TextMeshProUGUI>(gameObject, "SunNum");
            DivCards = UnityTool.Instance.GetComponentFromChild<RectTransform>(gameObject, "DivCards");
            DivSelectCards = UnityTool.Instance.GetComponentFromChild<RectTransform>(gameObject, "DivSelectCards");
            ButtonMenu = UnityTool.Instance.GetComponentFromChild<RectTransform>(gameObject, "ButtonMenu");

            UnityTool.Instance.GetComponentFromChild<Button>(DivSelectCards.gameObject, "ButtonStart").onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                m_CardManager.AddCardBySelectedCards(m_SelectCardManager.GetSelctPlants());
                Mediator.Instance.GetController<CameraController>().MoveCameraLeft();
                DivSelectCards.DOAnchorPosY(-508, 0.3f);
            });
            EventCenter.Instance.RegisterObserver(EventType.OnCameraMoveLeftFinish, () =>
            {
                ButtonMenu.DOAnchorPosY(0, 0.3f);
                DivCards.DOAnchorPosY(0, 0.3f);
            });
            EventCenter.Instance.RegisterObserver(EventType.OnCameraMoveRightFinish, () =>
            {
                if (ArchiveCommand.Instance.StageId >= 7)
                {
                    ButtonMenu.DOAnchorPosY(0, 0.3f);
                    DivSelectCards.DOAnchorPosY(0, 0.3f);
                    DivCards.DOAnchorPosY(0, 0.3f);
                }
            });
            EventCenter.Instance.RegisterObserver(EventType.OnFirstZombieAppear, () =>
            {
                isFirstZombieAppear = true;
                sliderProcess.transform.gameObject.SetActive(true);
            });

            m_MiddleImageUI = new MiddleImageUI();
            m_DialogueUI = new DialogueUI();
            m_SelectCardManager = new SelectCardManager();
            m_CardManager = new CardManager();

            m_MiddleImageUI.GameStart();
            m_DialogueUI.GameStart();
            m_SelectCardManager.GameStart();
            m_CardManager.GameStart();

            string BigStage = ArchiveCommand.Instance.GetBigStage().ToString();
            string SmallStage = ArchiveCommand.Instance.GetSmallStage().ToString();
            m_GameObject.transform.Find("TextStage").GetComponent<TextMeshProUGUI>().text = "Level " + BigStage + "-" + SmallStage;

            ButtonMenu.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Mediator.Instance.isFail)
                {
                    SceneModelCommand.Instance.LoadScene(SceneName.MainMenuScene);
                }
                else
                {
                    EnterPanel("PanelMenu");
                    if (Mediator.Instance.isGameStart == false)
                    {
                        AudioUtility.Instance.PlayOneShot("gravebutton");
                    }
                    else
                    {
                        AudioUtility.Instance.PlayOneShot("pause");
                    }
                    Time.timeScale = 0f;
                }
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
            m_SelectCardManager.GameUpdate();
            m_CardManager.GameUpdate();
            if (m_DialogueUI.isDialogueEnd() == false)
            {
                m_DialogueUI.GameUpdate();
            }
            if (m_DialogueUI.isDialogueEnd())
            {
                m_MiddleImageUI.GameUpdate();
                if (!Mediator.Instance.isGameStart)
                {
                    Mediator.Instance.GetController<CameraController>().TurnOnController();
                }
                else
                {
                    if (isGameStart == false)
                    {
                        isGameStart = true;
                        int MaxProcessId;
                        GameObject m_flag = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "Flag");
                        MaxProcessId = Mediator.Instance.GetController<WaveController>().GetMaxProcessId();
                        for (int i = 0; i < MaxProcessId - 1; i++)
                        {
                            GameObject flag = Object.Instantiate(m_flag, m_flag.transform.parent);
                            flag.SetActive(true);
                            RectTransform rect = flag.GetComponent<RectTransform>();
                            rect.anchoredPosition = Mediator.Instance.GetController<WaveController>().GetFlagPosition()[i];
                        }
                        m_GameObject.transform.Find("TextStage").gameObject.SetActive(true);
                    }
                    TextSun.text = UIModelCommand.Instance.SunNum.ToString();
                    if (isFirstZombieAppear)
                    {
                        sliderProcess.value = Mediator.Instance.GetController<WaveController>().GetProgressValue();
                    }
                    if (isCardClick)
                    {
                        EnterPanel("PanelAward");
                    }
                    if (m_MiddleImageUI.GetIsZombieWonDisappear())
                    {
                        EnterPanel("PanelFail");
                    }
                }
            }
        }
        public void SetCardClick()
        {
            isCardClick = true;
        }
        public SelectCardManager GetSelectCardManager()
        {
            return m_SelectCardManager;
        }
    }


}
