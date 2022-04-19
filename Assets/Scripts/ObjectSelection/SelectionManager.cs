using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Material highlight;

    private Material defaultMaterial;
    private Transform selectedObject;

    private int selectedId = -1;
    private int newSelectedId = 0;

    public bool selectedAgain()
    {
        return selectedId == newSelectedId;
    }

    void Update()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material = defaultMaterial;
            selectedObject = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<ISelectableBehaviour>() != null
                && Vector3.Distance(hit.transform.position, GameObject.Find("Player").GetComponent<Transform>().position) <= 10)
            {
                
                if (hit.transform.GetComponent<Renderer>() != null)
                {
                    defaultMaterial = hit.transform.GetComponent<Renderer>().material;
                    hit.transform.GetComponent<Renderer>().material = highlight;
                }
                selectedObject = hit.transform;
            }
            
        }
        if (Input.GetMouseButtonDown(0) && DialogueUI.canClick)
        {
            if (selectedObject != null && selectedObject.GetComponent<ISelectableBehaviour>() != null)
            {
                if (hit.transform.gameObject.GetInstanceID() == selectedId)
                {
                    newSelectedId = selectedId;
                }
                else
                {
                    selectedId = newSelectedId;
                    newSelectedId = hit.transform.gameObject.GetInstanceID();
                }
                selectedObject.GetComponent<ISelectableBehaviour>().clicked();
            }
        }
    }

    
}
