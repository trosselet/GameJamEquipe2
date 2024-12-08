using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private DimensionShifter dimensionShifter;
    private PlayerMovement playerMovement;
    private Translateur translateur;
    private Propulsion propulsion;

    private void Start()
    {
        dimensionShifter = GetComponent<DimensionShifter>();
        playerMovement = GetComponent<PlayerMovement>();
        translateur = GetComponent<Translateur>();
        propulsion = GetComponent<Propulsion>();
    }

    public void JumpInput(InputAction.CallbackContext ctx)
    {
        
    }
    
    public void DimensionInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            dimensionShifter.ChangeColor();
    }

    public void TranslateurInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            translateur.CreateTranslateur();
    }

    public void PropulsionInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            propulsion.Propulse();
    }
}
