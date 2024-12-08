using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private DimensionShifter dimensionShifter;
    private PlayerMovement playerMovement;

    private void Start()
    {
        dimensionShifter = GetComponent<DimensionShifter>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void JumpInput(InputAction.CallbackContext ctx)
    {
        
    }
    
    public void DimensionInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            dimensionShifter.ChangeColor();
    }
}
