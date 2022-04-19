using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPlacer : MonoBehaviour
{
    private TetrisBlock _currentBlock;
    public TetrisBlock CurrentBlock {
        get{
            return _currentBlock;
        }
        set {
            _currentBlock = value;
            if(value != null) dataPreviewText.text = _currentBlock.storyString;
        }
    }
    public List<TetrisBlock> blocks;
    public TetrisGrid grid;

    public Canvas canvas;

    public GameObject tetrisPreviewPrefab;
    private GameObject tetrisPreview;
    public GameObject puzzleCompletePrefab;
    private GameObject puzzleComplete;
    private TMPro.TMP_Text dataPreviewText;

    private bool dataClosed = false;
    private bool canPickup = true;

    //When enabled, tetris preview is added to tetris puzzle and displays text during a block being selected.
    void OnEnable(){
        tetrisPreview = Instantiate(tetrisPreviewPrefab, canvas.transform);
        dataPreviewText = tetrisPreview.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        CurrentBlock = blocks[0];
        blocks.RemoveAt(0);
        CurrentBlock.gameObject.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(CurrentBlock == null){
            if(dataClosed){
                GameObject.Find("Player").GetComponent<PlayerMovement>().ClearMovementLock();
                Destroy(puzzleComplete);
                Destroy(this);
            }
            return;
        }

        /*if(Input.GetKeyDown(KeyCode.Q)){
            blocks.Insert(0, CurrentBlock);
            CurrentBlock.gameObject.SetActive(false);
            CurrentBlock = blocks[blocks.Count - 1];
            blocks.RemoveAt(blocks.Count - 1);
            CurrentBlock.gameObject.SetActive(true);
            AudioManager.audioMgr.PlayUISFX("UIPositive");

        } else if(Input.GetKeyDown(KeyCode.E)){
            blocks.Add(CurrentBlock);
            CurrentBlock.gameObject.SetActive(false);
            CurrentBlock = blocks[0];
            blocks.RemoveAt(0);
            CurrentBlock.gameObject.SetActive(true);
            AudioManager.audioMgr.PlayUISFX("UIPositive");
        }*/

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if(Input.mouseScrollDelta.y != 0){
            CurrentBlock.Rotate(Input.mouseScrollDelta.y > 0);
            AudioManager.audioMgr.PlayUISFX("UIPing");
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            TetrisBlock newBlock;
            if(Input.GetMouseButtonDown(1) && hit.collider.gameObject.transform.parent.TryGetComponent<TetrisBlock>(out newBlock)){
                blocks.Add(CurrentBlock);
                CurrentBlock.gameObject.SetActive(false);
                CurrentBlock = newBlock;
                CurrentBlock.ToggleCollisions(false);
                CurrentBlock.gameObject.SetActive(true);
                PickUp();
            }
            Vector3 newPos = hit.point;
            newPos.y = 1f;
            newPos.x = Mathf.Round(newPos.x);
            newPos.z = Mathf.Round(newPos.z);
            CurrentBlock.transform.position = newPos;
        }


        if(Input.GetMouseButtonDown(0)){
            Place();
        }
    }

    private void Place(){
        int x = Mathf.RoundToInt(CurrentBlock.transform.position.z - grid.transform.position.z);
        int y = Mathf.RoundToInt(CurrentBlock.transform.position.x - grid.transform.position.x);
        Debug.Log($"{x}, {y}");
        if(grid.TryPlaceBlock(x, y, CurrentBlock)){
            CurrentBlock.ToggleCollisions(true);
            if(blocks.Count > 0){
                CurrentBlock = blocks[blocks.Count - 1];
                blocks.RemoveAt(blocks.Count - 1);
                CurrentBlock.gameObject.SetActive(true);
            } else {
                CurrentBlock = null;
            }
            AudioManager.audioMgr.PlayUISFX("UIStart");
        } else {
            Debug.Log($"Couldn't place, {x}, {y}");
            AudioManager.audioMgr.PlayUISFX("UINegative");
        }
    }

    private void PickUp(){
        if (!canPickup)
        {
            return;
        }
        AudioManager.audioMgr.PlayUISFX("UIStop");
        int x = Mathf.RoundToInt(CurrentBlock.transform.position.z - grid.transform.position.z);
        int y = Mathf.RoundToInt(CurrentBlock.transform.position.x - grid.transform.position.x);
        grid.RemoveBlock(x, y, CurrentBlock);
    }

    //When puzzle is completed, do the following
    public void CompletePuzzle()
    {
        canPickup = false;
        Destroy(tetrisPreview);
        var solvedDialogue = new List<DialogueUI.DialogueLine>() { 
            new DialogueUI.DialogueLine("System", "File restoration complete."),
            new DialogueUI.DialogueLine("System", "Data restored: 24%. Displaying uncorrupted data.")
        };
        var d = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();
        DialogueUI.dialogueRunning = true;
        DialogueUI.DialogEventEnd openDataPrefabHandler = null;
        
        openDataPrefabHandler = ()=>{
            puzzleComplete = Instantiate(puzzleCompletePrefab, canvas.transform);
            puzzleComplete.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(ClosePuzzleData);
            GameObject.Find("Player").GetComponent<PlayerMovement>().SetMovementLock();
            d.onDialogueEnd -= openDataPrefabHandler;
        };
        d.onDialogueEnd += openDataPrefabHandler;
        d.RunDialogue(solvedDialogue);
    }

    public void ClosePuzzleData(){
        
        print("closing");
        StartCoroutine(CloseDataDelay());
        
    }

    private IEnumerator CloseDataDelay()
    {
        var postSolveDialogue = new List<DialogueUI.DialogueLine>() {
            new DialogueUI.DialogueLine("Robyn", "Qrow... What happened?"),
            new DialogueUI.DialogueLine("Robyn", "You would think this would be enough to get fired, but..."),
            new DialogueUI.DialogueLine("Robyn", "I guess the boss of this company isn't very interested in suspicious activity."),
            new DialogueUI.DialogueLine("Robyn", "Well, let's try this bar then. Maybe they'll have more info.")

        };
        print("waiting");
        var d = GameObject.Find("dialoguebox").GetComponent<DialogueUI>();
        GameObject.Find("NPCs").GetComponent<LocationNPCTriggers>().readyToExit = true;
        yield return new WaitForSeconds(0.8f);
        print("running");
        d.RunDialogue(postSolveDialogue);
        dataClosed = true;
    }
}
