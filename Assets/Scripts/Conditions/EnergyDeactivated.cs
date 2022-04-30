using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyDeactivated : ICondition
{
    private ResourceManager resourceManager;
    private BuildingClickable buildingClickable;

    public bool Randomize()
    {
        if (resourceManager == null)
            resourceManager = GameObject.FindObjectOfType<ResourceManager>();

        List<BuildingClickable> availableBuildings = new List<BuildingClickable>();
        foreach (BuildingClickable b in resourceManager.buildings)
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
        return 8f;
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