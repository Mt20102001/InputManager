using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour, IInputProfile
{
    public InputType Type => InputType.MOBILE;

    public float Horizontal => 0;

    public float Vertical => 0;
    public bool Fire => false;

    public bool Jump => throw new System.NotImplementedException();

    public bool Run => throw new System.NotImplementedException();

    public bool Aim => throw new System.NotImplementedException();

    public bool Pickup => throw new System.NotImplementedException();

    public float HorizontalCamera => throw new System.NotImplementedException();

    public float VerticalCamera => throw new System.NotImplementedException();

    public string InputAxisNameCurrentOfXForCamera => throw new System.NotImplementedException();

    public string InputAxisNameCurrentOfYForCamera => throw new System.NotImplementedException();
}
