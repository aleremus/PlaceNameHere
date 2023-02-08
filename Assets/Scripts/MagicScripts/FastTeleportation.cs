using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTeleportation : MagicSystem
{
    [SerializeField, Range(2f, 10f)] float distance;
    protected override void CastSpell()
    { 
        transform.Translate(Vector3.forward * distance);
        
    }
}
