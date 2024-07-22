using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Grzybobranie.UI
{
    public class Dialogue : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;
        public List<General.MushroomList> lines;
        public float textSpeed;
        public int currentQuestIndex;
        private int currentLineIndex;

        [SerializeField] General.TalkableNPC talkableNPC;
        [SerializeField] UI.Objective objective;

        private void Start()
        {
            currentLineIndex = 0;
            currentQuestIndex = 0;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (textComponent.text == lines[currentQuestIndex].stringi[currentLineIndex])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[currentQuestIndex].stringi[currentLineIndex]; 
                }
            }
        }

        public void StartDialogue()
        {
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        public void StartDialogueAnew()
        {
            textComponent.text = string.Empty;
            currentLineIndex = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            foreach (char c in lines[currentQuestIndex].stringi[currentLineIndex].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        void NextLine()
        {
            if(currentLineIndex < lines[currentQuestIndex].stringi.Count - 1)
            {
                currentLineIndex++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                talkableNPC.DisableDialogueBox();
                objective.ActivateObjective();
            }
        }

        public void ResetDialogue()
        {
            currentLineIndex = 0;
        }    
    }
}


