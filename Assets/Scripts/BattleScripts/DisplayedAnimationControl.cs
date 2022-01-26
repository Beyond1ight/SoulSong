using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayedAnimationControl : MonoBehaviour
{
    public void CallDamageFlash()
    {
        Engine.e.battleSystem.SpriteDamageFlash(Engine.e.battleSystem.previousTargetReference);
    }
}
