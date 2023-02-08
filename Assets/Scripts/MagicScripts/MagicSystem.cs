using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{
    public GameObject spellPrefab;

    public List<KeyCode> keys;
    private int keysPressed;

    public GameObject indicatorPrefab;

    protected PlayerController playerControllerScript;
    protected PlayerCharacterstics playerCharactersticsScript;

    private GameObject indicator;
    private Indicator indicatorScipt;
    protected GameObject player;

    [SerializeField, Range(.5f, 2)] float preporationTime = 1;
    [SerializeField, Range(.25f, 1)] float activationTime = .75f;
    [SerializeField, Range(1, 100)] int manaCost = 5;

    protected virtual void Start()
    {
        player = GameObject.Find("Player").gameObject;
        indicator = Instantiate(indicatorPrefab);//player.transform.Find("Indicator").gameObject;
        indicatorScipt = indicator.gameObject.GetComponent<Indicator>();
        playerControllerScript = player.GetComponent<PlayerController>();
        playerCharactersticsScript = player.GetComponent<PlayerCharacterstics>();

        indicator.transform.position = transform.position - Vector3.up * transform.position.y;
        indicator.SetActive(false);

        keysPressed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        indicator.transform.position = transform.position - Vector3.up * transform.position.y;

        if (!Input.anyKeyDown)
        {
            return;
        }

        if(playerControllerScript.isMoving)
        {
            ResetSpellChars();
            return;
        }

        if(Input.GetKeyDown(keys[keysPressed]))
        {
            if(!indicatorScipt.isOn && keysPressed != 0)
            {
                ResetSpellChars();
                return;
            }

            keysPressed++;
            if (keysPressed >= keys.Count)
            {
                ResetSpellChars();
                if (playerCharactersticsScript.IsEnoughMana(manaCost))
                {
                    CastSpell();
                }
                playerCharactersticsScript.SpendMana(manaCost);
            }
            else
            {
                indicator.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(IndicatorCountdown());
            }
        }
        else
        {
            ResetSpellChars();
            return;
        }
    }

    IEnumerator IndicatorCountdown()
    {
        indicator.SetActive(true);
        yield return new WaitForSeconds(preporationTime * GetSpellDifficulty());
        indicatorScipt.Switch();
        yield return new WaitForSeconds(activationTime * GetSpellDifficulty());
        Debug.Log("Too Late");  
        indicator.SetActive(false);
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
        StopAllCoroutines();
        indicator.SetActive(false);
        keysPressed = 0;
    }
}


