using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI
{
    int stageId;
    bool isEnd;
    int isPlay = 0;
    int NextCount;
    List<DialogueData> m_datas;
    DialogueData m_HeadData;
    GameObject m_Dialogue;
    TextMeshProUGUI m_Name;
    TextMeshProUGUI m_text;
    TextMeshProUGUI m_Question;
    Transform answers;
    public DialogueUI()
    {
    }
    public void GameStart()
    {
        stageId = ArchiveCommand.Instance.StageId;
        m_datas = AttributeFactory.Instance.GetDialogue(stageId);
        if (m_datas != null)
        {
            m_HeadData = m_datas[0];
        }
        m_Dialogue = UnityTool.Instance.GetGameObjectInChild(GameObject.Find("MainCanvas"), "Dialogue");
        answers = m_Dialogue.transform.Find("Answers");
        m_text = m_Dialogue.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        m_Name = m_Dialogue.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        m_Question = m_Dialogue.transform.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        GameObject.Find("Main Camera").transform.position = new Vector3(-3.66f, 0f, -10f);

    }
    public void GameUpdate()
    {
        if (m_datas != null)
        {
            m_Dialogue.SetActive(true);
            NextCount = m_HeadData.nexts.Count;
            if (NextCount > 1)
            {
                m_text.transform.gameObject.SetActive(false);
                m_Name.transform.gameObject.SetActive(false);
                m_Question.gameObject.SetActive(true);
                answers.gameObject.SetActive(true);
                m_Question.text = m_HeadData.text;
                for (int i = 0; i < m_HeadData.nexts.Count; i++)
                {
                    DialogueData data = GetDialogueDataByIndex(m_HeadData.nexts[i]);
                    TextMeshProUGUI text = answers.transform.Find("Answer" + (i + 1)).GetComponent<TextMeshProUGUI>();
                    Button button = text.GetComponent<Button>();
                    text.transform.gameObject.SetActive(true);
                    text.text = (i + 1) + "." + data.text;
                    button.onClick.AddListener(() =>
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            answers.transform.Find("Answer" + (i + 1)).GetComponent<Button>().onClick.RemoveAllListeners();
                        }
                        m_HeadData = GetDialogueDataByIndex(data.nexts[0]);
                    });
                }
            }
            else
            {
                m_text.transform.gameObject.SetActive(true);
                m_Name.transform.gameObject.SetActive(true);
                m_Question.gameObject.SetActive(false);
                answers.gameObject.SetActive(false);
                for (int i = 0; i < 4; i++)
                {
                    foreach (DialogueData data in m_datas)
                    {
                        answers.transform.Find("Answer" + (i + 1)).gameObject.SetActive(false);
                    }
                }
                m_text.text = m_HeadData.text;
                m_Name.text = m_HeadData.name;
                if (Input.GetMouseButtonDown(0))
                {
                    if (NextCount == 1)
                    {
                        foreach (DialogueData data in m_datas)
                        {
                            if (data.index == m_HeadData.nexts[0])
                            {
                                m_HeadData = data; break;
                            }
                        }
                    }
                    else if (NextCount == 0)
                    {
                        if (isPlay == 0)
                        {
                            isPlay += 1;
                            m_Dialogue.SetActive(false);
                            GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
                            GameObject.Find("Main Camera").GetComponent<Animator>().Play("CameraMoveRight");
                            isEnd = true;
                        }
                    }
                }
            }
        }
        else
        {
            if (isPlay == 0)
            {
                isPlay += 1;
                GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
                GameObject.Find("Main Camera").GetComponent<Animator>().Play("CameraMoveRight");
                isEnd = true;
            }
        }

    }
    public bool isDialogueEnd()
    {
        return isEnd;
    }
    private DialogueData GetDialogueDataByIndex(int index)
    {
        foreach (DialogueData data in m_datas)
        {
            if (data.index == index)
            {
                return data;
            }
        }
        Debug.Log("DialogueUI GetDialogueDataByIndex return null");
        return null;
    }
}
