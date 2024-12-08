using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

namespace Grzybobranie.UI
{
    public class Objective : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private GameObject levelPausePanel;
        [SerializeField] private List<string> mushrooms;
        private int mushroomsPicked;
        [SerializeField] private int pointRequired;
        [SerializeField] Player.PlayerMovement playerMovement;
        [SerializeField] General.MapGenerator mapGenerator;
        [SerializeField] MushroomPreview mushroomPreview;
        [SerializeField] private GameObject objectivePanel;
        [SerializeField] TextMeshProUGUI timeText;
        public bool gamePaused;

        public List<General.MushroomList> mushroomObjectives;
        public int currentObjectiveIndex;
        public List<string> currentMushroomsObjective;
        [SerializeField] Dialogue dialogue;
        public bool isObjectiveUp;
        public bool isObjectiveComplete;

        private string mushroomName;

        private float gameTime;

        [SerializeField] TextMeshProUGUI timeElapsedText;
        [SerializeField] TextMeshProUGUI timeElapsedTextPause;

        [SerializeField] General.TalkableNPC talkableNPC;

        private void Start()
        {
            mushroomsPicked = 0;
            gamePaused = false;
            currentObjectiveIndex = 0;
            isObjectiveUp = false;
            isObjectiveComplete = false;
            gameTime = 0f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseLevel();
            }

            if(!gamePaused)
            {
                gameTime += Time.deltaTime;
                timeText.SetText("Czas: " + Mathf.Floor(gameTime).ToString() + "s");
            }
        }
        public string GetMushroomName()
        {
            return mushroomName;
        }

        public void SetMushroomName(string _mushroomName)
        {
            mushroomName = _mushroomName;
        }

        public void ActivateObjective()
        {
            if(!isObjectiveUp)
            {
                isObjectiveUp = true;
                objectivePanel.SetActive(true);
                currentMushroomsObjective = mushroomObjectives[currentObjectiveIndex].stringi.ToList();
                GenerateObjective();
                if(dialogue.currentQuestIndex < mapGenerator.GetMushroomsCount())
                {
                    mapGenerator.EnableSpecificMushroom(dialogue.currentQuestIndex);
                }
            }
        }

        public void ActivateNextObjective()
        {
            if(currentMushroomsObjective.Count == 0 && currentObjectiveIndex < mushroomObjectives.Count - 1)
            {
                currentObjectiveIndex++;
                dialogue.currentQuestIndex++;
                dialogue.ResetDialogue();
                isObjectiveUp = false;
            }
            else
            {
                return;
            }
        }
        public void GenerateObjective()
        {
            if (mushrooms.Count == 0)
                return;

            int checker = 0;
            GameObject targetMushroom;
            if (currentMushroomsObjective.Count == 0)
            {
                objectiveText.text = "Wszystko znalezione! Mogê wracaæ do krasnoludka";
                mushroomName = null;
                isObjectiveComplete = true;
                return;
            }
            else
            {
                isObjectiveComplete = false;
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
            timeElapsedText.SetText("Czas: " + Mathf.Floor(gameTime).ToString() + "s");
            playerMovement.DisablePlayerMovement();
            objectiveText.text = "";
            mushroomPreview.DeactivatePreview();
        }

        public void PauseLevel()
        {
            if(talkableNPC.GetIsDialogueUp())
            {
                return;
            }
            
            gamePaused = true;
            levelPausePanel.SetActive(true);
            timeElapsedTextPause.SetText("Czas: " + Mathf.Floor(gameTime).ToString() + "s");
            playerMovement.DisablePlayerMovement();
        }

        public void ResumeGame()
        {
            gamePaused = false;
            levelPausePanel.SetActive(false);
            playerMovement.EnablePlayerMovement();
        }

        public void RestartGame()
        {
            levelCompletePanel.SetActive(false);
            playerMovement.EnablePlayerMovement();
            mushroomsPicked = 0;
            UpdatePointText();
            mapGenerator.GenerateMap();
            gamePaused = false;
            ResetAllObjectives();
            gameTime = 0f;
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ResetAllObjectives()
        {
            currentMushroomsObjective = null;
            mushroomName = null;
            dialogue.ResetDialogue();

            currentObjectiveIndex = 0;
            dialogue.currentQuestIndex = 0;
            isObjectiveUp = false;
            isObjectiveComplete = false;
        }
    }
}

