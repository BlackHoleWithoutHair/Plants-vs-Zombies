using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelNewUser : IPanel
    {
        private TMP_InputField input;
        private GameObject DivMain;
        private GameObject DivWarning;
        public PanelNewUser(IPanel parent) : base(parent)
        {
            name = "PanelNewUser";
            m_GameObject = UnityTool.Instance.GetGameObjectInChild(parent.gameObject, name);
        }
        protected override void OnInit()
        {
            base.OnInit();
            DivMain = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "DivMain");
            DivWarning = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "DivWarning");
            input = UnityTool.Instance.GetComponentFromChild<TMP_InputField>(DivMain, "InputField");
            UnityTool.Instance.GetComponentFromChild<Button>(DivMain, "ButtonOk").onClick.AddListener(() =>
            {
                if (input.text != null && input.text.Length != 0)
                {
                    ArchiveCommand.Instance.NameList.Add(input.text);
                    ArchiveCommand.Instance.SaveData();
                    OnExit();
                }
                if (ArchiveCommand.Instance.NameList.Count == 0)
                {
                    if (input.text == null || input.text.Length == 0)
                    {
                        DivMain.gameObject.SetActive(false);
                        DivWarning.gameObject.SetActive(true);
                    }
                }
            });
            UnityTool.Instance.GetComponentFromChild<Button>(DivMain, "ButtonCancel").onClick.AddListener(() =>
            {
                if (ArchiveCommand.Instance.NameList.Count != 0)
                {
                    OnExit();
                }
            });
            UnityTool.Instance.GetComponentFromChild<Button>(DivWarning, "ButtonOk").onClick.AddListener(() =>
            {
                DivWarning.gameObject.SetActive(false);
                DivMain.gameObject.SetActive(true);
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();

        }

    }
}

