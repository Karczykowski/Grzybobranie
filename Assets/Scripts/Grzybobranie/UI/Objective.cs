using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Grzybobranie.UI
{
    public class Objective : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private List<string> mushrooms;
        private int mushroomsPicked;
        [SerializeField] private int pointRequired;
        [SerializeField] Player.PlayerMovement playerMovement;
        [SerializeField] General.MapGenerator mapGenerator;
        [SerializeField] MushroomPreview mushroomPreview;
        public bool gamePaused;

        private string mushroomName;

        private void Start()
        {
            mushroomsPicked = 0;
            gamePaused = false;
        }
        public string GetMushroomName()
        {
            return mushroomName;
        }

        public void GenerateObjective(string previousShroom = "")
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
            // while (targetMushroom == null || previousShroom.Contains(mushroomName)); <- upewnienie sie, ze grzyb w objectivie jest nowy
            objectiveText.text = "ZnajdŸ: " + mushroomName;
        }

        public void IncreateMushroomPicked(int count = 1)
        {
            mushroomsPicked += count;
            UpdatePointText();
        }

        public void UpdatePointText()
        {
            pointsText.text = "Punkty: " + mushroomsPicked;
            if(mushroomsPicked == pointRequired)
            {
                FinishLevel();
            }
        }

        public void FinishLevel()
        {
            Audio.AudioManager.instance.PlayOnce("Game Complete");
            gamePaused = true;
            levelCompletePanel.SetActive(true);
            playerMovement.DisablePlayerMovement();
            objectiveText.text = "";
            mushroomPreview.DeactivatePreview();
        }

        public void RestartGame()
        {
            levelCompletePanel.SetActive(false);
            playerMovement.EnablePlayerMovement();
            mushroomsPicked = 0;
            UpdatePointText();
            mapGenerator.GenerateMap();
            gamePaused = false;
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

