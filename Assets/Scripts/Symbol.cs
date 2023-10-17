using UnityEngine;

public class Symbol : MonoBehaviour
{
    // Start is called before the first frame update
    private ICharacter m_Character;
    public void SetCharacter(ICharacter character)
    {
        m_Character = character;
    }
    public ICharacter GetCharacter()
    {
        if (m_Character == null)
        {
            Debug.Log("SymbolŒ¥…Ë÷√");
        }
        return m_Character;
    }
}
