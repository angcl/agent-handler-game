using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VehicleArrived : ICondition
{
    private ParkingLotClickable parkingLotClickable;

    public bool Randomize()
    {
        ParkingLotClickable[] availableParkingLots = GameObject.FindObjectsOfType<ParkingLotClickable>();

        availableParkingLots = availableParkingLots.AsQueryable().Where(b => !b.isWaitingForArrival && !b.isArrived && !b.HasTask()).ToArray();
        if(availableParkingLots.Length == 0){
            return false;
        }

        parkingLotClickable = availableParkingLots[Random.Range(0, availableParkingLots.Length)];
        return true;
    }

    public bool Check()
    {
        return parkingLotClickable.isArrived;
    }

    public float TimeToSolve() 
    {
        return parkingLotClickable.timeForVehicleToArrive * 1.5f;
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
