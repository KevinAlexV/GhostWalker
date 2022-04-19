using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1EndingDialogue : MonoBehaviour, ISelectableBehaviour
{
    public GameObject activatedObject;
    public List<DialogueUI.DialogueLine> interactDialogue = new List<DialogueUI.DialogueLine>();

    public void clicked()
    {
        var dialogueUI = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();

        Debug.Log(LevelManager.endingCount);

        GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
        {
            activatedObject.active = true;
            this.gameObject.active = false;
        };

        dialogueUI.RunDialogue(interactDialogue);
    }
}