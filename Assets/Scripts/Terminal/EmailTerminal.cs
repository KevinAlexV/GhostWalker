using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EmailTerminal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    public GameObject nslogo, screen, email, emailbody;
    
    private Animator animator;
    private SpriteRenderer fadeImage, background;
    private float fadeTime = 3f;

    public int terminalStage = 0;

    public void Start()
    {

        //AudioManager.audioMgr.ChangeMusic(cutsceneMusic);

        animator = nslogo.GetComponent<Animator>();
        background = screen.GetComponent<SpriteRenderer>();
        
        nextStage();

    }
    
    public void nextStage()
    {
        Debug.Log($"Next stage");
        
        switch (terminalStage)
        {
            case 0:
                startUpSequence();
                break;
            case 1:
                animator = background.GetComponent<Animator>();
                animator.Play("FadeToWhite", 0);
                email.SetActive(true);
                break;
            case 2:
                LevelManager.Instance.LoadLevel("Home");
                break;
            default:
                Debug.Log($"<color=red>Error: Invalid state of terminal.</color>");
                break;
        }
        
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void startUpSequence()
    {
        animator.Play("Logo", 0);

        Debug.Log("Animating...");
        
    }

    public void OnPointerEnter(PointerEventData eventData) { }
    public void OnPointerExit(PointerEventData eventData) { }
    public void OnPointerUp(PointerEventData eventData) { }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer clicked");

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(emailbody.GetComponent<TMP_Text>(), eventData.position, eventData.pressEventCamera);

        // Handle new Link selection.
         if (linkIndex != -1)
        {  
            Debug.Log("Current Link index is  " + linkIndex);
            terminalStage++;
            nextStage();

            if (linkIndex == 1)
            {
                Debug.Log("Link was selected");

            }
            
        }
    }
}
