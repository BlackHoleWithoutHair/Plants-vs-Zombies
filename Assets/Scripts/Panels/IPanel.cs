using System.Collections.Generic;
using UnityEngine;

public abstract class IPanel
{
    protected string name;
    protected GameObject m_GameObject;
    public GameObject gameObject => m_GameObject;
    protected Transform m_Canvas;
    protected AbstractController m_UIController;
    public AbstractController UIController => m_UIController;
    protected IPanel parent;
    protected List<IPanel> children;
    protected bool m_isSuspend;
    public bool isSuspend => m_isSuspend;
    protected bool isShowPanelAfterExit;
    protected bool isStart;
    protected bool isEnter;
    public IPanel(IPanel parent)
    {
        m_Canvas = GameObject.Find("MainCanvas").transform;
        children = new List<IPanel>();
        this.parent = parent;
    }
    protected virtual void GameStart()
    {
        OnInit();
    }
    public virtual void GameUpdate()
    {
        if (!isStart)
        {
            isStart = true;
            GameStart();
        }
        foreach (IPanel child in children)
        {
            child.GameUpdate();
        }
        if (m_isSuspend == false)
        {
            OnUpdate();
        }
    }

    protected virtual void OnInit()//�����������ͣ.������OnInit��ʹ��EnterPanel,��Ϊ�ݹ�ᵼ��panel��ͣ�Ӷ��޷�ִ��OnUpdate
    {
        OnSuspend();
        if (name == null)
        {
            Debug.Log("IPanel name is null");
        }
        if (m_GameObject == null)
        {
            m_GameObject = UnityTool.Instance.GetGameObjectFromCanvas(name);
            if (m_GameObject == null)
            {
                //m_GameObject = Object.Instantiate(ProxyResourceFactory.Instance.Factory.GetPanel(name), m_Canvas);
            }
            if (m_GameObject == null)
            {
                Debug.Log(name + " m_GameObject is null");
                return;
            }
        }
        //foreach(IPanel panel in children)
        //{
        //    panel.OnInit();
        //}
    }

    protected virtual void OnEnter()
    {
        gameObject.SetActive(true);
        Debug.Log(name);
    }
    protected virtual void OnUpdate()
    {
        if (!isEnter)
        {
            isEnter = true;
            OnEnter();
        }
    }
    protected virtual void OnSuspend()
    {
        m_isSuspend = true;
    }
    protected virtual void OnResume()
    {
        m_isSuspend = false;
    }
    //���Ҫ�����˳����ҳ��,Ҫ���մӵ����ϵ�˳������˳�����������,ԭ��:OnEnter
    public virtual void OnExit()//����˳��Ĵ������������
    {
        if (!isShowPanelAfterExit)
        {
            m_GameObject.SetActive(false);
        }
        OnSuspend();
        parent.isEnter = false;
        parent.OnResume();
    }
    protected virtual void EnterPanel(string name)
    {

        IPanel panel = GetPanelFromChildren(name);
        panel.gameObject.SetActive(true);
        panel.OnResume();
        panel.isEnter = false;
        OnSuspend();
    }
    public IPanel GetPanelFromChildren(string name)
    {
        if (this.name == name)
        {
            return this;
        }
        foreach (IPanel child in children)
        {
            if (child.name == name)
            {
                return child;
            }
        }
        Debug.Log("IPanel GetPanelFromChildren" + "(" + name + ")" + " return null");
        return null;
    }
    public IPanel GetPanelFromAll(string name)
    {
        if (this.name == name)
        {
            return this;
        }
        foreach (IPanel child in children)
        {
            if (child.name == name)
            {
                return child;
            }
            IPanel result = child.GetPanelFromAll(name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}

