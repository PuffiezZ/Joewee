using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += context =>
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        };
    }
 
    void Update()
    {
        characterController.Move(currentMovement);
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
