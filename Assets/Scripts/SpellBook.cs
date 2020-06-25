using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Spell[] spells;

    public Spell CastSpell(int index)
    {
        return spells[index];
    }
}
