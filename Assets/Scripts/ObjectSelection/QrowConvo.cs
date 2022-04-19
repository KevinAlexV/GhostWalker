using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QrowConvo : MonoBehaviour, ISelectableBehaviour
{
    public List<DialogueUI.DialogueLine> stay = new List<DialogueUI.DialogueLine>();
    public List<DialogueUI.DialogueLine> leave = new List<DialogueUI.DialogueLine>();

    public Material purpleSky, outdoors;

    private List<DialogueUI.DialogueLine> intro;


    private bool introPlayed = false;

    public void Start()
    {
        intro = new List<DialogueUI.DialogueLine>()
        {
                new DialogueUI.DialogueLine("Robyn", "Qrow???"),
                new DialogueUI.DialogueLine("Qrow", "Robyn? Is that you??"),
                new DialogueUI.DialogueLine("Robyn", "Uhh, yeah. Wh-why are you here? I thought you were dead?"),
                new DialogueUI.DialogueLine("Qrow", "Dead!? What do you mean dead? I'm... dead? How... what went wrong?"),
                new DialogueUI.DialogueLine("Robyn", "You mean your upload?"),
                new DialogueUI.DialogueLine("Qrow", "...H-how do you know about that?"),
                new DialogueUI.DialogueLine("Robyn", "I've been doing some digging. I found out why: You wanted to stay here, and uploading was a step in that process, right?"),
                new DialogueUI.DialogueLine("Qrow", "Um... Yeah, kinda. I... I wasn't planning on dying though. "),
                new DialogueUI.DialogueLine("Robyn", "Is that not a part of the process? I figure once you upload, you'd cease to be."),
                new DialogueUI.DialogueLine("Qrow", "I just wanted to backup my mind. Staying here permanently was just an arrangement I'd have to make on the outside, where I can live here without needing to worry about my body, but it'd still be alive."),
                new DialogueUI.DialogueLine("Qrow", "This? ...This wasn't supposed to happen."),
                new DialogueUI.DialogueLine("Robyn", "Then why back up your mind??"),
                new DialogueUI.DialogueLine("Qrow", "Well... You're seeing why. I wanted to live here forever... but I was too scared to just upload my mind and die in the outside world."),
                new DialogueUI.DialogueLine("Qrow", "I guess I got my wish though..."),
                new DialogueUI.DialogueLine("Robyn", "...But...Why? You know I wouldn't have been able to visit you."),
                new DialogueUI.DialogueLine("Qrow", "Robyn, that was a choice. Not a restriction, not a restraint, and not a reason. You choose not to enter the AVR."),
                new DialogueUI.DialogueLine("Qrow", "Haven't you seen why this place is so interesting to me? The world? The people? The feeling of freedom? It's incomparable to the outside world."),
                new DialogueUI.DialogueLine("Robyn", "I..."),
                new DialogueUI.DialogueLine("Qrow", "Here, I can be anyone I want to be. See anything I want."),
                new DialogueUI.DialogueLine("Qrow", "Even the things I love"),
        };

        if (stay.Count == 0)
        {

            stay = new List<DialogueUI.DialogueLine>()
            {
                new DialogueUI.DialogueLine("Qrow","So I wanted to stay, yeah."),
                new DialogueUI.DialogueLine("Robyn", "..."),
                new DialogueUI.DialogueLine("Qrow","Robyn, I'm stuck here. Forever. I... Want to ask."),
                new DialogueUI.DialogueLine("Qrow","After all you've seen, do... You want to stay here too? With me?"),
                new DialogueUI.DialogueLine("Qrow","Err, sorry that makes it sound weird. I know you hate NetScape, but I also know that the people here do too."),
                new DialogueUI.DialogueLine("Qrow","We all are trying to make the AVR a better place for everyone, regardless of NetScape. So you'd fit right in."),
                new DialogueUI.DialogueLine("Qrow","...But I also just want to hang out again. As friends. Can you join me?"),
                new DialogueUI.DialogueLine("Robyn","...I know people are trying to make a difference, but to stay here permanently? I just got here dude. "),
                new DialogueUI.DialogueLine("Qrow","Yet you also hate it out there! We've both commented on how shitty the city is. How annoying it is that NetScape, and other conglomerates are trying to rule the outside."),
                new DialogueUI.DialogueLine("Qrow","Why not just join a world free of that responsibility?"),
                new DialogueUI.DialogueLine("Robyn","...Fine."),
                new DialogueUI.DialogueLine("Qrow","Fine?? As in you'll actually stay?"),
                new DialogueUI.DialogueLine("Robyn","No, not permanently, not yet. But... I'll be here for much longer, yes. It seems like I can work here anyway, so... I guess I could truly live a life worth living."),
                new DialogueUI.DialogueLine("Robyn","But I'm not uploading my mind or anything like that... Not yet anyway."),
                new DialogueUI.DialogueLine("Qrow","You're doing more than I could ask for, thank you Robyn."),
                new DialogueUI.DialogueLine("Qrow","I... I'm looking forward to the future. For the first time, in a long time."),
                new DialogueUI.DialogueLine("Robyn","...Me too Qrow. "),
                new DialogueUI.DialogueLine("Robyn","Me too. "),
            };

            
        }

        if (leave.Count == 0)
        {
            leave = new List<DialogueUI.DialogueLine>()
            {
                new DialogueUI.DialogueLine("Qrow","So I wanted to stay, yeah."),
                new DialogueUI.DialogueLine("Robyn", "..."),
                new DialogueUI.DialogueLine("Qrow","Robyn, I'm stuck here. Forever. I... Want to ask."),
                new DialogueUI.DialogueLine("Qrow","After all you've seen, do... You want to stay here too? With me?"),
                new DialogueUI.DialogueLine("Qrow","Err, sorry that makes it sound weird. I know you hate NetScape, but I also know that the people here do too."),
                new DialogueUI.DialogueLine("Qrow","We all are trying to make the AVR a better place for everyone, regardless of NetScape. So you'd fit right in."),
                new DialogueUI.DialogueLine("Qrow","...But I also just want to hang out again. As friends. Can you join me?"),
                new DialogueUI.DialogueLine("Robyn","...I know people are trying to make a difference, but to stay here permanently? I just got here dude. "),
                new DialogueUI.DialogueLine("Qrow","Yet you also hate it out there! We've both commented on how shitty the city is. How annoying it is that NetScape, and other conglomerates are trying to rule the outside."),
                new DialogueUI.DialogueLine("Qrow","Why not just join a world free of that responsibility?"),
                new DialogueUI.DialogueLine("Robyn","I...Can't Qrow."),
                new DialogueUI.DialogueLine("Qrow","Can't? Or won't?"),
                new DialogueUI.DialogueLine("Robyn","I won't. Qrow, reality can often be disappointing. But running away from it won't solve anything."),
                new DialogueUI.DialogueLine("Robyn","You're just delaying the inevitable."),
                new DialogueUI.DialogueLine("Qrow","So you don't understand after all. You're just like before. Like you always were. Stubborn."),
                new DialogueUI.DialogueLine("Robyn","Qrow, I do understand. I know why you wanted to, and I can't blame you for choosing this path. But I refuse to run. I'll make a difference, you'll see."),
                new DialogueUI.DialogueLine("Robyn","You? I want you to find your happiness here. This is your reality now. Despite all the suffering out there, I do think you can still make a difference here. But don't ignore the issue. Don't ignore the pain."),
                new DialogueUI.DialogueLine("Robyn","And... I know you can. I know you will. You've definitely made something of a mark here, given all the conversations I've had. So keep going. Keep being... You."),
                new DialogueUI.DialogueLine("Qrow","...I'm gonna miss you, man."),
                new DialogueUI.DialogueLine("Robyn","I'm not going anywhere, I'll still come see you. This won't be my life, sure, but I'll still visit."),
                new DialogueUI.DialogueLine("Qrow","Heh... I'm holding you to that."),
                new DialogueUI.DialogueLine("Robyn","I wouldn't expect anything less."),
                new DialogueUI.DialogueLine("Robyn","C'mon, let's make a difference."),
                new DialogueUI.DialogueLine("Robyn","Together."),
            };
        }


    }

    public void clicked()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
        GameObject.Find("Player").GetComponent<CameraMovement>().canLook = false;

        var dialogueBox = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();

        if (!introPlayed)
        {
            dialogueBox.RunDialogue(intro);
            dialogueBox.onDialogueEnd += () =>
            {
                changeWorld();

                GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
                GameObject.Find("Player").GetComponent<CameraMovement>().canLook = true;
                introPlayed = true;
            };
        }
        else
        { 
            if (LevelManager.endingCount >= 10)
                dialogueBox.RunDialogue(stay);
            else
                dialogueBox.RunDialogue(leave);

            dialogueBox.onDialogueEnd += () =>
            {
                StartCoroutine(waitBeforeEnd());
            };
        }
    }

    private void changeWorld()
    {
        RenderSettings.skybox = outdoors;
    }

    IEnumerator waitBeforeEnd()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
