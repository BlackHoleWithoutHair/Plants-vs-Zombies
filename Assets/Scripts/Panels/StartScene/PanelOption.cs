using UnityEngine;
using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelOption : IPanel
    {
        Transform sliderMusic;
        Transform sliderSound;
        public PanelOption(IPanel parent) : base(parent)
        {
            name = "PanelOptions";
            m_GameObject = m_Canvas.Find(name).gameObject;
            sliderMusic = m_GameObject.transform.Find("SliderMusic");
            sliderSound = m_GameObject.transform.Find("SliderSound");
        }
        protected override void OnInit()
        {
            base.OnInit();
            m_GameObject.transform.Find("ButtonOk").GetComponent<Button>().onClick.AddListener(() =>
            {
                OnExit();
                AudioUtility.Instance.PlayOneShot("buttonclick");
            });
            sliderMusic.GetComponent<Slider>().onValueChanged.AddListener(val =>
            {
                ArchiveCommand.Instance.MusicVolume = val;
                AudioUtility.Instance.SetBgmVolume(val);
            });
            sliderSound.GetComponent<Slider>().onValueChanged.AddListener(val =>
            {
                ArchiveCommand.Instance.SoundVolume = val;
                AudioUtility.Instance.SetSfxVolume(val);
            });
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            sliderMusic.GetComponent<Slider>().value = ArchiveCommand.Instance.MusicVolume;
            sliderSound.GetComponent<Slider>().value = ArchiveCommand.Instance.SoundVolume;
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

    }
}

