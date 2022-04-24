using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyDeactivated : ICondition
{
    private BuildingClickable buildingClickable;

    public bool Randomize()
    {
        BuildingClickable[] availableBuildings = GameObject.FindObjectsOfType<BuildingClickable>();
        availableBuildings = availableBuildings.AsQueryable().Where(b => b.isEnergized && !b.HasTask()).ToArray();
        if(availableBuildings.Length == 0){
            return false;
        }
        buildingClickable = availableBuildings[Random.Range(0, availableBuildings.Length)];
        return true;
    }

    public bool Check()
    {
        return !buildingClickable.isEnergized;
    }

    public float TimeToSolve() {
        return 10.0f;
    }

    public float ReputationLoss()
    {
        return 0.15f;
    }

    public ICondition Clone() {
        return (ICondition) this.MemberwiseClone();
    }
    
    public GameObject GetObjectToFocus() {
        return buildingClickable.gameObject;
    }

    public ETaskIcon GetTaskIcon() {
        return ETaskIcon.ENERGY;
    }    
}