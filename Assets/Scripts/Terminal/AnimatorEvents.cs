using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    [SerializeField]
    private EmailTerminal script;

    void logoAnimEnd()
    {
        script.terminalStage += 1;
        script.nextStage();
        this.gameObject.SetActive(false);
    }

}
