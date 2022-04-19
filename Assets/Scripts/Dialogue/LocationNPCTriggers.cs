using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationNPCTriggers : MonoBehaviour
{
    public bool majorNPCTalkedTo = false;
    public bool readyToExit = false;
    public GameObject door;

    public void disableDoor()
    {

        if (door.active)
            door.active = false;

    }
}
