using TMPro;
using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelRename : IPanel
    {
        private TMP_InputField input;
        private TextMeshProUGUI placeHolder;
        public PanelRename(IPanel parent) : base(parent)
        {
            name = "PanelRename";
            m_GameObject = UnityTool.Instance.GetGameObjectInChild(parent.gameObject, name);
            input = UnityTool.Instance.GetComponentFromChild<TMP_InputField>(m_GameObject, "InputField");
            placeHolder = UnityTool.Instance.GetComponentFromChild<TextMeshProUGUI>(m_GameObject, "Placeholder");
        }
        protected override void OnInit()
        {
            base.OnInit();
            UnityTool.Instance.GetComponentFromChild<Button>(m_GameObject, "ButtonOk").onClick.AddListener(() =>
            {
                if (input.text.Length == 0)
                {
                    placeHolder.text = "name can't be empty";
                }
                else
                {
                    (parent as PanelNameList).SetEditText(input.text);
                    OnExit();
                }
            });
            UnityTool.Instance.GetComponentFromChild<Button>(m_GameObject, "ButtonCancel").onClick.AddListener(() =>
            {
                OnExit();
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            input.text = (parent as PanelNameList).GetEditText();
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}

