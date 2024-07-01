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
        [SerializeField] private GameObject objectivePanel;
        public bool gamePaused;

        public List<General.MushroomList> mushroomObjectives;
        public int currentObjectiveIndex;
        public List<string> currentMushroomsObjective;

        private string mushroomName;

        private void Start()
        {
            mushroomsPicked = 0;
            gamePaused = false;
            currentObjectiveIndex = 0;
        }
        public string GetMushroomName()
        {
            return mushroomName;
        }

        public void ActivateNextObjective()
        {
            objectivePanel.SetActive(true);
            currentMushroomsObjective = mushroomObjectives[currentObjectiveIndex].stringi;
            GenerateObjective();
        }
        public void GenerateObjective()
        {
            if (mushrooms.Count == 0)
                return;

            int checker = 0;
            GameObject targetMushroom;
            if (currentMushroomsObjective.Count == 0)
            {
                objectiveText.text = "Wszystko znalezione!";
                return;
            }

            do
            {
                mushroomName = currentMushroomsObjective[Random.Range(0, currentMushroomsObjective.Count)];
                targetMushroom = GameObject.Find(mushroomName + " Variant(Clone)");
                checker++;
                if (checker > 100)
                {
                    Debug.Log("Blad znajdywania celu, na mapie nie ma grzyba, który powinien byæ celem");
                    break;
                }
            } while (targetMushroom == null);
            // while (targetMushroom == null || previousShroom.Contains(mushroomName)); <- upewnienie sie, ze grzyb w objectivie jest nowy
            objectiveText.text = "ZnajdŸ: " + mushroomName;
        }

        public void RemoveMushroomFromObjective()
        {
            currentMushroomsObjective.Remove(mushroomName);
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

