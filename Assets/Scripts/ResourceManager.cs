using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public BuildingClickable[] buildings;
    public CameraClickable[] cameras;
    public ParkingLotClickable[] parkingLots;

    void Awake()
    {
        buildings = FindObjectsOfType<BuildingClickable>();
        cameras = FindObjectsOfType<CameraClickable>();
        parkingLots = FindObjectsOfType<ParkingLotClickable>();
    }
}
