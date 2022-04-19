using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class OpeningNarration : MonoBehaviour
{
    public EmailTerminal script;
    //public GameObject emails;
    //public TMPro.TMP_Text email1body, email2body, email3body;
    //public TMPro.TMP_Text email1Subject, email2Subject, email3Subject;
    
    
    // Start is called before the first frame update
    void Start()
    {

        //GameObject email1go = getChildGameObject(emails, "Email1");
        //email1body = getChildGameObject(email1go, "EmailBody").GetComponent<TextMeshProUGUI>();
        //email2body = getChildGameObject(email2go, "EmailBody").GetComponent<TextMeshProUGUI>();
        //email3body = getChildGameObject(email3go, "EmailBody").GetComponent<TextMeshProUGUI>();

        //email1Subject = getChildGameObject(email1go, "SubjectLine").GetComponent<Text>();


        /*email1Subject.text = "Notice of User's demise: Qrow";
        email1body.text = "Hello Robyn, \n\n
            This is an automated notification for the death of Markus Crenshaw (Online Alias: Qrow).\n
            His online account has been disabled as a result of his death, and you are the only written associate in his TOS agreement.\n
            Please join the AVR event link within this email to go to the user's homepage and save whatever should be spared from the deletion process.\n
            Afterwards, you may hold a free complentary funeral service on the NetScape AVR, and invite up to 10 other users (may be increased with payments of $10 per user).\n
            \nQrow's account will be deleted in 3 days, regardless of actions taken by then. Please act accordingly.\n\n
            Thank you for your attention.\n\n
            Signed, \nNetScape User Registry System (URS)\n\n
            <u><b><color=#2081c8><link=1>Join NetScape AVR event</link></color></b></u>*/
        
    }

    //If looking for a child gameobject, find the gameobject by name and return the object (if none found, return null.
    public GameObject getChildGameObject(GameObject source, string name)
    {
        Transform[] children = source.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.gameObject.name == name) return child.gameObject;
        }

        return null;
    }
}
