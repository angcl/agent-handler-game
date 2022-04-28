using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleArrived : ICondition
{
    private ResourceManager resourceManager;
    private ParkingLotClickable parkingLotClickable;

    public bool Randomize()
    {
        if (resourceManager == null)
            resourceManager = GameObject.FindObjectOfType<ResourceManager>();

        List<ParkingLotClickable> availableParkingLots = new List<ParkingLotClickable>();
        foreach (ParkingLotClickable parkingLot in resourceManager.parkingLots)
        {
            if (!parkingLot.isWaitingForArrival && !parkingLot.isArrived && !parkingLot.HasTask())
                availableParkingLots.Add(parkingLot);
        }
        
        if (availableParkingLots.Count == 0) {
            return false;
        }

        parkingLotClickable = availableParkingLots[Random.Range(0, availableParkingLots.Count)];
        return true;
    }

    public bool Check()
    {
        return parkingLotClickable.isArrived;
    }

    public float TimeToSolve() 
    {
        return parkingLotClickable.timeForVehicleToArrive * 1.6f;
    }

    public float ReputationLoss()
    {
        return 0.25f;
    }

    public ICondition Clone() 
    {
        return (ICondition) this.MemberwiseClone();
    }
    
    public GameObject GetObjectToFocus() 
    {
        return parkingLotClickable.gameObject;
    }

    public ETaskIcon GetTaskIcon()
    {
        return ETaskIcon.CALL;
    }
}
