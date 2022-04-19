using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGrid : MonoBehaviour
{
    public bool[,] data;
    public List<TetrisBlock> placedBlocks;
    private float cooldown = .5f;
    private float lastAttempt;

    //temp for testing
    [TextArea]
    public string dataString;
    
    void Start()
    {

        lastAttempt = Time.time;
        data = new bool[6,6]{
            {true, true, true, true, true, true},
            {true, true, false, false, false, true},
            {true, true, false, false, false, true},
            {true, true, false, false, true, true},
            {true, true, false, false, false, false},
            {true, true, true, true, true, true}};
    }

    void Update(){
        
    }

    public bool TryPlaceBlock(int x, int y, TetrisBlock block){

        float currentTime = Time.time;

        //Feel free to use this code if you think a cooldown would help. Otherwise, just delete it :P
        //Debug.Log("CTime: " + currentTime + " | LTime: " + lastAttempt + " | Math: " + (currentTime - lastAttempt));

        //if (currentTime - lastAttempt >= cooldown)
       //{
            lastAttempt = Time.deltaTime;

            Debug.Log($"Trying to place at ({x}, {y}):");
            PrintData(block.data);
            PrintData(data);

            bool[,] dataCopy = (bool[,]) data.Clone(); //attempt to add block to grid and discard if collision detected
            if(y + block.data.GetLength(0) > data.GetLength(0) || x + block.data.GetLength(1) > data.GetLength(1)){ //check if the block fits within the bounds of the grid
                return false;
            } else if(x < 0 || y < 0){
                return false;
            }

            for(int i = y; i - y < block.data.GetLength(0) && i < data.GetLength(0); i++){
                for(int j = x; j - x < block.data.GetLength(1) && j < data.GetLength(1); j++){
                    if(block.data[i-y, j-x] && dataCopy[i,j]){ //If both the grid and the block have a bit in the current cell
                        return false;
                    } else {
                        dataCopy[i,j] = block.data[i-y, j-x];
                    }
                }
            }
            placedBlocks.Add(block);
            data = dataCopy;
            Debug.Log($"Placed at ({x}, {y}):");
            dataString = PrintData(data);
            if(placedBlocks.Count > 2){
                Debug.Log("Win");
                TetrisPlacer placer = GameObject.FindObjectOfType<TetrisPlacer>();
                enabled = false;
                placer.CompletePuzzle();
            } 
            return true;
        //}

        //return false;
    }

    public bool RemoveBlock(int x, int y, TetrisBlock block){
        if(!placedBlocks.Contains(block)) return false;

        placedBlocks.Remove(block);

        bool[,] dataCopy = (bool[,]) data.Clone(); //attempt to add block to grid and discard if collision detected
        if(y + block.data.GetLength(0) > data.GetLength(0) || x + block.data.GetLength(1) > data.GetLength(1)){ //check if the block fits within the bounds of the grid
            return false;
        } else if(x < 0 || y < 0){
            return false;
        }

        for(int i = y; i - y < block.data.GetLength(0) && i < data.GetLength(0); i++){
            for(int j = x; j - x < block.data.GetLength(1) && j < data.GetLength(1); j++){
                if(block.data[i-y, j-x] && dataCopy[i,j]){ //If both the grid and the block have a bit in the current cell
                    dataCopy[i,j] = false;
                }
            }
        }
        data = dataCopy;
        dataString = PrintData(data);
        return true;
    }

    public static string PrintData(bool[,] d){
        string output = "\n";
        for(int i = 0; i < d.GetLength(0); i++){
            for(int j = 0; j < d.GetLength(1); j++){
                output += d[i,j] ? "X" : "-";
            }
            output += "\n";
        }
        //Debug.Log(output);
        return output;
    }
}
