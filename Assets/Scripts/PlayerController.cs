using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private Rigidbody rbPlayer;
    [SerializeField]private float moveSpeed;
    private Vector3 movement;

    public void Initialize()
    {
        GameInputManager.Instance.SwitchProfile(InputType.CONTROLLER);
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = GameInputManager.Instance.CurrentProfile.Horizontal;
        movement.z = GameInputManager.Instance.CurrentProfile.Vertical;
    }

    private void FixedUpdate()
    {
        // Movement
        rbPlayer.MovePosition(rbPlayer.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
