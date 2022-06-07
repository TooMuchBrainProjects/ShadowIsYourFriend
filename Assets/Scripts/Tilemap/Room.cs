using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomDoors { Top, Bottom }

public class Room : MonoBehaviour
{
    [SerializeField] public float roomWidth = 36;
    [SerializeField] public RoomDoors entryDoors;
    [SerializeField] public RoomDoors exitDoors;

}
