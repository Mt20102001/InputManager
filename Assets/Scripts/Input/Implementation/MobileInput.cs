using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour, IInputProfile
{
    public InputType Type => InputType.MOBILE;

    public float Horizontal => 0;

    public float Vertical => 0;
    public bool Fire => false;


}
