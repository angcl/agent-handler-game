using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyDeactivated : ICondition
{
    private BuildingClickable buildingClickable;

    public bool Randomize()
    {
        BuildingClickable[] allBuildings = GameObject.FindObjectsOfType<BuildingClickable>();

        List<BuildingClickable> availableBuildings = new List<BuildingClickable>();
        foreach (BuildingClickable b in allBuildings)
        {
            if (!b.isHacked && !b.HasTask())
                availableBuildings.Add(b);
        }
        
        if(availableBuildings.Count == 0){
            return false;
        }
        
        buildingClickable = availableBuildings[Random.Range(0, availableBuildings.Count)];
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