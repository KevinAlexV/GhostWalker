using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Dictionary<string,string> items = new Dictionary<string,string>();
    
    void Awake(){
        if(instance != null){
            Destroy(this);
        }
        instance = this;
        items.Add("download priority pass", "gives the user priority in the download queue");
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            PrintInventory();
        }
    }

    public void AddItem(string name, string desc){
        var addedDialogue = new List<DialogueUI.DialogueLine>() { new DialogueUI.DialogueLine("System", $"*Added 1 {name} to inventory*") };
        //GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(addedDialogue);
        items.Add(name, desc);
    }

    public void PrintInventory(){
        var inv = "";
        foreach(string item in items.Keys){
            inv += $"{item} : {items[item]}\n";
        }
        var inventoryDialogue = new List<DialogueUI.DialogueLine>() { new DialogueUI.DialogueLine("Inventory", inv) };
        GameObject.Find("dialoguebox").GetComponent<DialogueUI>().RunDialogue(inventoryDialogue);
    }
}
