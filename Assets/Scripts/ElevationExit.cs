using System.Collections.Generic;
using UnityEngine;

public class ElevationExit : MonoBehaviour
{
    public List<Collider2D> MountainColliders;
    public List<Collider2D> BoundaryColliders;

    private const int GROUND_LEVEL_SORTING_ORDER = 5;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            EnableMountainColliders();
            DisableBoundaryColliders();
        }

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
