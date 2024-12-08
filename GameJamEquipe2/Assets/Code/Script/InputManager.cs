using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerJumping playerJumping;
    private DimensionShifter dimensionShifter;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJumping = GetComponent<PlayerJumping>();
        dimensionShifter = GetComponent<DimensionShifter>();
    }
    
    public void DimensionInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            dimensionShifter.ChangeColor();
    }
}
