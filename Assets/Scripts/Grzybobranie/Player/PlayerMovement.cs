using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grzybobranie.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        private float currentMoveSpeed;
        [SerializeField] private Rigidbody2D rb;
        private Vector2 movement;
        private bool isSlowed;

        private void Start()
        {
            isSlowed = false;
            currentMoveSpeed = moveSpeed;
        }

        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * currentMoveSpeed * Time.fixedDeltaTime);
        }

        public float GetCurrentMoveSpeed()
        {
            return currentMoveSpeed;
        }
        public void SetCurrentMoveSpeed(float currentMoveSpeed)
        {
            this.currentMoveSpeed = currentMoveSpeed;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }

        public bool GetIsSlowed()
        {
            return isSlowed;
        }

        public void SetIsSlowed(bool isSlowed)
        {
            this.isSlowed = isSlowed;
        }
    }

}
