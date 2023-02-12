using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaveLevelOne : Spell
{
    [SerializeField, Range(10, 50)] int amountOfParticles;
    public override void Cast()
    {
        base.Cast();
        for(int i = - amountOfParticles / 2; i < amountOfParticles / 2 + 1; i++)
        {
            Instantiate(spellPrefab, target.transform.position, target.transform.rotation).transform.Rotate(Vector3.up * 90f / amountOfParticles * i);
        }
    }
}
