using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBayEntrance : MonoBehaviour, ISelectableBehaviour
{
    public InteractableObject bookshelf;

    public void clicked()
    {

        if (bookshelf.interactedWith)
        {
            var startDialogue = new List<DialogueUI.DialogueLine>() {
                new DialogueUI.DialogueLine("System", "Please use panel to select destination."),
                new DialogueUI.DialogueLine("Robyn", "Hmmm... Ok, I should try the Church of Investments."),
                new DialogueUI.DialogueLine("Robyn", "Let's see..."),
                new DialogueUI.DialogueLine("Robyn", "There we go!"),
            };
            GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(startDialogue);
            GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
            {
                LevelManager.Instance.LoadLevel("Cathedral");
            };
        }
        else
        {
            var startDialogue = new List<DialogueUI.DialogueLine>() {
                new DialogueUI.DialogueLine("Robyn", "Uhh... What is this thing?"),
                new DialogueUI.DialogueLine("System", "Please use panel to select destination."),
                new DialogueUI.DialogueLine("Robyn", "Is this like a fast travel thing?"),
                new DialogueUI.DialogueLine("Robyn", "...Not sure where to go though."),
            };
            GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(startDialogue);


        }
    }
}
