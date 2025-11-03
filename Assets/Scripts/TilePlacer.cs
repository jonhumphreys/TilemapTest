using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{
    public Tilemap InteractTilemap;
    public TileBase TileToPlace;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private void Update()
    {
        if (WasPlaceButtonPressed())
        {
            PlaceTileAtPlayerPosition();
        }
    }

    private bool WasPlaceButtonPressed()
    {
        bool keyboardPressed = IsKeyboardPlacePressed();
        bool gamepadPressed = IsGamepadPlacePressed();
        
        return keyboardPressed || gamepadPressed;
    }

    private bool IsKeyboardPlacePressed()
    {
        if (KeyboardInput == null)
        {
            return false;
        }
        
        return KeyboardInput.WasPlaceButtonPressed();
    }

    private bool IsGamepadPlacePressed()
    {
        if (GamepadInput == null)
        {
            return false;
        }
        
        return GamepadInput.WasPlaceButtonPressed();
    }

    private void PlaceTileAtPlayerPosition()
    {
        if (InteractTilemap == null)
        {
            return;
        }
        
        if (TileToPlace == null)
        {
            return;
        }
        
        Vector3Int cellPosition = GetCellPositionAtPlayer();
        InteractTilemap.SetTile(cellPosition, TileToPlace);
    }

    private Vector3Int GetCellPositionAtPlayer()
    {
        return InteractTilemap.WorldToCell(transform.position);
    }
}