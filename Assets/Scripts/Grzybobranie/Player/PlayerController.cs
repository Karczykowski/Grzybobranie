using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Grzybobranie.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float range = 1;
        [SerializeField] private LayerMask mushroomLayerMask;
        [SerializeField] private UI.Objective _objective;
        [SerializeField] private UI.MushroomPreview mushroomPreview;
        [SerializeField] private float popupDelayTime = 0f;
        [SerializeField] private Animator fadeoutAnimator;
        private Collider2D closestShroom;
        private Collider2D previousShroom;
        private bool isPreviewUp;
        private void Start()
        {
            isPreviewUp = false;
            previousShroom = null;
        }
        
        private void Update()
        {
            closestShroom = Physics2D.OverlapCircle(transform.position, range, mushroomLayerMask);

            if (closestShroom != null && (!isPreviewUp || previousShroom != closestShroom) && !_objective.gamePaused)
            {
                mushroomPreview.DeactivatePreview();
                mushroomPreview.ActivatePreview(closestShroom.gameObject);
                previousShroom = closestShroom;
                isPreviewUp = true;
            }
            else if (closestShroom == null && isPreviewUp)
            {
                mushroomPreview.DeactivatePreview();
                isPreviewUp = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
            }
        }

        void PickUp()
        {
            if(_objective.GetMushroomName() != null)
            {
                if (closestShroom != null && closestShroom.name.Contains(_objective.GetMushroomName()))
                {
                    _objective.RemoveMushroomFromObjective();
                    _objective.GenerateObjective();
                    Destroy(closestShroom.gameObject);
                    _objective.IncreateMushroomPicked();
                    Audio.AudioManager.instance.Play("Mushroom Pickup");
                }
                else if (closestShroom != null)
                {
                    fadeoutAnimator.gameObject.SetActive(true);
                    fadeoutAnimator.Play("TextFadeout");
                    CancelInvoke();
                    Invoke("HideWrongMushroomPopup", popupDelayTime);
                }
            }
            else
            {
                return;
            }    
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        private void HideWrongMushroomPopup()
        {
            fadeoutAnimator.gameObject.SetActive(false);
        }
    }
}

