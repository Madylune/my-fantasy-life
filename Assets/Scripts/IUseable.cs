using System;
using UnityEngine;

public interface IUseable
{
    Sprite MyIcon { get; set; }

    void Use();
}
