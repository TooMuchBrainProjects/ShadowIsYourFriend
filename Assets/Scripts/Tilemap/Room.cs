using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomDoors { Top, Bottom }

public class Room : MonoBehaviour
{
    [Header("Logic Variables")]
    [SerializeField] public float roomWidth = 36;
    [SerializeField] public RoomDoors entryDoors;
    [SerializeField] public RoomDoors exitDoors;

}
