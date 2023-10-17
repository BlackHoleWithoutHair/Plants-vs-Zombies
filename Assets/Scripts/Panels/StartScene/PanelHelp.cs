using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelHelp : IPanel
    {
        private CanvasGroup group;
        private Button m_Button;
        public PanelHelp(IPanel parent) : base(parent)
        {
            name = "PanelHelp";
            m_GameObject = UnityTool.Instance.GetGameObjectInChild(m_Canvas.gameObject, "PanelHelp");
            m_Button = m_GameObject.transform.Find("ButtonMainMenu").GetComponent<Button>();
        }
        protected override void OnInit()
        {
            base.OnInit();
            group = m_GameObject.GetComponent<CanvasGroup>();
            group.alpha = 0;
            m_Button.onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                m_GameObject.gameObject.SetActive(false);
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            group.DOFade(1, 0.5f);
        }
        public override void OnExit()
        {
            base.OnExit();
            group.alpha = 0;
        }
    }
}
