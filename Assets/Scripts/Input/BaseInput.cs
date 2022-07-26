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

}

public interface IInputManager
{
    void SwitchProfile(InputType type);

    IInputProfile CurrentProfile { get; }
}