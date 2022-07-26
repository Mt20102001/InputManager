using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    void Start()
    {
        GameInputManager.Instance.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // var horizontal = GameInputManager.Instance.CurrentProfile.Horizontal;
        // var vertical = GameInputManager.Instance.CurrentProfile.Vertical;

        // Debug.LogError($"{horizontal} - {vertical}");
    }


    [ContextMenu("SWITCH TO MOBILE")]
    public void Test01()
    {
        GameInputManager.Instance.SwitchProfile(InputType.MOBILE);
        Debug.LogError("switch mobile");
    }

    [ContextMenu("SWITCH TO DESKTOP")]
    public void Test02()
    {
        GameInputManager.Instance.SwitchProfile(InputType.DESKTOP);
        Debug.LogError("switch desktop");
    }

    [ContextMenu("SWITCH TO CONTROLLER")]
    public void Test03()
    {
        GameInputManager.Instance.SwitchProfile(InputType.CONTROLLER);
        Debug.LogError("switch controller");
    }
}
