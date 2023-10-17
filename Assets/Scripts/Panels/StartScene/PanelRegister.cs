using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelRegister : IPanel
    {
        TMP_InputField input;
        TextMeshProUGUI PlaceHolder;
        private GameObject DivMain;
        private GameObject DivWarning;
        public PanelRegister(IPanel parent) : base(parent)
        {
            name = "PanelRegister";
            m_GameObject = m_Canvas.transform.Find(name).gameObject;
            DivMain = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "DivMain");
            DivWarning = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "DivWarning");
            input = UnityTool.Instance.GetComponentFromChild<TMP_InputField>(DivMain, "InputField");
            PlaceHolder = UnityTool.Instance.GetComponentFromChild<TextMeshProUGUI>(input.gameObject, "Placeholder");

        }
        protected override void OnInit()
        {
            base.OnInit();
            UnityTool.Instance.GetComponentFromChild<Button>(DivMain, "ButtonOk").onClick.AddListener(() =>
            {
                if (input.text.Length == 0 || input.text == null)
                {
                    PlaceHolder.text = "name can't be empty";
                }
                else
                {
                    ArchiveCommand.Instance.UserName = input.text;
                    ArchiveCommand.Instance.NameList.Add(input.text);
                    //ArchiveUtility.Instance.SaveData();
                    m_GameObject.SetActive(false);
                }
            });
            UnityTool.Instance.GetComponentFromChild<Button>(DivMain, "ButtonCancel").onClick.AddListener(() =>
            {
                if (ArchiveCommand.Instance.UserName != null && ArchiveCommand.Instance.UserName.Length != 0)
                {
                    OnExit();
                }
                else
                {
                    DivMain.SetActive(false);
                    DivWarning.SetActive(true);
                }
            });
            UnityTool.Instance.GetComponentFromChild<Button>(DivWarning, "ButtonOk").onClick.AddListener(() =>
            {
                DivMain.SetActive(true);
                DivWarning.SetActive(false);
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();

        }
    }
}

