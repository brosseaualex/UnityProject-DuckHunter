using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : BaseWeapon
{
    private void Awake()
    {
        IsAcceptingMagazine = false;
    }
}
