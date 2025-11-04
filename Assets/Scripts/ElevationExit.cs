using System.Collections.Generic;
using UnityEngine;

// Handles when the player exits an elevated area (e.g., climbing down from a mountain)
public class ElevationExit : MonoBehaviour
{
    // Colliders that block the player when at ground level (re-enabled when descending)
    public List<Collider2D> MountainColliders;
    // Colliders that keep the player from falling off edges when elevated (disabled when descending)
    public List<Collider2D> BoundaryColliders;

    private const int GROUND_LEVEL_SORTING_ORDER = 5;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            // Re-enable mountain collision now that player is back at ground level
            EnableMountainColliders();
            // Remove elevated area boundaries since player is no longer elevated
            DisableBoundaryColliders();
        }

        // Changes the sprite's rendering layer to appear at ground level
        MovePlayerDown(other);
    }

    private bool IsPlayer(Collider2D other)
    {
        return other.CompareTag("Player");
    }

    private void EnableMountainColliders()
    {
        SetCollidersStatus(MountainColliders, true);
    }

    private void DisableBoundaryColliders()
    {
        SetCollidersStatus(BoundaryColliders, false);
    }

    private void SetCollidersStatus(List<Collider2D> colliders, bool isEnabled)
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isEnabled;
        }
    }

    private void MovePlayerDown(Collider2D other)
    {
        SpriteRenderer spriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = GROUND_LEVEL_SORTING_ORDER;
        }
    }
}
