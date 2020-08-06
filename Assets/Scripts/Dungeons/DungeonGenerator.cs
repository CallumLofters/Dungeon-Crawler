using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    // PUBLIC VARIABLES 

    public GameObject RoomSpawnPoint;
    public GameObject[] Rooms;
    public int Level;
    public int Difficulty;
    public int AmountOfRooms;
    public int AmountOfNumberEnemies;

    public GameObject[] ChestsPrefabs;
    public GameObject[] MaterialsPrefabs;
    public GameObject[] FriendliesPrefabs;
    public GameObject[] EnemiesPrefabs;

    public int[][] FloorSpace;
    public int LowestX = 0;
    public int LowestZ = 0;
    public int HighestX = 0;
    public int HighestZ = 0;
    public int Columns;
    public int Rows;
    // PRIVATE VARIABLES

    public int NumberMaterialsInDungeon;
    public int NumberChests;
    public int NumberFriendlies;
    public int NumberEnemies;

    private GameObject DungeonHolder;
    private GameObject ChestHolder;
    private GameObject MaterialsHolder;
    private GameObject FriendliesHolder;
    private GameObject EnemiesHolder;

    private List<Room> PlacedRoomLocations = new List<Room>();
    private List<Vector3> CurrentLocations = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
        DungeonHolder = new GameObject("Dungeon");
        ChestHolder = new GameObject("Chests");
        MaterialsHolder = new GameObject("Materials");
        FriendliesHolder = new GameObject("Friendlies");
        EnemiesHolder = new GameObject("Enemies");

        GenerateDungeon();
        SpawnDungeon();
        GenerateNumbers();
        SetupFloorSpaceArray();
        PopulateRooms();
        // PopulateDungeon();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            PopulateRooms();
        }
    }

    void GenerateNumbers()
    {
        NumberMaterialsInDungeon = Random.Range(AmountOfRooms / 2, AmountOfRooms * 3);
        NumberChests = Random.Range(1, AmountOfRooms / 5);
        NumberFriendlies = Random.Range(0, 3);
        NumberEnemies = Random.Range(AmountOfRooms / 2, AmountOfRooms);

        LowestX = Mathf.Abs(-80);
        LowestX += 4;

        LowestZ = Mathf.Abs(LowestZ);
        LowestZ += 4;
        HighestX += 4;
        HighestZ += 4;

        Columns = Mathf.Abs(LowestX + HighestX + 1);
        Rows = Mathf.Abs(LowestZ + HighestZ + 1);
    }

    void SetupFloorSpaceArray()
    {
        // Set the tiles jagged array to the correct width.
        FloorSpace = new int[Columns][];

        // Go through all the tile arrays...
        for (int i = 0; i < FloorSpace.Length; i++)
        {
            // ... and set each tile array is the correct height.
            FloorSpace[i] = new int[Rows];
        }
    }

    void GenerateDungeon()
    {
        // CREATING FIRST ROOM FOR THE DUNGEON

        Room FirstRoom = new Room();
        FirstRoom.setLocation(new Vector3(0f, 0f, 0f));
        PlacedRoomLocations.Add(FirstRoom);
        CurrentLocations.Add(PlacedRoomLocations[0].getLocation());


        for (int i = 0; i < AmountOfRooms - 1; i++)
        {

            int x = Random.Range(0, i);
            Room PrevRoom = PlacedRoomLocations[x];
            Vector3 MoveRoom = new Vector3();
            Room NewRoom = new Room();
            Vector3 NewRoomLocation = PrevRoom.getLocation();
            int Direction = Random.Range(1, 5);
            int Clash = 0;
            if (PrevRoom.CheckDoors(Direction))
            {

                if (Direction == 1)
                {
                    // print("1");
                    MoveRoom = new Vector3(0f, 0f, 10f);
                    NewRoom.setDoors(3);
                }
                else if (Direction == 2)
                {
                    // print("2");
                    MoveRoom = new Vector3(10f, 0f, 0f);
                    NewRoom.setDoors(4);
                }
                else if (Direction == 3)
                {
                    // print("3");
                    MoveRoom = new Vector3(0f, 0f, -10f);
                    NewRoom.setDoors(1);
                }
                else if (Direction == 4)
                {
                    // print("4");
                    MoveRoom = new Vector3(-10f, 0f, 0f);
                    NewRoom.setDoors(2);
                }

                NewRoomLocation = NewRoomLocation + MoveRoom;
                PlacedRoomLocations[x].setDoors(Direction);
                //PlacedRoomLocations.Add(NewRoom);

                // print("NEW ROOM LOCATION " + NewRoomLocation);
                for (int location = 0; location < PlacedRoomLocations.Count - 1; location++)
                {
                    if (CurrentLocations.Contains(NewRoomLocation))
                    {
                        i--;
                        Clash = 1;
                        break;
                    }
                }

                if (Clash == 0)
                {
                    CurrentLocations.Add(NewRoomLocation);
                    NewRoom.setLocation(NewRoomLocation);
                    if (NewRoom.getLocation().x < LowestX)
                    {
                        LowestX = (int)NewRoom.getLocation().x;
                    }
                    else if (NewRoom.getLocation().x > HighestX)
                    {
                        HighestX = (int)NewRoom.getLocation().x;
                    }
                    if (NewRoom.getLocation().z < LowestZ)
                    {
                        LowestZ = (int)NewRoom.getLocation().z;
                    }
                    else if (NewRoom.getLocation().z > HighestZ)
                    {
                        HighestZ = (int)NewRoom.getLocation().z;
                    }
                    PlacedRoomLocations.Add(NewRoom);
                }
            }
            else
            {
                i--;
            }
            //print(
            //    "----------------------------------------------------------- \n" +
            //    "Room Number:           " + (i + 2) + "\n" +
            //    "Previous Room:         " + (x + 1) + "\n" +
            //    "Direction:             " + Direction + "\n" +
            //    "Previous Rooms Doors:  " + PlacedRoomLocations[x].getDoors() + "\n" +
            //    "Current Rooms Doors:   " + PlacedRoomLocations[i + 1].getDoors()
            //    );

        }
        //Instantiate(RoomSpawnPoint, movement, Quaternion.identity);

    }
    void SpawnDungeon()
    {
        //for (int i = 0; i < AmountOfRooms; i++)
        //{
        //    print(PlacedRoomLocations[i].getLocation());
        //}

        foreach (Room room in PlacedRoomLocations)
        {

            GameObject RoomInstance = Instantiate(Rooms[CalculateDoors(room)], room.getLocation(), Quaternion.identity);
            RoomInstance.transform.parent = DungeonHolder.transform;
        }
    }
    void PopulateRooms()
    {
        int TempRoom;
        int TempX;
        int TempZ;
        Vector3 ObjectLocation;
        int ObjectX;
        int ObjectZ;

        // GENERATE CHESTS IN DUNGEON
        for (int i = 0; i < NumberChests; i++)
        {
            TempRoom = Random.Range(0, AmountOfRooms);
            TempX = Random.Range(-4, 5);
            TempZ = Random.Range(-4, 5);
            ObjectLocation = PlacedRoomLocations[TempRoom].getLocation();
            ObjectLocation += new Vector3(TempX, 0f, TempZ);
            ObjectX = (int)ObjectLocation.x + LowestX;
            ObjectZ = (int)ObjectLocation.z + LowestZ;


            if (FloorSpace[ObjectX][ObjectZ] != 1)
            {
                FloorSpace[ObjectX][ObjectZ] = 1;
                GameObject ChestInstance = Instantiate(ChestsPrefabs[Random.Range(0, ChestsPrefabs.Length)], ObjectLocation, Quaternion.identity);
                ChestInstance.transform.parent = ChestHolder.transform;
            }
            else
            {
                i--;
            }
        }

        // GENERATE MATERIALS IN DUNGEON
        for (int i = 0; i < NumberMaterialsInDungeon; i++)
        {
            TempRoom = Random.Range(0, AmountOfRooms);

            TempX = Random.Range(0, 9) - 4;
            TempZ = Random.Range(0, 9) - 4;
            ObjectLocation = PlacedRoomLocations[TempRoom].getLocation();
            ObjectLocation += new Vector3(TempX, 0f, TempZ);
            ObjectX = (int)ObjectLocation.x + LowestX;
            ObjectZ = (int)ObjectLocation.z + LowestZ;
            print(
                "TempRoom = " + TempRoom + "\n" +
                "TempX = " + TempX + "\n" +
                "TempZ = " + TempZ + "\n" +
                "Material Location: " + ObjectLocation + "\n" +
                "ObjectX = " + ObjectX + "\n" +
                 "ObjectZ = " + ObjectZ + "\n"




                );
            print(FloorSpace[ObjectX][ObjectZ]);

            if (FloorSpace[ObjectX][ObjectZ] != 1)
            {
                FloorSpace[ObjectX][ObjectZ] = 1;
                GameObject MaterialsInstance = Instantiate(MaterialsPrefabs[Random.Range(0, MaterialsPrefabs.Length)], ObjectLocation, Quaternion.identity);
                MaterialsInstance.transform.parent = MaterialsHolder.transform;
            }
            else
            {
                i--;
            }


        }

        // GENERATE FRIENDLIES IN DUNGEON 
        for (int i = 0; i < NumberFriendlies; i++)
        {
            TempRoom = Random.Range(0, AmountOfRooms);
            TempX = Random.Range(0, 9) - 4;
            TempZ = Random.Range(0, 9) - 4;
            ObjectLocation = PlacedRoomLocations[TempRoom].getLocation();
            ObjectLocation += new Vector3(TempX, 0f, TempZ);
            ObjectX = (int)ObjectLocation.x + LowestX;
            ObjectZ = (int)ObjectLocation.z + LowestZ;


            if (FloorSpace[ObjectX][ObjectZ] != 1)
            {
                FloorSpace[ObjectX][ObjectZ] = 1;
                GameObject FriendlyInstance = Instantiate(FriendliesPrefabs[Random.Range(0, FriendliesPrefabs.Length)], ObjectLocation, Quaternion.identity);
                FriendlyInstance.transform.parent = FriendliesHolder.transform;
            }
            else
            {
                i--;
            }
        }

        //GENERATE ENEMIES IN DUNGEON
        for (int i = 0; i < NumberEnemies; i++)
        {
            TempRoom = Random.Range(0, AmountOfRooms);
            TempX = Random.Range(0, 9) - 4;
            TempZ = Random.Range(0, 9) - 4;
            ObjectLocation = PlacedRoomLocations[TempRoom].getLocation();
            ObjectLocation += new Vector3(TempX, 0f, TempZ);
            ObjectX = (int)ObjectLocation.x + LowestX;
            ObjectZ = (int)ObjectLocation.z + LowestZ;


            if (FloorSpace[ObjectX][ObjectZ] != 1)
            {
                FloorSpace[ObjectX][ObjectZ] = 1;
                GameObject EnemyInstance = Instantiate(EnemiesPrefabs[Random.Range(0, EnemiesPrefabs.Length)], ObjectLocation, Quaternion.identity);
                EnemyInstance.transform.parent = EnemiesHolder.transform;
            }
            else
            {
                i--;
            }
        }
    }

    int CalculateDoors(Room room)
    {
        int Doorvalue = room.getDoors();

        if (Doorvalue == 2)
        {
            return 0;
        }
        else if (Doorvalue == 3)
        {
            return 1;
        }
        else if (Doorvalue == 5)
        {
            return 2;
        }
        else if (Doorvalue == 7)
        {
            return 3;
        }
        else if (Doorvalue == 6)
        {
            return 4;
        }
        else if (Doorvalue == 10)
        {
            return 5;
        }
        else if (Doorvalue == 14)
        {
            return 6;
        }
        else if (Doorvalue == 15)
        {
            return 7;
        }
        else if (Doorvalue == 21)
        {
            return 8;
        }
        else if (Doorvalue == 35)
        {
            return 9;
        }
        else if (Doorvalue == 30)
        {
            return 10;
        }
        else if (Doorvalue == 42)
        {
            return 11;
        }
        else if (Doorvalue == 70)
        {
            return 12;
        }
        else if (Doorvalue == 105)
        {
            return 13;
        }
        else if (Doorvalue == 210)
        {
            return 14;
        }
        else
        {
            return 15;
        }

    }
}
