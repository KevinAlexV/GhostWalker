using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public BlockPatten pattern;
    public bool[,] data;
    public TetrisGrid grid;

    [TextArea]
    public string dataString;

    [TextArea]
    public string storyString;

    // Start is called before the first frame update
    void Awake()
    {
        SetData(pattern);
        dataString = TetrisGrid.PrintData(data);
    }


    private void SetData(BlockPatten p){
        switch(p){
            case BlockPatten.I:
                data = new bool[,]{{true, true, true, true}};
                break;
            case BlockPatten.J:
                data = new bool[,]{{true, false, false}, {true, true, true}};
                break;
            case BlockPatten.L:
                data = new bool[,]{{false, false, true}, {true, true, true}};
                break;
            case BlockPatten.O:
                data = new bool[,]{{true, true}, {true, true}};
                break;
            case BlockPatten.S:
                data = new bool[,]{{false, true, true}, {true, true, false}};
                break;
            case BlockPatten.T:
                data = new bool[,]{{false, true, false}, {true, true, true}};
                break;
            case BlockPatten.Z:
                data = new bool[,]{{true, true, false}, {false, true, true}};
                break;
            default:
                throw new System.Exception("Unimplemented block type");
        }
        RotateChildren();
    }

    public void Rotate(bool positive){
        if(positive){ //+90 degrees
            Transpose();
            ReverseRows();
            RotateChildren();
        } else { //-90 degrees
            ReverseRows();
            Transpose();
            RotateChildren();
        }
        dataString = TetrisGrid.PrintData(data);
    }

    public void ToggleCollisions(bool enabled){
        foreach(Transform t in transform){
            Collider c;
            if(t.gameObject.TryGetComponent<Collider>(out c)){
                c.enabled = enabled;
            }
        }
    }

    private void RotateChildren(){
        int count = 0;
        for(int i = 0; i < data.GetLength(0); i++){
            for(int j = 0; j < data.GetLength(1); j++){
                if(data[i,j]){
                    transform.GetChild(count).transform.localPosition = new Vector3(i, 0, j);
                    count++;
                }
            }
        }
    }

    private void Transpose(){
        bool[,] newData = new bool[data.GetLength(1),data.GetLength(0)];
        for(int i = 0; i < data.GetLength(0); i++){
            for(int j = 0; j < data.GetLength(1); j++){
                newData[j,i] = data[i,j];
            }
        }
        data = newData;
    }

    private void ReverseRows(){
        for(int i = 0; i < data.GetLength(0); i++){
            for(int j = 0; j < Mathf.FloorToInt(data.GetLength(1) / 2f); j++){
                bool temp = data[i,data.GetLength(1)-j-1];
                data[i,data.GetLength(1)-j-1] = data[i,j];
                data[i, j] = temp;
            }
        }
    }
}

public enum BlockPatten{
    I,
    J,
    L,
    O,
    S,
    T,
    Z

}
