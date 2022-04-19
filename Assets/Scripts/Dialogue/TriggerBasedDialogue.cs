using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBasedDialogue : MonoBehaviour, ISelectableBehaviour
{
    private LocationNPCTriggers npcs;

    [SerializeField]
    private bool isMajorNPC, opensDoor;

    public List<DialogueUI.DialogueLine> beforeMajorNPC = new List<DialogueUI.DialogueLine>();
    public List<DialogueUI.DialogueLine> afterMajorNPC = new List<DialogueUI.DialogueLine>();

    public void Start()
    {
        npcs = GameObject.Find("NPCs").GetComponent<LocationNPCTriggers>();
    }

    public void clicked()
    {

        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
        GameObject.Find("Player").GetComponent<CameraMovement>().canLook = false;

        if (npcs.majorNPCTalkedTo)
        {

            GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(afterMajorNPC);
        }
        else
        {
            GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(beforeMajorNPC);
        }

        GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
        {
            if (isMajorNPC && npcs.majorNPCTalkedTo != true)
                npcs.majorNPCTalkedTo = true;

            GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
            GameObject.Find("Player").GetComponent<CameraMovement>().canLook = true;
        };

        if (opensDoor){
            npcs.disableDoor();
            GameObject.Find("Player").GetComponent<PlayerMovement>().canFly = true;
        }
            
    }
    
}
