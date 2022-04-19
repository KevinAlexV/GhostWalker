using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeBot027Dialogue : MonoBehaviour, ISelectableBehaviour
{
    public List<GameObject> ObjectsToDisable;
   
    public string requestedItem;
    public string offeredItem;
    public string offeredItemDesc;
    public List<DialogueUI.DialogueLine> offerDialogue = new List<DialogueUI.DialogueLine>();
    public List<DialogueUI.DialogueLine> tradedDialogue = new List<DialogueUI.DialogueLine>();
    
    public void clicked(){
        var dialogueUI = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();

        if (InventoryManager.instance.items.ContainsKey(requestedItem))
        {
            InventoryManager.instance.AddItem(offeredItem, offeredItemDesc);
            InventoryManager.instance.items.Remove(requestedItem);
            
            GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
            {
                foreach (GameObject door in ObjectsToDisable)
                {
                    door.GetComponent<ParticleSystem>().Stop();
                    door.GetComponent<BoxCollider>().enabled = false;
                }
            };

            dialogueUI.RunDialogue(tradedDialogue);
            
        }
        else {dialogueUI.RunDialogue(offerDialogue);}
        
    }


}