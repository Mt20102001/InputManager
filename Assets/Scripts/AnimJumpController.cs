using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimJumpController : MonoBehaviour
{
    public event System.Action OnStartJump;
    public bool startJump;
    // Start is called before the first frame update
    void Start()
    {
        OnStartJump = null;
        startJump = false;
        OnStartJump += StartJumpCallback;
    }

    private void StartJumpCallback()
    {
        startJump = true;
    }

    public void JumpStart()
    {
        OnStartJump?.Invoke();
    }
}
