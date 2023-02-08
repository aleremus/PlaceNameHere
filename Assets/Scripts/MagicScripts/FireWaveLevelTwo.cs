using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaveLevelTwo : MagicSystem
{
    // Start is called before the first frame update
    [SerializeField, Range(12, 90)] int amountOfParticles;
    protected override void CastSpell()
    {
        for (int i = 0; i < amountOfParticles; i++)
        {
            Instantiate(spellPrefab, player.transform.position, player.transform.rotation).transform.Rotate(Vector3.up * 360f / amountOfParticles * i);
        }
    }
}
