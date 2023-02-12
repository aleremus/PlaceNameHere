using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWhip : Spell
{
    [SerializeField, Range(5, 20)] int amountOfSegments;
    [SerializeField, Range(1f, 10f)] int costOfSegment;
    [SerializeField, Range(.5f, 5f)] float duration;
    public override void Cast()
    {
        base.Cast();
        StartCoroutine(SpawnWhipSegment());
    }

    IEnumerator SpawnWhipSegment()
    {
        for(int i = 0; i < amountOfSegments; i++)
        {
            if (playerCharactersticsScript.IsEnoughMana(costOfSegment))
            {
                Instantiate(spellPrefab, target.transform.position, target.transform.rotation);
                playerCharactersticsScript.SpendMana(costOfSegment);
            }
            else
            {
                playerCharactersticsScript.SpendMana(costOfSegment);
                StopCoroutine(SpawnWhipSegment());
            }
            yield return new WaitForSeconds(duration / amountOfSegments);
        }
    }
}
