using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private Transform AttackPoint;

    private Vector3 movement;
    private bool fire;

    // Update is called once per frame
    private void Update()
    {
        // -- Input Movement
        movement.x = GameInputManager.Instance.CurrentProfile.Horizontal;
        movement.z = GameInputManager.Instance.CurrentProfile.Vertical;

        // -- Input Fire
        fire = GameInputManager.Instance.CurrentProfile.Fire;
    }

    private void FixedUpdate()
    {
        // Movement
        rbPlayer.MovePosition(rbPlayer.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Fire
        if (fire)
        {
            // GetFromBool(AttackPoint.position, AttackPoint.rotation);
        }
    }

    
}
