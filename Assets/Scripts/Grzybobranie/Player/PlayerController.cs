using System.Collections;
using System.Collections.Generic;
using Grzybobranie.Objects;
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
        //[SerializeField] private float popupDelayTime = 0f;
        [SerializeField] private Animator fadeoutAnimator;
        [SerializeField] private float highlightScale;
        [SerializeField] TextMeshProUGUI wrongMushroomText;
        private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
        private Collider2D closestShroom;
        private Collider2D previousShroom;
        private bool isPreviewUp;
        private bool isHighlighted;

        private float waitTime = 0f;
        private float baseWaitTime = 1f;
        private float currentWaitTime = 1f;
        private void Start()
        {
            isPreviewUp = false;
            isHighlighted = false;
            previousShroom = null;
        }
        
        private void Update()
        {
            closestShroom = GetClosestMushroom();

            if (previousShroom != null && previousShroom != closestShroom && isHighlighted)
            {
                if (previousShroom.gameObject != null)
                {
                    ResetMushroom(previousShroom.gameObject);
                }
                isHighlighted = false;
            }

            if (closestShroom != null && (!isPreviewUp || previousShroom != closestShroom) && !_objective.gamePaused)
            {
                mushroomPreview.DeactivatePreview();
                mushroomPreview.ActivatePreview(closestShroom.gameObject);
                HighlightMushroom(closestShroom.gameObject);
                previousShroom = closestShroom;
                isPreviewUp = true;
                isHighlighted = true;
            }
            else if (closestShroom == null && isPreviewUp)
            {
                if (previousShroom != null && previousShroom.gameObject != null)
                {
                    ResetMushroom(previousShroom.gameObject);
                }
                mushroomPreview.DeactivatePreview();
                isPreviewUp = false;
            }


            if (Input.GetKeyDown(KeyCode.E) && waitTime <= 0f)
            {
                PickUp();
            }
            else if (waitTime > 0f)
            {
                waitTime -= Time.deltaTime;

                if (waitTime <= 0f)
                {
                    wrongMushroomText.gameObject.SetActive(false);
                }
            }
        }
                
        public Collider2D GetClosestMushroom()
        {
            Collider2D[] mushrooms = Physics2D.OverlapCircleAll(transform.position, range, mushroomLayerMask);

            Collider2D closest = null;
            float minDistance = Mathf.Infinity;

            foreach(var mushroom in mushrooms)
            {
                float distance = Vector2.Distance(transform.position, mushroom.transform.position);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    closest = mushroom;
                }
            }
            return closest;
        }
        void HighlightMushroom(GameObject mushroom)
        {
            SpriteRenderer renderer = mushroom.GetComponent<SpriteRenderer>();
            mushroom.transform.localScale = Vector3.one * highlightScale;

            if (!originalColors.ContainsKey(mushroom))
            {
                originalColors[mushroom] = renderer.color;
            }

            Color originalColor = originalColors[mushroom];
            Color newColor = originalColor + new Color(0.1f, 0.1f, 0.1f, 0);
            renderer.color = newColor;
        }

        void ResetMushroom(GameObject mushroom)
        {
            SpriteRenderer renderer = mushroom.GetComponent<SpriteRenderer>();
            mushroom.transform.localScale = new Vector3(0.4f, 0.4f, 1f);

            if (originalColors.ContainsKey(mushroom))
            {
                renderer.color = originalColors[mushroom];
                originalColors.Remove(mushroom);
            }
        }

        void PickUp()
        {
            if (_objective.GetMushroomName() != null)
            {
                if (closestShroom != null && closestShroom.name.ToLower().Contains(_objective.GetMushroomName()))
                {
                    _objective.RemoveMushroomFromObjective();
                    _objective.GenerateObjective();
                    Destroy(closestShroom.gameObject);
                    _objective.IncreateMushroomPicked();
                    Audio.AudioManager.instance.Play("Mushroom Pickup");

                    waitTime = 0f;
                    currentWaitTime = baseWaitTime;
                }
                else if (closestShroom != null)
                {
                    wrongMushroomText.gameObject.SetActive(true);

                    waitTime = currentWaitTime;
                    currentWaitTime *= 1.5f;
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

