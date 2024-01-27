using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grzybobranie.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _followSpeed;
        [SerializeField] private Vector2 _offset;

        [Header("Instances")]
        [SerializeField] private Transform _target;

        private void Start()
        {
            if (_target == null)
                return;
        }

        private void Update()
        {
            if (_target == null)
                return;

            Vector2 targetPos = GetTargetPos();

            LerpToPosition(targetPos);
        }

        private Vector2 GetTargetPos()
        {
            return (Vector2)_target.position + _offset;
        }

        private void LerpToPosition(Vector2 targetPos)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, targetPos, _followSpeed * Time.deltaTime);
            newPos.z = -10;

            transform.position = newPos;
        }
    }
}
