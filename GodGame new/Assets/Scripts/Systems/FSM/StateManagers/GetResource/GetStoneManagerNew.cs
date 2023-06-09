using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetStoneManager : GetResourceManager
{   
    public override System.Type targetResourceSource 
    { 
        get { return typeof(StoneResource); } 
    }
    public override ResourceTypes targetResourceType 
    { 
        get { return ResourceTypes.STONE; } 
    }

    protected void Awake() {
        resourceProp = transform.Find("Resources")?.Find("Stone")?.gameObject;
        toolProp = transform.Find("Resources")?.Find("PickAxe").gameObject;
    }    

    public override DepletableResource[] GetValidResources() => World.Instance.GetFreeResource<StoneResource>().ToArray();

}
