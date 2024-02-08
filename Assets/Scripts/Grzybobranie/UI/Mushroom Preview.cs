using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grzybobranie.UI
{
    public class MushroomPreview : MonoBehaviour
    {
        [SerializeField] private Image mushroomPreviewPanel;
        [SerializeField] private GameObject protectedIcon;
        [SerializeField] private GameObject edibleIcon;
        [SerializeField] private GameObject uneatableIcon;
        [SerializeField] private GameObject poisonousIcon;
        public void SetImage(Sprite mushroomSprite)
        {
            mushroomPreviewPanel.sprite = mushroomSprite;
        }

        public void ActivatePreview(GameObject Mushroom)
        {
            mushroomPreviewPanel.gameObject.SetActive(true);

            Objects.Mushroom _mushroom = Mushroom.GetComponent<Objects.Mushroom>();
            SetImage(_mushroom.previewSprite);

            if (_mushroom.isProtected)
                protectedIcon.SetActive(true);
            if (_mushroom.isEdible)
                edibleIcon.SetActive(true);
            if (_mushroom.isUneatable)
                uneatableIcon.SetActive(true);
            if (_mushroom.isPoisonous)
                poisonousIcon.SetActive(true);
        }

        public void DeactivatePreview()
        {
            mushroomPreviewPanel.gameObject.SetActive(false);
            protectedIcon.SetActive(false);
            edibleIcon.SetActive(false);
            uneatableIcon.SetActive(false);
            poisonousIcon.SetActive(false);
        }
    }
}
