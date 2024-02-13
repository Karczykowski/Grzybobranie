using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grzybobranie.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float range = 1;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private UI.Objective _objective;
        [SerializeField] private UI.MushroomPreview mushroomPreview;
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
            closestShroom = Physics2D.OverlapCircle(transform.position, range, layerMask);

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
            if (closestShroom != null && closestShroom.name.Contains(_objective.GetMushroomName()))
            {
                _objective.GenerateObjective(closestShroom.name);
                Destroy(closestShroom.gameObject);
                _objective.IncreateMushroomPicked();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}

