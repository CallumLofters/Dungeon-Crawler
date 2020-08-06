using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public Vector3 Location;
    public int Doors = 1;


    // SETTERS
    public void setLocation(Vector3 newLocation)
    {
        Location = newLocation;
    }

    public void setDoors(int doorCheck)
    {
        if (doorCheck == 1)
        {
            Doors *= 2;
        }
        else if (doorCheck == 2)
        {
            Doors *= 3;
        }
        else if (doorCheck == 3)
        {
            Doors *= 5;
        }
        else if (doorCheck == 4)
        {
            Doors *= 7;
        }
    }

    // GETTERS

    public Vector3 getLocation()
    {
        return Location;
    }
    public int getDoors()
    {
        return Doors;
    }

   // CHECK DOORS

   public bool CheckDoors(int doorCheck)
    {
        if (doorCheck == 1)
        {
            return !((Doors % 2) == 0);
        }
        else if (doorCheck == 2)
        {
            return !((Doors % 3) == 0);
        }
        else if (doorCheck == 3)
        {
            return !((Doors % 5) == 0);
        }
        else if (doorCheck == 4)
        {
            return !((Doors % 7) == 0);
        }

        return true;
    }
}
