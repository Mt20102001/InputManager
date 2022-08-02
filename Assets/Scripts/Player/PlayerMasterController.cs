using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMasterController : MonoBehaviour
{
    [SerializeField] private MovePlayerController movePlayerController;
    //[SerializeField] private CombatPlayerController combatPlayerController;
    [SerializeField] private Transform cam;

    private Vector3 movement;

    private void Update()
    {
        movement = new Vector3(GameInputManager.Instance.CurrentProfile.Horizontal, 0, GameInputManager.Instance.CurrentProfile.Vertical);
        movePlayerController.Move(movement, GameInputManager.Instance.CurrentProfile.Run, cam);
    }
}
