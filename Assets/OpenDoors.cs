using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour, ISelectableBehaviour
{
    public List<GameObject> ObjectsToDisable;

    public List<DialogueUI.DialogueLine> unlockDialogue = new List<DialogueUI.DialogueLine>();

    private bool unlocked = false;

    public void clicked()
    {
        if (!unlocked)
        { 
            if (unlockDialogue.Count != 0)
            {
                var dialogueUI = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();

                /*GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
                {
                    foreach (GameObject door in ObjectsToDisable)
                    {
                        door.GetComponent<ParticleSystem>().Stop();
                        door.GetComponent<BoxCollider>().enabled = false;
                    }
                };*/

                dialogueUI.RunDialogue(unlockDialogue);
            }

       

            Debug.Log("Doors now being disabled...");
            foreach (GameObject door in ObjectsToDisable)
            {
                door.GetComponent<ParticleSystem>().Stop();
                door.GetComponent<BoxCollider>().enabled = false;
            }

            unlocked = true;
        }
    }
}
