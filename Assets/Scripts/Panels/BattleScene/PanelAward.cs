using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace BattleScene
{
    public class PanelAward : IPanel
    {
        private GameObject DivAward;
        private Animator m_Animator;
        private GameObject White;
        public PanelAward(IPanel parent) : base(parent)
        {
            name = "PanelAward";
            m_GameObject = m_Canvas.transform.Find(name).gameObject;
            White = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "White");
            White.SetActive(false);
            m_Animator = White.GetComponent<Animator>();
        }
        protected override void OnInit()
        {
            base.OnInit();
            DivAward = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "DivAward");
            m_GameObject.transform.Find("DivAward/Describe").GetComponent<TextMeshProUGUI>().text = CardFactory.GetCardDescribe();
            GameObject obj = CardFactory.GetCard();
            if(obj!=null)
            {
                obj.transform.SetParent(m_GameObject.transform.Find("DivAward"));
                obj.transform.localScale = new Vector3(1f, 1f, 1f);
                obj.GetComponent<RectTransform>().anchoredPosition = m_GameObject.transform.Find("DivAward/Point").GetComponent<RectTransform>().anchoredPosition;
                obj.GetComponent<Image>().SetNativeSize();
            }
            m_GameObject.transform.Find("DivAward/ButtonNext").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneModelCommand.Instance.LoadScene(SceneName.BattleScene);
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Enter");
            White.SetActive(true);
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
            AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("Disappear"))
            {
                DivAward.SetActive(true);
            }
            if (info.IsName("Disappear") && info.normalizedTime > 1)
            {
                White.SetActive(false);
            }
        }
    }

}
