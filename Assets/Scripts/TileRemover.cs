using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRemover : MonoBehaviour
{
    public Tilemap AutoCollectTilemap;
    public Tilemap InteractTilemap;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private void Update()
    {
        if (WasPickupButtonPressed())
        {
            RemoveTileAtPlayerPosition();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RemoveAutoCollectTileAtPlayerPosition();
    }

    private bool WasPickupButtonPressed()
    {
        bool keyboardPressed = IsKeyboardPickupPressed();
        bool gamepadPressed = IsGamepadPickupPressed();
        
        return keyboardPressed || gamepadPressed;
    }

    private bool IsKeyboardPickupPressed()
    {
        if (KeyboardInput == null)
        {
            return false;
        }
        
        return KeyboardInput.WasPickupButtonPressed();
    }

    private bool IsGamepadPickupPressed()
    {
        if (GamepadInput == null)
        {
            return false;
        }
        
        return GamepadInput.WasPickupButtonPressed();
    }

    private void RemoveTileAtPlayerPosition()
    {
        if (InteractTilemap == null)
        {
            return;
        }
        
        Vector3Int cellPosition = InteractTilemap.WorldToCell(transform.position);
        InteractTilemap.SetTile(cellPosition, null);
    }

    private void RemoveAutoCollectTileAtPlayerPosition()
    {
        if (AutoCollectTilemap == null)
        {
            return;
        }
        
        Vector3Int cellPosition = AutoCollectTilemap.WorldToCell(transform.position);
        AutoCollectTilemap.SetTile(cellPosition, null);
    }
}