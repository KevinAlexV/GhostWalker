using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CaptchaOption: Image, IPointerClickHandler
{
    public CaptchaPuzzleManager manager;
    public bool triggerFail = false;
    private GameObject correctIcon;
    public void OnPointerClick(PointerEventData p){
        if(triggerFail){
            manager.Fail();
        } else {
            if(correctIcon != null) return;
            correctIcon = Instantiate(manager.passIcon.gameObject, transform);
            AudioManager.audioMgr.PlayUISFX("UIPositive");
            correctIcon.SetActive(true);
            manager.SelectCorrect();
        }
    }

    public void Reset(Sprite s, bool tFail){
        sprite = s;
        triggerFail = tFail;
        Destroy(correctIcon);
    }
}
