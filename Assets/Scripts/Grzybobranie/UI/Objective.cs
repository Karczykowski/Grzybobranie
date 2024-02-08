using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Grzybobranie.UI
{
    public class Objective : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private List<string> mushrooms;

        private string mushroomName;

        public string GetMushroomName()
        {
            return mushroomName;
        }

        public void GenerateObjective()
        {
            if (mushrooms.Count == 0)
                return;

            int checker = 0;
            GameObject targetMushroom;
            do
            {
                mushroomName = mushrooms[Random.Range(0, mushrooms.Count)];
                targetMushroom = GameObject.Find(mushroomName + " Variant(Clone)");
                checker++;
                if (checker > 100)
                {
                    Debug.Log("Blad znajdywania celu");
                    break;
                }
            } while (targetMushroom == null);
            objectiveText.text = "Znajdü: " + mushroomName;
        }
    }
}

