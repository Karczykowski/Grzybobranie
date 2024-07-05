using System;
using System.Collections;
using System.Collections.Generic;
using Grzybobranie.UI;
using UnityEngine;

namespace Grzybobranie.General
{
    public class TalkableNPC : MonoBehaviour
    {
        [SerializeField] private float range = 3;
        [SerializeField] LayerMask playerLayerMask;
        private Collider2D player;

        [SerializeField] Player.PlayerMovement playerMovement;

        [SerializeField] GameObject dialogBox;
        [SerializeField] Dialogue dialogue;

        private bool isDialogueUp;

        private void Start()
        {
            isDialogueUp = false;
        }

        void Update()
        {
            player = Physics2D.OverlapCircle(transform.position, range, playerLayerMask);

            if (player != null && Input.GetKeyDown(KeyCode.E))
            {
                if(!isDialogueUp)
                {
                    EnableDialogBox();
                }
                else
                {

                }
            }
        }

        public void EnableDialogBox()
        {
            dialogBox.SetActive(true);
            playerMovement.enabled = false;
            dialogue.StartDialogue();
            isDialogueUp = true;
        }
        public void DisableDialogueBox()
        {
            dialogBox.SetActive(false);
            playerMovement.enabled = true;
            isDialogueUp = false;
        }
    }

    
}

