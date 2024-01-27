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
        void Start()
        {
            GenerateObjective();
        }

        public string GetMushroomName()
        {
            return mushroomName;
        }

        public void GenerateObjective()
        {
            if (mushrooms.Count == 0)
                return;

            mushroomName = mushrooms[Random.Range(0, mushrooms.Count)];
            objectiveText.text = "Znajdü: " + mushroomName;
        }
    }
}

