using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWhip : MagicSystem
{
    [SerializeField, Range(5, 20)] int amountOfSegments;
    [SerializeField, Range(1f, 10f)] int costOfSegment;
    [SerializeField, Range(.5f, 5f)] float duration;
    protected override void CastSpell()
    {
        StartCoroutine(SpawnWhipSegment());
    }

    IEnumerator SpawnWhipSegment()
    {
        for(int i = 0; i < amountOfSegments; i++)
        {
            if (playerCharactersticsScript.IsEnoughMana(costOfSegment))
            {
                Instantiate(spellPrefab, player.transform.position, player.transform.rotation);
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
