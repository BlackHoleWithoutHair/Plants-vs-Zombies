using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelDelete : IPanel
    {
        public PanelDelete(IPanel parent) : base(parent)
        {
            name = "PanelDelete";
            m_GameObject = UnityTool.Instance.GetGameObjectInChild(parent.gameObject, name);
        }
        protected override void OnInit()
        {
            base.OnInit();
            UnityTool.Instance.GetComponentFromChild<Button>(m_GameObject, "ButtonYes").onClick.AddListener(() =>
            {
                for (int i = 0; i < ArchiveCommand.Instance.NameList.Count; i++)
                {
                    string name = (parent as PanelNameList).GetEditText();
                    if (ArchiveCommand.Instance.NameList[i] == name)
                    {
                        ArchiveCommand.Instance.NameList.RemoveAt(i);
                    }
                    if (name == ArchiveCommand.Instance.UserName)
                    {
                        ArchiveCommand.Instance.UserName = null;
                    }
                }
                OnExit();
            });
            UnityTool.Instance.GetComponentFromChild<Button>(m_GameObject, "ButtonNo").onClick.AddListener(() =>
            {
                OnExit();
            });
        }
    }

}
