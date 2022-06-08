using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] public Transform explorer;
    [SerializeField] public GameObject[] rooms;
    [SerializeField] public GameObject startRoom;
    [HideInInspector] private LinkedList<GameObject> activeRooms;
    [HideInInspector] private Room lastRoom;
    [HideInInspector] private GameObject lastRoomTemplate;
    private void Start()
    { 
        if (rooms.Length < 1)
            Debug.LogError("No rooms selected to place.");

        activeRooms = new LinkedList<GameObject>();
        activeRooms.AddLast(startRoom);
        lastRoom = startRoom.GetComponent<Room>();
        CreateNextRoom();

    }

    private void Update()
    {
        if(lastRoom.transform.position.x - explorer.transform.position.x < lastRoom.roomWidth/2f)   
        {
            CreateNextRoom();
            if (activeRooms.Count > 3)
                DestroyFirst();
        }
    }

    public void CreateNextRoom()
    {
        GameObject[] matchingRooms = Array.FindAll(rooms, r => r.GetComponent<Room>().entryDoors == lastRoom.exitDoors && r.gameObject != lastRoomTemplate);
        if(matchingRooms.Length < 1)
            Debug.LogError("No matching rooms available.");

        GameObject nextRoom = matchingRooms[UnityEngine.Random.Range(0, matchingRooms.Length)];
        lastRoomTemplate = nextRoom;
        GameObject instantiatedRoom = Instantiate(nextRoom, new Vector3(lastRoom.transform.localPosition.x + lastRoom.roomWidth / 2 + nextRoom.GetComponent<Room>().roomWidth / 2f, 0), Quaternion.identity, transform);
        activeRooms.AddLast(instantiatedRoom);
        lastRoom = instantiatedRoom.GetComponent<Room>();
    }

    public void DestroyFirst()
    {
        GameObject first = activeRooms.First.Value;
        activeRooms.Remove(first);
        Destroy(first);
    }
}
