using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : BaseWeapon
{
    private void Awake()
    {
        IsAcceptingMagazine = true;
    }
}
