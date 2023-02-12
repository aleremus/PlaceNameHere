using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public  class Spell : MonoBehaviour
{
    [SerializeField] List<KeyCode> keyCodes;
    [SerializeField] private int manaCost;

    protected GameObject target;

    public GameObject spellPrefab;

    protected PlayerCharacterstics playerCharactersticsScript;

    protected void OnEnable()
    {
        target = GameObject.Find("Player").gameObject;
        playerCharactersticsScript = target.GetComponent<PlayerCharacterstics>();
    }

    public virtual void Cast()
    {
        if (!playerCharactersticsScript.IsEnoughMana(manaCost))
        {
            playerCharactersticsScript.SpendMana(manaCost);
            return;
        }
        playerCharactersticsScript.SpendMana(manaCost);
    }

    public List<KeyCode> GetKeyCodes()
    {
        return new List<KeyCode>(keyCodes);
    }
}
