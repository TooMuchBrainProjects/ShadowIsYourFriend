using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] public Transform explorer;
    [SerializeField] public GameObject[] rooms;
    [SerializeField] public GameObject startRoom;
    [HideInInspector] private LinkedList<GameObject> activeRooms;
    private void Start()
    { 
        if (rooms.Length < 1)
            Debug.LogError("No rooms selected to place.");
        activeRooms = new LinkedList<GameObject> ();
        activeRooms.AddLast(startRoom);
        CreateNextRoom();

    }

    private void Update()
    {
        //if(explorer.x - activeRooms.Last().transform.x < )
    }

    public void CreateNextRoom()
    {
        Room lastRoom = activeRooms.Last().GetComponent<Room>();
        IEnumerable<GameObject> matchingRooms = rooms.Where((r) => r.GetComponent<Room>().entryDoors == lastRoom.exitDoors);
        GameObject nextRoom = matchingRooms.ElementAt(Random.Range(0, matchingRooms.Count()));

        activeRooms.AddLast(Instantiate(nextRoom, new Vector3(lastRoom.transform.localPosition.x + nextRoom.GetComponent<Room>().roomWidth, 0), Quaternion.identity, transform));
    }
}
