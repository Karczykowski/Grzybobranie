using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 targetOffset;
    [SerializeField] private Vector2 padding;

    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        Vector2 resolution = new Vector2(Screen.width, Screen.height);
        Vector2 targetPosition = (Vector2)target.position + targetOffset;
        
        ClampPositionInsideScreenView(targetPosition, GetLeftBottomCorner(), GetRightTopCorner(resolution));
        RotateTowardsTarget(target.position);
    }

    private Vector2 GetRightTopCorner(Vector2 resolution)
    {
        return cam.ScreenToWorldPoint(resolution - padding); ;
    }
    private Vector2 GetLeftBottomCorner()
    {
        return cam.ScreenToWorldPoint(padding);
    }

    private void ClampPositionInsideScreenView(Vector2 position, Vector2 leftBottomCornder, Vector2 rightTopCorner)
    {
        float clampedX = Mathf.Clamp(position.x, leftBottomCornder.x, rightTopCorner.x);
        float clampedY = Mathf.Clamp(position.y, leftBottomCornder.y, rightTopCorner.y);
        transform.position = new Vector2(clampedX, clampedY);
    }

    private void RotateTowardsTarget(Vector2 target)
    {
        Vector2 dir = target - (Vector2)transform.position;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, rotZ + 90f);

        transform.localRotation = targetRot;
    }
}
