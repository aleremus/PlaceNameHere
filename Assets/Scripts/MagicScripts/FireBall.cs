using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MagicSystem
{
    
    protected override void CastSpell()
    {
        Instantiate(spellPrefab, player.transform.position, player.transform.rotation);
    }
}
