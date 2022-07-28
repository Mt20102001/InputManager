using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum InputType
{
    MOBILE = 0,
    CONTROLLER = 1,
    DESKTOP = 2
}


public interface IInputProfile
{
    InputType Type { get; }
    float Horizontal { get; }
    float Vertical { get; }
    bool Fire { get; }
    bool Jump { get; }
    bool Run { get; }
    bool Aim { get; }
    float HorizontalCamera { get; }
    float VerticalCamera { get; }
    string InputAxisNameCurrentOfXForCamera { get; }
    string InputAxisNameCurrentOfYForCamera { get; }

}

public interface IInputManager
{
    void SwitchProfile(InputType type);

    IInputProfile CurrentProfile { get; }
}