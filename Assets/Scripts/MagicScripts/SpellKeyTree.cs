using System.Collections.Generic;
using UnityEngine;

public class SpellKeyTree
{
    private Dictionary<KeyCode, SpellKeyTree> leafs;
    private List<KeyCode> avaibleKeyCodes;
    private bool isLeaf;
    public Spell spell;

    public SpellKeyTree()
    {
        leafs = new Dictionary<KeyCode, SpellKeyTree>();
        avaibleKeyCodes = new List<KeyCode>();
        isLeaf = false;
    }

    public void AddBranch(List<KeyCode> keyCodes, Spell spell)
    {
        
        if (keyCodes.Count == 0)
        {
            isLeaf = true;
            this.spell = spell; 
            return;
        }

        KeyCode currentKeyCode = keyCodes[0]; 
        keyCodes.RemoveAt(0);

        Debug.Log(currentKeyCode.ToString());
        if (!this.Contains(currentKeyCode))
        {
            SpellKeyTree leaf = new SpellKeyTree();
            
            leafs.Add(currentKeyCode, leaf);
            avaibleKeyCodes.Add(currentKeyCode);

        }
        isLeaf = false;
        leafs[currentKeyCode].AddBranch(keyCodes, spell);
    }

    
    
    
    
    public SpellKeyTree GetLeafByKey(KeyCode keyCode)
    {
        return leafs[keyCode];
    }
    public bool Contains(KeyCode keyCode)
    {
        return avaibleKeyCodes.Contains(keyCode);
    }

    public List<KeyCode> GetKeyCodes()
    {
        return avaibleKeyCodes;
    }

    public bool IsLeaf()
    {
        return isLeaf;
    }

    public void CastSpell()
    {
        if (spell)
        {
            spell.Cast();
        }
    }
    
}
