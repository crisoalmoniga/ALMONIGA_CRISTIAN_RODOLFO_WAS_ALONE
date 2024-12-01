using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private void LateUpdate()
    {
        if (target == null) return;

        float clampedX = Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}