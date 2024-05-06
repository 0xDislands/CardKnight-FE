using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonPowerup : MonoBehaviour
{
    public PowerupId id;

    public abstract void OnClick();
}
