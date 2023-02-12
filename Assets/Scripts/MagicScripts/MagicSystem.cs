using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{
    public List<Spell> spells;
    private int keysPressed;

    public GameObject indicatorPrefab;

    private PlayerController playerControllerScript;
    private PlayerCharacterstics playerCharactersticsScript;

    private bool isAbleToCastSpell;

    private GameObject indicator;
    private Indicator indicatorScipt;
    private GameObject player;

    private SpellKeyTree initialTree;
    private SpellKeyTree currentTree;
    private List<KeyCode> spellKeyCodes;

    [SerializeField, Range(.5f, 2)] float preporationTime = 1;
    [SerializeField, Range(.25f, 1)] float activationTime = .75f;
    [SerializeField, Range(1, 100)] int manaCost = 5;

    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        indicator = player.transform.Find("Indicator").gameObject;
        indicatorScipt = indicator.gameObject.GetComponent<Indicator>();
        playerControllerScript = player.GetComponent<PlayerController>();
        playerCharactersticsScript = player.GetComponent<PlayerCharacterstics>();

        indicator.transform.position = transform.position - Vector3.up * transform.position.y;
        indicator.SetActive(false);
        keysPressed = 0;
        isAbleToCastSpell = true;

        initialTree = new SpellKeyTree();
        
        
        foreach (var spell in spells)
        {
            string output = spell.GetType().ToString();
            foreach (var VARIABLE in spell.GetKeyCodes())
            {
                output += " " + VARIABLE;
            }
            initialTree.AddBranch(spell.GetKeyCodes(), spell);
            Debug.Log(output);
        }

        
        foreach (var spell in spells)
        {
            currentTree = initialTree;
            string output = spell.GetType().ToString();
            foreach (var VARIABLE in spell.GetKeyCodes())
            {
                output += " " + VARIABLE;
                if (currentTree.IsLeaf())
                {
                    output += " " + currentTree.spell.GetType().ToString();
                }
                currentTree = currentTree.GetLeafByKey(VARIABLE);
            }
            Debug.Log(output);
        }
        
        
        spellKeyCodes = initialTree.GetKeyCodes();
        currentTree = initialTree;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isMoving)
        {
            ResetSpellChars();
            return;
        }
        
        if(!Input.anyKeyDown)
        {
            return;
        }
        
        foreach (var keyCode in spellKeyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (!isAbleToCastSpell)
                {
                    ResetSpellChars();
                    return;
                }
                StopAllCoroutines();
                indicator.SetActive(false);
                StartCoroutine(IndicatorCountdown());
                Debug.Log("StagePassed" + keyCode +"pressed");
                

                keysPressed++; 
                currentTree = currentTree.GetLeafByKey(keyCode);
                spellKeyCodes = currentTree.GetKeyCodes();
                if (currentTree.IsLeaf())
                {
                    currentTree.CastSpell();
                    Debug.Log("Spell is casted");
                    ResetSpellChars();
                    return;
                }

                return;
            }
        }
        
        if(Input.GetMouseButtonDown(1))
            return;
        
        ResetSpellChars();
    }

    IEnumerator IndicatorCountdown()
    {
        indicator.SetActive(true);
        isAbleToCastSpell = false;
        yield return new WaitForSeconds(preporationTime * GetSpellDifficulty());
        indicatorScipt.Switch();
        isAbleToCastSpell = true;
        yield return new WaitForSeconds(activationTime * GetSpellDifficulty());
        Debug.Log("Too Late");  
        indicator.SetActive(false);
        ResetSpellChars();
    }

    protected virtual void CastSpell()
    {

    }

    private float GetSpellDifficulty()
    {
        return 1 - .2f * keysPressed;
    }

    private void ResetSpellChars()
    {
        isAbleToCastSpell = true;
        StopAllCoroutines();
        indicator.SetActive(false);
        keysPressed = 0;

        currentTree = initialTree;
        spellKeyCodes = currentTree.GetKeyCodes();
    }
}


