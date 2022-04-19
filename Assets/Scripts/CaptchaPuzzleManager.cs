using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptchaPuzzleManager : MonoBehaviour
{
    public List<CaptchaSet> captchas;
    public List<CaptchaOption> optionImages;
    public Text question;
    public Image failIcon;
    public Image passIcon;

    private int currentIndex;
    private int correctGuesses = 0;

    void Start(){
        foreach(CaptchaOption c in optionImages){
            c.manager = this;
        }
        NextQuestion();
        GameObject.Find("Player").GetComponent<PlayerMovement>().SetMovementLock();

        var startDialogue = new List<DialogueUI.DialogueLine>() {
            new DialogueUI.DialogueLine("System", "Security Alert, Please prove your humanity."),
        };
        GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(startDialogue);
    }

    public void NextQuestion(){
        var current = captchas[currentIndex];
        for(int i = 0; i < optionImages.Count; i++){
            optionImages[i].Reset(current.options[i], current.incorrectIndex == i);
        }
        question.text = current.question;
    }

    public void SelectCorrect(){
        correctGuesses++;
        if(correctGuesses > 2){
            Pass();
        }
    }

    public void Pass(){
        Debug.Log("Passed");
        StartCoroutine(PassEnumerator());
        currentIndex++;
        correctGuesses = 0;
        if(currentIndex < captchas.Count){
            NextQuestion();
        } else{
            Complete();
        }
    }

    public void Fail(){
        Debug.Log("Failed");
        StartCoroutine(FailEnumerator());
        currentIndex = 0;
        correctGuesses = 0;
        NextQuestion();
    }

    public void Complete(){
        var completeDialogue = new List<DialogueUI.DialogueLine>() { 
            new DialogueUI.DialogueLine("System", "Humanity Confirmed, Access Granted"),
            new DialogueUI.DialogueLine("System", "Welcome to Qrow's Homepage"),
            new DialogueUI.DialogueLine("System", "WARN || No such user 'Qrow' exists."),
            new DialogueUI.DialogueLine("System", "ERROR || A critical error has occured (0x2A45). Account required to instantiate user."),
            new DialogueUI.DialogueLine("System", "INFO || Use of sock puppet accounts is a bannable offense."),
            new DialogueUI.DialogueLine("Robyn", "Well, I'm finally here. I hope you're happy Qrow. Your death was all it took..."),
            new DialogueUI.DialogueLine("Robyn", "So what the hell happened to you?"),
        };
        GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(completeDialogue);
        GameObject.Find("dialoguebox").GetComponent<DialogueUI>().onDialogueEnd += () =>
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().ClearMovementLock();
            transform.localScale = new Vector3(0, 0, 0);
        };
        AudioManager.audioMgr.PlayUISFX("UIStart");
    }

    public IEnumerator FailEnumerator(){
        failIcon.gameObject.SetActive(true);
        AudioManager.audioMgr.PlayUISFX("UINegative");
        yield return new WaitForSeconds(2.0f);
        failIcon.gameObject.SetActive(false);
        yield return null;
    }

    public IEnumerator PassEnumerator(){
        passIcon.gameObject.SetActive(true);
        AudioManager.audioMgr.PlayUISFX("UIPositive");
        yield return new WaitForSeconds(2.0f);
        passIcon.gameObject.SetActive(false);
        yield return null;
    }
}

[System.Serializable]
public struct CaptchaSet{
    public string question;
    public int incorrectIndex;
    public List<Sprite> options;
}