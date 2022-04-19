using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeExit : MonoBehaviour, ISelectableBehaviour
{
    public List<DialogueUI.DialogueLine> complete = new List<DialogueUI.DialogueLine>();
    public List<DialogueUI.DialogueLine> incomplete = new List<DialogueUI.DialogueLine>();

    public void clicked()
    {
        var dialogueUI = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();
        if (InventoryManager.instance.items.ContainsKey("puppet account"))
        {
        
            GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
            {
                LevelManager.Instance.LoadLevel("Ending");
            };

            dialogueUI.RunDialogue(complete);

        }
        else
        {
            dialogueUI.RunDialogue(incomplete);
        }
    }
}
