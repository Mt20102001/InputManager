using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour, IInputManager
{

    public static GameInputManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameInputManager>();
            return instance;
        }
    }

    private static GameInputManager instance;

    public IInputProfile CurrentProfile { get; private set; }

    public void SwitchProfile(InputType type)
    {
        var profile = GetProfileByType(type);
        if (profile == null)
        {
            CurrentProfile = profiles[0];
            return;
        }
        CurrentProfile = profile;
    }

    private List<IInputProfile> profiles = new List<IInputProfile>();

    public void Initialize()
    {
        var targets = this.GetComponents<IInputProfile>();
        profiles.AddRange(targets);

        SwitchProfile(InputType.DESKTOP);
    }


    private IInputProfile GetProfileByType(InputType type)
    {
        foreach (var profile in profiles)
        {
            if (profile.Type.Equals(type))
            {
                return profile;
            }
        }
        return null;
    }


}
