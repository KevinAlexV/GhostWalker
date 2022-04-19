using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordTerminal : MonoBehaviour, ISelectableBehaviour
{
    private LocationNPCTriggers npcs;
    private bool isInteracting = false;
    public int terminalStage = 0;

    [SerializeField]
    private GameObject terminal;

    [SerializeField]
    private Canvas sceneCanvas;

    [SerializeField]
    public GameObject chatlogPrefab;

    [SerializeField]
    private TMP_InputField password;

    private string qrowspassword = "Robyn";
    private GameObject chatLog;

    //________________________________________________________________________________________________

    //Complete puzzle function, run when the password is solved, from update function. 
    public void CompletePuzzle()
    {

        var solvedDialogue = new List<DialogueUI.DialogueLine>() {
            new DialogueUI.DialogueLine("System", "Welcome Qrow. Please note, you only have one open thread. When closed, it will be greyed out and no longer accessible until it's deletion date, where it will be removed from your logs."),
            new DialogueUI.DialogueLine("Robyn", "Ok, let's check this out."),
        };

        var d = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();

        d.RunDialogue(solvedDialogue);

        d.onDialogueEnd += () =>
        {
            npcs.readyToExit = true;
            GameObject.Find("Player").GetComponent<PlayerMovement>().ClearMovementLock();

        };

        DialogueUI.DialogEventEnd openDataPrefabHandler = null;





        openDataPrefabHandler = () => {
            chatLog = Instantiate(chatlogPrefab, sceneCanvas.transform);
            chatLog.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(ClosePuzzleData);
            GameObject.Find("Player").GetComponent<PlayerMovement>().SetMovementLock();
            d.onDialogueEnd -= openDataPrefabHandler;
        };
        d.onDialogueEnd += openDataPrefabHandler;
        d.RunDialogue(solvedDialogue);
    }


    //Close puzzle data, which is instantiated when puzzle is completed. These are post-data handlers.
    public void ClosePuzzleData()
    {
        Destroy(chatLog);

        var postSolveDialogue = new List<DialogueUI.DialogueLine>() {
            new DialogueUI.DialogueLine("Robyn", "A sock puppet account..."),
            new DialogueUI.DialogueLine("Robyn", "Aren't those usually used for illegal activities?"),
            new DialogueUI.DialogueLine("Robyn", "Sounds like he got caught up on the wrong end of a deal."),
            new DialogueUI.DialogueLine("Robyn", "Well, let's see if I can find this person. 'Tradebot_024'.")

        };

        var d = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();
        GameObject.Find("NPCs").GetComponent<LocationNPCTriggers>().readyToExit = true;
        d.onDialogueEnd += () =>
        {
            isInteracting = false;
        };

        d.RunDialogue(postSolveDialogue);
    }



    //________________________________________________________________________________________________

    public void clicked()
    {
        if (!isInteracting)
        { 
             GameObject.Find("Player").GetComponent<PlayerMovement>().SetMovementLock();

            isInteracting = true;
            terminal.active = true;
        }
    }

    public void nextStage()
    {
        Debug.Log($"Next stage");

        switch (terminalStage)
        {
            //Temrminal unregistered user
            case 0:
                break;
            //Terminal locked
            case 1:
                break;
            //Terminal unlocked
            case 2:
                break;
            default:
                break;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        npcs = GameObject.Find("NPCs").GetComponent<LocationNPCTriggers>();
    }

    void Update()
    {
        if (isInteracting)
        {
            if (npcs.majorNPCTalkedTo)
            {
                string currentInput2 = password.text.ToString().ToLower();

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    string currentInput = password.text.ToString().ToLower();

                    if (currentInput.Equals(qrowspassword.ToLower()))
                    {
                        Debug.Log("Correct Password");
                        AudioManager.audioMgr.PlayUISFX("UIStart");
                        terminal.active = false;

                        CompletePuzzle();
                    }
                    else
                    {
                        AudioManager.audioMgr.PlayUISFX("UINegative");
                        Debug.Log("Incorrect Password: " + currentInput + " is not " + qrowspassword);
                        password.text = "";
                    }
                }
                else if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetMouseButtonDown(1)))
                {
                    terminal.active = false;
                    GameObject.Find("Player").GetComponent<PlayerMovement>().ClearMovementLock();

                    isInteracting = false;
                }

            }
            else
            {
                terminal.active = false;
                GameObject.Find("Player").GetComponent<PlayerMovement>().ClearMovementLock();

                isInteracting = false;
            }
        }
    }
}