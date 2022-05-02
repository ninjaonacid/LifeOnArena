using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject[] Rooms;
    public Transform container;
    private Transform _lastRoom = null;
    [SerializeField] private Transform _startRoom;
    private RoomController _RoomController;
    void Start()
    {
        _lastRoom = _startRoom;
        
       
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    void InitializeLevel()
    {

    }

   void DungeonGeneration()
    {
        
    }
}
