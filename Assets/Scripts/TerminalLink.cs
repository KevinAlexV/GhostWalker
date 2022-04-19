using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TerminalLink : MonoBehaviour, IPointerClickHandler
{
    TMP_Text pTextMeshPro;
    public delegate void OnClick();
    public event OnClick onClick;
    void Start(){
        pTextMeshPro = GetComponent<TMP_Text>();
    }
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Could get him");
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, eventData.position, eventData.pressEventCamera);
        if( linkIndex != -1 ) { // was a link clicked?
            TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];

            if(onClick != null){
                onClick();
            }
        }
    }
}
