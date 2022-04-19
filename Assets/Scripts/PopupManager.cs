using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    private static PopupManager _manager;

    public GameObject popupPrefab;
    public int minAdds;
    public int maxAdds;

    private int spawnedAdds;

    void Start(){
        if(_manager != null){
            Destroy(this);
        }
        _manager = this;
        gameObject.SetActive(false);
    }

    void Update(){
        if(transform.childCount == 0){
            GameObject.Find("Player").GetComponent<PlayerMovement>().ClearMovementLock();
            gameObject.SetActive(false);
        }
    }

    public static void SpawnAdds(){
        GameObject.Find("Player").GetComponent<PlayerMovement>().SetMovementLock();
        _manager.gameObject.SetActive(true);

        _manager.spawnedAdds = Random.Range(_manager.minAdds, _manager.maxAdds);
        for(int i = 0; i < _manager.spawnedAdds; i++){
            var add = Instantiate<GameObject>(_manager.popupPrefab, _manager.transform);
            Rect bound = _manager.GetComponent<RectTransform>().rect;
            Vector2 randPos = new Vector2(
                Random.Range(add.GetComponent<RectTransform>().rect.width * 0.5f, bound.width - add.GetComponent<RectTransform>().rect.width),
                -Random.Range(add.GetComponent<RectTransform>().rect.height * 0.5f, bound.height - add.GetComponent<RectTransform>().rect.height)
            );
            add.GetComponent<RectTransform>().anchoredPosition = randPos;
        }
    }
}
