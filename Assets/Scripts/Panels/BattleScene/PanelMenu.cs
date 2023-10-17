using UnityEngine;
using UnityEngine.UI;
namespace BattleScene
{
    public class PanelMenu : IPanel
    {
        private Slider sliderMusic;
        private Slider sliderSound;
        public PanelMenu(IPanel parent) : base(parent)
        {
            name = "PanelMenu";
            m_GameObject = m_Canvas.Find(name).gameObject;
            sliderMusic = m_GameObject.transform.Find("SliderMusic").GetComponent<Slider>();
            sliderSound = m_GameObject.transform.Find("SliderSound").GetComponent<Slider>();
        }
        protected override void OnInit()
        {
            base.OnInit();
            m_GameObject.transform.Find("ButtonBackGame").GetComponent<Button>().onClick.AddListener(() =>
            {
                OnExit();
                AudioUtility.Instance.PlayOneShot("buttonclick");
                Time.timeScale = 1.0f;
            });
            m_GameObject.transform.Find("ButtonMainMenu").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("buttonclick");
                Time.timeScale = 1.0f;
                SceneModelCommand.Instance.LoadScene(SceneName.MainMenuScene);
            });
            m_GameObject.transform.Find("ButtonRestart").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioUtility.Instance.PlayOneShot("buttonclick");
                Time.timeScale = 1.0f;
                SceneModelCommand.Instance.LoadScene(SceneName.BattleScene);
            });
            m_GameObject.transform.Find("SliderMusic").GetComponent<Slider>().onValueChanged.AddListener(val =>
            {
                ArchiveCommand.Instance.MusicVolume = val;
                AudioUtility.Instance.SetBgmVolume(val);
            });
            m_GameObject.transform.Find("SliderSound").GetComponent<Slider>().onValueChanged.AddListener(val =>
            {
                ArchiveCommand.Instance.SoundVolume = val;
                AudioUtility.Instance.SetSfxVolume(val);
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            sliderMusic.value = ArchiveCommand.Instance.MusicVolume;
            sliderSound.value = ArchiveCommand.Instance.SoundVolume;
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}

