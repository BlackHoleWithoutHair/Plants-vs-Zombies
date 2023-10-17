using UnityEngine.UI;
namespace BattleScene
{
    public class PanelFail : IPanel
    {
        public PanelFail(IPanel parent) : base(parent)
        {
            name = "PanelFail";
            m_GameObject = m_Canvas.transform.Find(name).gameObject;
        }
        protected override void OnInit()
        {
            base.OnInit();
            m_GameObject.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneModelCommand.Instance.LoadScene(SceneName.BattleScene);
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
