
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInput : MonoBehaviour
{
    public float Deadzone = 0.2f;
    public int GamepadIndex = 0;
    public bool UseDpad = true;

    public Vector2 GetMovement()
    {
        Gamepad gamepad = GetGamepad();
        
        if (gamepad == null)
        {
            return Vector2.zero;
        }

        Vector2 movement = GetStickMovement(gamepad);
        
        if (UseDpad)
        {
            movement = GetDpadMovementIfActive(gamepad, movement);
        }
        
        movement = AdjustForDiagonalMovement(movement);
        
        return movement;
    }

    public bool WasPickupButtonPressed()
    {
        if (Gamepad.current == null)
        {
            return false;
        }
        
        return Gamepad.current.buttonSouth.wasPressedThisFrame;
    }

    public bool WasPlaceButtonPressed()
    {
        if (Gamepad.current == null)
        {
            return false;
        }
        
        return Gamepad.current.buttonNorth.wasPressedThisFrame;
    }
    
    public bool WasAttackButtonPressed()
    {
        if (Gamepad.current == null)
        {
            return false;
        }
        
        return Gamepad.current.buttonWest.wasPressedThisFrame;
    }

    private Gamepad GetGamepad()
    {
        if (HasNoGamepads())
        {
            return null;
        }
        
        if (IsInvalidGamepadIndex())
        {
            return null;
        }
        
        return Gamepad.all[GamepadIndex];
    }

    private bool HasNoGamepads()
    {
        return Gamepad.all.Count == 0;
    }

    private bool IsInvalidGamepadIndex()
    {
        return GamepadIndex < 0 || GamepadIndex >= Gamepad.all.Count;
    }

    private Vector2 GetStickMovement(Gamepad gamepad)
    {
        Vector2 stickInput = gamepad.leftStick.ReadValue();
        
        if (IsStickInputAboveDeadzone(stickInput))
        {
            return stickInput;
        }
        
        return Vector2.zero;
    }

    private bool IsStickInputAboveDeadzone(Vector2 stickInput)
    {
        return stickInput.magnitude >= Deadzone;
    }

    private Vector2 GetDpadMovementIfActive(Gamepad gamepad, Vector2 currentMovement)
    {
        Vector2 dpadInput = gamepad.dpad.ReadValue();
        
        if (IsDpadActive(dpadInput))
        {
            return dpadInput;
        }
        
        return currentMovement;
    }

    private bool IsDpadActive(Vector2 dpadInput)
    {
        return dpadInput.sqrMagnitude > 0f;
    }

    private Vector2 AdjustForDiagonalMovement(Vector2 movement)
    {
        if (IsDiagonalMovement(movement))
        {
            movement.Normalize();
        }
        
        return movement;
    }

    private bool IsDiagonalMovement(Vector2 movement)
    {
        return movement.sqrMagnitude > 1f;
    }
}