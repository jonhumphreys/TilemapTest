using System.Collections.Generic;
using UnityEngine;

// Handles when the player enters an elevated area (e.g., climbing up a mountain)
public class ElevationEntry : MonoBehaviour
{
    // Colliders that block the player when at ground level (disabled when elevated)
    public List<Collider2D> MountainColliders;
    // Colliders that keep the player from falling off edges when elevated (enabled when elevated)
    public List<Collider2D> BoundaryColliders;

    private const int ELEVATED_SORTING_ORDER = 15;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            // Allow player to walk through the mountain now that they're elevated
            DisableMountainColliders();
            // Prevent player from walking off the elevated area
            EnableBoundaryColliders();
        }

        // Changes the sprite's rendering layer to appear above ground-level objects
        MovePlayerUp(other);
    }

    private bool IsPlayer(Collider2D other)
    {
        return other.CompareTag("Player");
    }

    private void DisableMountainColliders()
    {
        SetCollidersStatus(MountainColliders, false);
    }

    private void EnableBoundaryColliders()
    {
        SetCollidersStatus(BoundaryColliders, true);
    }

    private void SetCollidersStatus(List<Collider2D> colliders, bool isEnabled)
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isEnabled;
        }
    }

    private void MovePlayerUp(Collider2D other)
    {
        SpriteRenderer spriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = ELEVATED_SORTING_ORDER;
        }
    }
}
