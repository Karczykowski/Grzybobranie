using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grzybobranie.Player
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private UI.Objective _objective;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
            }
        }

        void PickUp()
        {
            Collider2D itemPicked = Physics2D.OverlapCircle(transform.position, range, layerMask);

            if (itemPicked != null && itemPicked.name.Contains(_objective.GetMushroomName()))
            {
                Destroy(itemPicked.gameObject);
                _objective.GenerateObjective();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}

