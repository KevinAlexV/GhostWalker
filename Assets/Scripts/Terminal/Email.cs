using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Email : MonoBehaviour
{
    public List<Email.EmailEntry> unread = new List<Email.EmailEntry>();
    public List<Email.EmailEntry> saved = new List<Email.EmailEntry>();

    public TextMeshProUGUI bodyDisplay;

    [SerializeField]
    private GameObject emailPrefab;

    [System.Serializable]
    public class EmailEntry
    {

        [SerializeField] // Email Subject line
        public string subject;
        [SerializeField] // Email body
        public string body;
        [SerializeField] // If there is associated dialogue with the email
        public List<DialogueUI.DialogueLine> emailDialogue;
        

        public EmailEntry(string subject, string body, List<DialogueUI.DialogueLine> dialogue)
        {
            this.subject = subject;
            this.body = body;

            this.emailDialogue = dialogue;

        }
    }

    private void Start()
    {
        float posX = 350, posY = 820;

        var emailScreen = GameObject.Find("EmailSubject").GetComponent<Transform>();

        foreach (Email.EmailEntry email in unread)
        {

            var newEmail = Instantiate(emailPrefab, new Vector3(posX, posY,0), Quaternion.identity, emailScreen.transform);
            newEmail.GetComponentInChildren<TextMeshProUGUI>().text = email.subject;

            newEmail.GetComponent<Button>().onClick.AddListener(() => {
                bodyDisplay.richText.Equals(true);
                bodyDisplay.text = email.body;
                if(email.emailDialogue.Count != 0) //&& email.emailDialogue[0].text != "")
                    GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(email.emailDialogue);
            });

            posY -= 110;

            Debug.Log("Email generated" + email);

        }

        foreach (Email.EmailEntry email in unread)
        {



        }

    }
    
}
