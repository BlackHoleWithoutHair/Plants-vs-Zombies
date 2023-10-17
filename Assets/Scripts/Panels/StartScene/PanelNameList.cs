using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenuScene
{
    public class PanelNameList : IPanel
    {
        private GameObject DivMain;
        private GameObject DivNameList;
        private Button ButtonCreate;
        private Button ButtonBeEdit;
        private List<Button> ButtonList;
        private string SelectName;
        private string OriginName;
        public PanelNameList(IPanel parent) : base(parent)
        {
            name = "PanelNameList";
            SelectName = "";
            ButtonList = new List<Button>();
            m_GameObject = UnityTool.Instance.GetGameObjectFromCanvas(name);
            children.Add(new PanelRename(this));
            children.Add(new PanelNewUser(this));
            children.Add(new PanelDelete(this));
        }
        protected override void OnInit()
        {
            base.OnInit();
            DivMain = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "DivMain");
            DivNameList = UnityTool.Instance.GetGameObjectInChild(m_GameObject, "DivNameList");
            ButtonCreate = UnityTool.Instance.GetComponentFromChild<Button>(DivNameList, "ButtonCreate");
            ButtonCreate.onClick.AddListener(() =>
            {
                EnterPanel("PanelNewUser");
            });
            UnityTool.Instance.GetComponentFromChild<Button>(DivMain, "ButtonRename").onClick.AddListener(() =>
            {
                EnterPanel("PanelRename");
            });
            UnityTool.Instance.GetComponentFromChild<Button>(DivMain, "ButtonOk").onClick.AddListener(() =>
            {
                if (SelectName.Length != 0)
                {
                    ArchiveCommand.Instance.UserName = SelectName;
                    ArchiveCommand.Instance.SaveData();
                }
                OnExit();
            });
            UnityTool.Instance.GetComponentFromChild<Button>(DivMain, "ButtonCancel").onClick.AddListener(() =>
            {
                OnExit();
            });
            UnityTool.Instance.GetComponentFromChild<Button>(DivMain, "ButtonDelete").onClick.AddListener(() =>
            {
                EnterPanel("PanelDelete");
            });
            foreach (string name in ArchiveCommand.Instance.NameList)
            {
                Button button = Object.Instantiate<Button>(ButtonCreate, DivNameList.transform);
                button.name = name;
                UnityTool.Instance.GetComponentFromChild<TextMeshProUGUI>(button.gameObject, "Text").text = name;
                button.onClick.AddListener(() =>
                {
                    SelectName = button.GetComponentInChildren<TextMeshProUGUI>().text;
                    ButtonBeEdit = button;
                });
                ButtonList.Add(button);
                ButtonCreate.transform.SetAsLastSibling();
            }
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            if (ArchiveCommand.Instance.NameList.Count == 0)
            {
                EnterPanel("PanelNewUser");
            }
            else
            {
                if (ArchiveCommand.Instance.UserName == null || ArchiveCommand.Instance.UserName.Length == 0)
                {
                    ArchiveCommand.Instance.UserName = ArchiveCommand.Instance.NameList[0];
                }
            }
            OriginName = ArchiveCommand.Instance.UserName;
            foreach (string name in ArchiveCommand.Instance.NameList)
            {
                if (UnityTool.Instance.GetGameObjectInChild(DivNameList, name) == null)
                {
                    Button button = Object.Instantiate<Button>(ButtonCreate, DivNameList.transform);
                    UnityTool.Instance.GetComponentFromChild<TextMeshProUGUI>(button.gameObject, "Text").text = name;
                    button.name = name;
                    button.onClick.AddListener(() =>
                    {
                        SelectName = button.GetComponentInChildren<TextMeshProUGUI>().text;
                        ButtonBeEdit = button;
                    });
                    ButtonList.Add(button);
                    ButtonCreate.transform.SetAsLastSibling();
                }
            }
            for (int i = 0; i < DivNameList.transform.childCount; i++)
            {
                if (DivNameList.transform.GetChild(i).name != ButtonCreate.name)
                {
                    int isFind = 0;
                    foreach (string name in ArchiveCommand.Instance.NameList)
                    {
                        if (name == DivNameList.transform.GetChild(i).name)
                        {
                            isFind = 1;
                        }
                    }
                    if (isFind == 0)
                    {
                        ButtonList.Remove(DivNameList.transform.GetChild(i).GetComponent<Button>());
                        Object.Destroy(DivNameList.transform.GetChild(i).gameObject);
                    }
                }
            }
            foreach (Button button in ButtonList)
            {
                if (button.name == OriginName)
                {
                    button.transform.SetAsFirstSibling();
                    button.Select();
                    ButtonBeEdit = button;
                    SelectName = button.GetComponentInChildren<TextMeshProUGUI>().text;
                }
            }

        }
        public void SetEditText(string name)//获取被修改的按钮
        {
            for (int i = 0; i < ArchiveCommand.Instance.NameList.Count; i++)
            {
                if (ArchiveCommand.Instance.NameList[i] == ButtonBeEdit.name)
                {
                    ArchiveCommand.Instance.NameList[i] = name;
                    ButtonBeEdit.name = name;
                    ButtonBeEdit.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = name;
                    break;
                }
            }
        }
        public string GetEditText()
        {
            return ButtonBeEdit.transform.Find("Text").GetComponent<TextMeshProUGUI>().text;
        }
    }
}

