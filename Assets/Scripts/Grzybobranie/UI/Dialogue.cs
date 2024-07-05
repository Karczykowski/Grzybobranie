using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Grzybobranie.UI
{
    public class Dialogue : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;
        public string[] lines;
        public float textSpeed;

        private int index;

        [SerializeField] General.TalkableNPC talkableNPC;
        [SerializeField] UI.Objective objective;

        private void Start()
        {
            index = 0;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index]; 
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
            index = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        void NextLine()
        {
            if(index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                talkableNPC.DisableDialogueBox();
                objective.ActivateNextObjective();
            }
        }

        public void ResetDialogue()
        {
            index = 0;
        }    
    }
}


