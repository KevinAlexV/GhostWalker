using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarExit : MonoBehaviour, ISelectableBehaviour
{
    [SerializeField]
    private LocationNPCTriggers npcs;

    public List<DialogueUI.DialogueLine> complete = new List<DialogueUI.DialogueLine>();
    public List<DialogueUI.DialogueLine> incomplete = new List<DialogueUI.DialogueLine>();

    public void clicked()
    {
        var dialogueUI = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();
        if (npcs.readyToExit)
        {
            dialogueUI.RunDialogue(complete);
            GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
            {
                LevelManager.Instance.LoadLevel("PirateBay");
            };
        }
        else
        {
            dialogueUI.RunDialogue(incomplete);
        }
    }
}