using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell
{
    
    public override void Cast()
    {
        base.Cast();
        Instantiate(spellPrefab, target.transform.position, target.transform.rotation);
    }
}
