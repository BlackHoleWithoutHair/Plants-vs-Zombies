using UnityEngine;
using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelQuit : IPanel
    {
        private Button ButtonCancel;
        private Button ButtonQuit;
        public PanelQuit(IPanel parent) : base(parent)
        {
            name = "PanelQuit";
            m_GameObject = UnityTool.Instance.GetGameObjectInChild(m_Canvas.gameObject, name);
            ButtonCancel = m_GameObject.transform.Find("ButtonCancel").GetComponent<Button>();
            ButtonQuit = m_GameObject.transform.Find("ButtonQuit").GetComponent<Button>();
        }
        protected override void OnInit()
        {
            base.OnInit();
            ButtonCancel.onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("gravebutton");
                OnExit();
            });
            ButtonQuit.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
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
