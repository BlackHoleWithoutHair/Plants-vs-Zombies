using UnityEngine;
public enum CharacterType
{
    Player = 0,
    Enemy = 1,
}
public class ICharacter
{
    public IAttribute m_Attr;
    public bool isDie { get; protected set; }
    public bool ShouldBeRemove { get; protected set; }
    public GameObject gameObject { get; protected set; }
    public Transform transform { get => gameObject.transform; }
    protected bool isFullStatus = true;
    protected CharacterType m_Type;
    
    private bool isInit;
    private bool isDieInit;
    protected ICharacter(GameObject obj)
    {
        gameObject = obj;
    }
    protected virtual void OnCharacterStart()
    {
        m_Attr.Hp = m_Attr.ShareAttr.MaxHp;
    }
    protected virtual void OnCharacterUpdate()
    {
        if (!isInit)
        {
            isInit = true;
            OnCharacterStart();
        }
    }
    protected virtual void OnCharacterDieStart() { }
    protected virtual void OnCharacterDieUpdate()
    {
        if (!isDieInit)
        {
            isDieInit = true;
            OnCharacterDieStart();
        }
    }
    protected virtual void OnAlwaysUpdate() { }
    public virtual void GameUpdate()
    {
        if (m_Attr == null)
        {
            Debug.Log("m_Attr is null");
            return;
        }
        if (!isDie)
        {
            OnCharacterUpdate();
        }
        else
        {
            OnCharacterDieUpdate();
        }
        OnAlwaysUpdate();
        if (m_Attr.Hp <= 0f)
        {
            isDie = true;
        }
    }
    public virtual void UnderAttack(float damage)
    {
        m_Attr.Hp -= damage;
        isFullStatus = false;
    }
    public void RemoveCharacter()
    {
        ShouldBeRemove = true;
    }
    
}
