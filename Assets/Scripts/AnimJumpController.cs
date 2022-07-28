using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimJumpController : MonoBehaviour
{
    public event System.Action OnStartJump;
    public event System.Action OnEndJump;
    public event System.Action OnStartLanding;
    public bool startJump, endJump, startLanding;
    // Start is called before the first frame update
    void Start()
    {
        OnStartJump = null;
        OnEndJump = null;
        OnStartLanding = null;
        OnStartJump += StartJumpCallback;
        OnEndJump += EndJumpCallback;
        OnStartLanding += StartLandingCallback;
        startJump = false;
        endJump = false;
        startLanding = false;
    }

    private void StartJumpCallback()
    {
        startJump = true;
    }
    private void EndJumpCallback()
    {
        endJump = true;
    }
    private void StartLandingCallback()
    {
        startLanding = true;
    }

    public void JumpStart()
    {
        OnStartJump?.Invoke();
    }

    public void JumpEnd()
    {
        OnEndJump?.Invoke();
        Debug.LogError("JumpEnd");
    }

    public void LandingStart()
    {
        OnStartLanding?.Invoke();
        Debug.LogError("LandingStart");
    }
}
