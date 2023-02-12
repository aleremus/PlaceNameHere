using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireWaveLevelTwo : Spell
{
    // Start is called before the first frame update
    [SerializeField, Range(12, 90)] int amountOfParticles;
    public override void Cast()
    {
        base.Cast();
        for (int i = 0; i < amountOfParticles; i++)
        {
            Instantiate(spellPrefab, target.transform.position, target.transform.rotation).transform.Rotate(Vector3.up * 360f / amountOfParticles * i);
        }
    }
}
