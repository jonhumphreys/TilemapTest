
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    public Vector2 GetMovement()
    {
        if (IsKeyboardUnavailable())
        {
            return Vector2.zero;
        }

        float horizontalInput = GetHorizontalInput();
        float verticalInput = GetVerticalInput();

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        movement = AdjustForDiagonalMovement(movement);
        
        return movement;
    }

    public bool WasPickupButtonPressed()
    {
        if (IsKeyboardUnavailable())
        {
            return false;
        }
        
        return Keyboard.current[Key.E].wasPressedThisFrame;
    }

    public bool WasPlaceButtonPressed()
    {
        if (IsKeyboardUnavailable())
        {
            return false;
        }
        
        return Keyboard.current[Key.F].wasPressedThisFrame;
    }
    
    public bool WasAttackButtonPressed()
    {
        if (IsKeyboardUnavailable())
        {
            return false;
        }
        
        return Keyboard.current[Key.LeftShift].wasPressedThisFrame;
    }

    private bool IsKeyboardUnavailable()
    {
        return Keyboard.current == null;
    }

    private float GetHorizontalInput()
    {
        float horizontal = 0f;
        
        if (IsLeftKeyPressed())
        {
            horizontal = horizontal - 1f;
        }
        
        if (IsRightKeyPressed())
        {
            horizontal = horizontal + 1f;
        }
        
        return horizontal;
    }

    private float GetVerticalInput()
    {
        float vertical = 0f;
        
        if (IsUpKeyPressed())
        {
            vertical = vertical + 1f;
        }
        
        if (IsDownKeyPressed())
        {
            vertical = vertical - 1f;
        }
        
        return vertical;
    }

    private bool IsLeftKeyPressed()
    {
        return Keyboard.current.aKey.isPressed;
    }

    private bool IsRightKeyPressed()
    {
        return Keyboard.current.dKey.isPressed;
    }

    private bool IsUpKeyPressed()
    {
        return Keyboard.current.wKey.isPressed;
    }

    private bool IsDownKeyPressed()
    {
        return Keyboard.current.sKey.isPressed;
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