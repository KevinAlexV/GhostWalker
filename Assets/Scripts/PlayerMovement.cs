using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 5f)]
    public float movespeed;

    [HideInInspector]
    public bool canMove = true;
    public bool canFly = false;
    private bool sprinting = false;

    private float bobTimer = 0;
    private float defaultYPos = 0;
    private Camera cam;

    private float stepAudioTimer = 0;
    private float stepAudioTimerMax = 0.5f;
    private float stepAudioTimerMaxSprint = 0.3f;

    private int movementLocks = 0; //super hacky solution

    //TEMP Prototype stuff
    void Start(){
        //var addedDialogue = new List<DialogueUI.DialogueLine>() { new DialogueUI.DialogueLine("Player", $"Alright, I need to find me some launch codes and get out of this place. I'm sure someone here is willing to sell them to me. (Press I for inventory)") };
        //GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(addedDialogue);
    }

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        defaultYPos = cam.transform.localPosition.y;
    }

    private bool floor = true;
    private void OnCollisionStay()
    {
        floor = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        floor = false;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        Vector3 velocity = ((transform.forward * inputDir.y) + (transform.right * inputDir.x)) * movespeed;

        if(canFly){
            GetComponent<Rigidbody>().useGravity = false;
            if(Input.GetKey(KeyCode.Space)){
                velocity += Vector3.up * movespeed;
            } else if(Input.GetKey(KeyCode.LeftControl)){
                velocity += Vector3.down * movespeed;
            }
        } else if(!floor)
        {
            velocity = new Vector3(velocity.x, -9.8f, velocity.z);
        } else
        {
            velocity = new Vector3(velocity.x, 0, velocity.z);
        }
        

        if (!canMove || DialogueUI.dialogueRunning)
        {
            GetComponent<Rigidbody>().velocity = velocity * 0;
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            GetComponent<Rigidbody>().velocity = velocity * 2;
            sprinting = true;
        } 
        else
        {
            GetComponent<Rigidbody>().velocity = velocity;
            sprinting = false;
        }

        // bobbing effect
        if (GetComponent<Rigidbody>().velocity.x != Vector3.zero.x && GetComponent<Rigidbody>().velocity.z != Vector3.zero.z)
        {
            handleBobbing();
        } else
        {
            cam.transform.localPosition = new Vector3(
            cam.transform.localPosition.x,
            defaultYPos,
            cam.transform.localPosition.z);
            stepAudioTimer = 0;
        }
    }

    void handleBobbing()
    {
        bobTimer += Time.deltaTime * (sprinting ? 18 : 14);
        stepAudioTimer -= Time.deltaTime;

        cam.transform.localPosition = new Vector3(
            cam.transform.localPosition.x,
            defaultYPos + Mathf.Sin(bobTimer) * (sprinting ? 0.1f : 0.05f),
            cam.transform.localPosition.z);

        if (stepAudioTimer <= 0.2 && sprinting)
        {
            stepAudioTimer = stepAudioTimerMax;
            if (AudioManager.audioMgr != null)
            {
                if (Random.value > 0.4f)
                {
                    AudioManager.audioMgr.PlaySFX(4, vol: 0.25f);
                } 
                else
                {
                    AudioManager.audioMgr.PlaySFX(5, vol: 0.25f);
                }
                
            }
            
        } 
        else if (stepAudioTimer <= 0)
        {
            stepAudioTimer = stepAudioTimerMax;
            if (AudioManager.audioMgr != null)
            {
                if (Random.value > 0.4f)
                {
                    AudioManager.audioMgr.PlaySFX(4, vol: 0.25f);
                }
                else
                {
                    AudioManager.audioMgr.PlaySFX(5, vol: 0.25f);
                }
            }
        }
    }

    public void SetMovementLock(){
        movementLocks++;
        if(movementLocks == 1){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canMove = false;
            GetComponent<CameraMovement>().canLook = false;
        }
    }

    public void ClearMovementLock(){
        movementLocks--;
        if(movementLocks < 1){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canMove = true;
            GetComponent<CameraMovement>().canLook = true;
            movementLocks = 0;
        }
    }

}
