using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelAlmanacPlant : IPanel
    {
        public PanelAlmanacPlant(IPanel parent) : base(parent)
        {
            name = "PanelAlmanacPlant";
            m_GameObject = m_Canvas.transform.Find(name).gameObject;
        }
        protected override void OnInit()
        {
            base.OnInit();
            m_GameObject.transform.Find("ButtonClose").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                OnExit();
                parent.OnExit();

            });
            m_GameObject.transform.Find("ButtonBack").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                OnExit();
            });
        }
    }

}
