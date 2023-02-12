using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTeleportation : Spell
{
    [SerializeField, Range(2f, 10f)] float distance;
    public override void Cast()
    {
        base.Cast();
        transform.Translate(Vector3.forward * distance);
    }
}
