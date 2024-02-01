using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grzybobranie.UI
{
    public class MushroomPreview : MonoBehaviour
    {
        [SerializeField] private Image mushroomPreviewPanel;
        public void setImage(Sprite mushroomSprite)
        {
            mushroomPreviewPanel.sprite = mushroomSprite;
        }

        public void ActivatePreview(GameObject Mushroom)
        {
            mushroomPreviewPanel.gameObject.SetActive(true);
            setImage(Mushroom.GetComponent<Objects.Mushroom>().previewSprite);
        }

        public void DeactivatePreview()
        {
            mushroomPreviewPanel.gameObject.SetActive(false);
        }
    }
}
