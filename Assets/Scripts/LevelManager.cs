using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static int endingCount = 0;

    private static LevelManager _instance;
    public static LevelManager Instance {
        get{
            return _instance;
        }
    }

    public Animator anim;

    /* LEVEL NAME DEFINITIONS */
    public List<string> levels;

    public delegate void OnLoad();

    // Start is called before the first frame update
    void Start()
    {
        if(_instance != null){
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(string levelName){
        if(levels.Contains(levelName)){
            //Debug.Log(levelName);
            int musicIndex = levels.IndexOf(levelName);
            try { AudioManager.audioMgr.ChangeMusic(musicIndex); }
            catch { Debug.Log("Could not load music, error with Audio Manager."); }
            StartCoroutine(LoadScene(levelName));
        } else {
            Debug.LogError($"Tried to load level '{levelName}' that didn't exist");
        }
    }

    private IEnumerator LoadScene(string sceneName, OnLoad onLoadHandler = null){
        anim.SetTrigger("SceneClose");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        anim.SetTrigger("SceneStart");
        if(onLoadHandler != null){
            onLoadHandler();
        }

        PopupManager.SpawnAdds();
    }
}
