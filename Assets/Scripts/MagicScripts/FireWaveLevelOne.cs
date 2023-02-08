using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaveLevelOne : MagicSystem
{
    [SerializeField, Range(10, 50)] int amountOfParticles;
    protected override void CastSpell()
    {
        for(int i = - amountOfParticles / 2; i < amountOfParticles / 2 + 1; i++)
        {
            Instantiate(spellPrefab, player.transform.position, player.transform.rotation).transform.Rotate(Vector3.up * 90f / amountOfParticles * i);
        }
    }
}
