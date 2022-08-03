using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMasterController : MonoBehaviour
{
    [SerializeField] private MovePlayerController movePlayerController;
    [SerializeField] private CombatPlayerController combatPlayerController;
    [SerializeField] private InteractingWithItemsPlayerController interactingWithItems;
    [SerializeField] private Transform cam;

    private Vector3 movement;
    private bool canPickup;

    private void Start()
    {
        movePlayerController.Initialize();
        interactingWithItems.Initialize();
    }

    private void Update()
    {
        CheckToPickUp();
        movePlayerController.CheckPlayerInGround();
        if (interactingWithItems.pickupDone)
        {
            movement = new Vector3(GameInputManager.Instance.CurrentProfile.Horizontal, 0, GameInputManager.Instance.CurrentProfile.Vertical);
            movePlayerController.Move(movement, GameInputManager.Instance.CurrentProfile.Run, cam);

            movePlayerController.Jump(GameInputManager.Instance.CurrentProfile.Jump);

            combatPlayerController.Aim(GameInputManager.Instance.CurrentProfile.Aim, movePlayerController.GetAnglePlayer());

            combatPlayerController.Shoot(GameInputManager.Instance.CurrentProfile.Fire);

            if (canPickup)
            {
                interactingWithItems.PickUpItem(GameInputManager.Instance.CurrentProfile.Pickup);
            }
        }

    }

    private void CheckToPickUp()
    {
        if (combatPlayerController.aimming || !interactingWithItems.pickupDone)
        {
            canPickup = false;
        }
        else
        {
            canPickup =  true;
        }
    }
}
