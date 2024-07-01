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
        void Start()
        {
            textComponent.text = string.Empty;
            StartDialogue();
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

        void StartDialogue()
        {
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
    }
}


