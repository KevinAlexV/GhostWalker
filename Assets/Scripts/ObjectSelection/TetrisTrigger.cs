using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisTrigger : MonoBehaviour, ISelectableBehaviour
{
    private bool hasClicked = false;

    // Start is called before the first frame update
    public void clicked()
    {
        if (!hasClicked) {

            hasClicked = true;

            var dialogueBox = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();

            var openDialogue = new List<DialogueUI.DialogueLine>() {
                new DialogueUI.DialogueLine("System", "Initializing data recovery mode."),
                new DialogueUI.DialogueLine("System", "Please left click to place data block, and right click to remove placement."),
                new DialogueUI.DialogueLine("System", "Use the scroll wheel to rotate data blocks.")

                //new DialogueUI.DialogueLine("System", "Q will select next block, and SPACE will exit data recovery mode.")
            };


            dialogueBox.RunDialogue(openDialogue);
            dialogueBox.onDialogueEnd += () =>
            {
                try { GameObject.Find("Player").GetComponentInChildren<TetrisPlacer>().enabled = true; }
                catch { Debug.Log("Tetris placer doesn't exist. Ignoring line 30, tetrisTrigger.cs");  }
            };

        }
    }
}
