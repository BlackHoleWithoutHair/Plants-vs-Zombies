using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelAlmanac : IPanel
    {
        public PanelAlmanac(IPanel parent) : base(parent)
        {
            name = "PanelAlmanac";
            m_GameObject = m_Canvas.transform.Find(name).gameObject;
            children.Add(new PanelAlmanacPlant(this));
            children.Add(new PanelAlmanacZombie(this));
        }
        protected override void OnInit()
        {
            base.OnInit();
            m_GameObject.transform.Find("ButtonClose").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                OnExit();
            });
            m_GameObject.transform.Find("ButtonPlant").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                EnterPanel("PanelAlmanacPlant");
            });
            m_GameObject.transform.Find("ButtonZombie").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("tap");
                EnterPanel("PanelAlmanacZombie");
            });
        }
    }
}

