using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDialogue : MonoBehaviour, ISelectableBehaviour
{

    public List<DialogueUI.DialogueLine> objectDialogue = new List<DialogueUI.DialogueLine>();

    public void clicked()
    {

        if(LevelManager.endingCount >= 10) { }
        

    }
}