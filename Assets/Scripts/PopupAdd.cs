using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupAdd : MonoBehaviour
{
    public Image addImage;
    public Button closeButton;

    public List<Sprite> adds;
    // Start is called before the first frame update
    void Start()
    {
        addImage.sprite = adds[Random.Range(0, adds.Count - 1)];
        closeButton.onClick.AddListener(()=>{
            AudioManager.audioMgr.PlayUISFX("UIPositive");
            Destroy(gameObject);
        });
        gameObject.SetActive(false);
        AudioManager.audioMgr.StartCoroutine(PopDelay());
    }

    private IEnumerator PopDelay(){
        float delay = Random.Range(0.0f, 1.5f);
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(true);
        AudioManager.audioMgr.PlayUISFX("UINegative");
    }
}
