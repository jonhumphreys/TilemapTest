using System.Collections.Generic;
using UnityEngine;

public class ElevationEntry : MonoBehaviour
{
    public List<Collider2D> MountainColliders;
    public List<Collider2D> BoundaryColliders;

    private const int ELEVATED_SORTING_ORDER = 15;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            DisableMountainColliders();
            EnableBoundaryColliders();
        }

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
