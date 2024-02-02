using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grzybobranie.Objects
{
    public class Bush : MonoBehaviour
    {
        [SerializeField] private float slowAmount = 0.1f;
        private Player.PlayerMovement playerMovement;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 3)
            {
                if (!playerMovement.GetIsSlowed())
                {
                    playerMovement.SetCurrentMoveSpeed(playerMovement.GetCurrentMoveSpeed() * (1 - slowAmount));
                    playerMovement.SetIsSlowed(true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.layer == 3)
            {
                if (playerMovement.GetIsSlowed())
                {
                    playerMovement.SetCurrentMoveSpeed(playerMovement.GetMoveSpeed());
                    playerMovement.SetIsSlowed(false);
                }
            }
        }

        public void SetPlayerMovement(Player.PlayerMovement playerMovement)
        {
            this.playerMovement = playerMovement;
        }
    }

}
