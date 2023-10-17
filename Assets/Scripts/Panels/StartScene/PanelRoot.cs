using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelRoot : IPanel
    {
        private TextMeshProUGUI TextUserName;
        public PanelRoot() : base(null)
        {
            name = "PanelRoot";
            m_GameObject = UnityTool.Instance.GetGameObjectFromCanvas(name);

            children.Add(new PanelOption(this));
            children.Add(new PanelHelp(this));
            children.Add(new PanelQuit(this));
            children.Add(new PanelAlmanac(this));
            children.Add(new PanelRegister(this));
            children.Add(new PanelNameList(this));
        }
        protected override void OnInit()
        {
            base.OnInit();
            OnResume();
            TextUserName = UnityTool.Instance.GetComponentFromChild<TextMeshProUGUI>(m_GameObject, "UserName");
            UnityTool.Instance.GetGameObjectInChild(m_GameObject, "ButtonAdventure").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                SceneModelCommand.Instance.LoadScene(SceneName.BattleScene);
            });
            UnityTool.Instance.GetGameObjectInChild(m_GameObject, "ButtonOption").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                EnterPanel("PanelOptions");
            });
            UnityTool.Instance.GetGameObjectInChild(m_GameObject, "ButtonHelp").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                EnterPanel("PanelHelp");
            });
            UnityTool.Instance.GetGameObjectInChild(m_GameObject, "ButtonQuit").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                EnterPanel("PanelQuit");
            });
            m_GameObject.transform.Find("ButtonAlmanac").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                EnterPanel("PanelAlmanac");
            });
            m_GameObject.transform.Find("WelcomeImage/Rename").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                EnterPanel("PanelNameList");
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            UnityTool.Instance.GetGameObjectInChild(m_GameObject, "BigStage").GetComponent<TextMeshProUGUI>().text = ArchiveCommand.Instance.GetBigStage().ToString();
            UnityTool.Instance.GetGameObjectInChild(m_GameObject, "SmallStage").GetComponent<TextMeshProUGUI>().text = ArchiveCommand.Instance.GetSmallStage().ToString();
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (ArchiveCommand.Instance.UserName == null || ArchiveCommand.Instance.UserName.Length == 0)
            {
                EnterPanel("PanelRegister");
            }
            else
            {
                TextUserName.text = ArchiveCommand.Instance.UserName;
            }
        }
    }
}

