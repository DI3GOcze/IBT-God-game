using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGetFood : GActionBase
{
    List<System.Type> SupportedGoals = new List<System.Type>{typeof(GoalGetFood)};
    GetFoodManager Manager;

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override void OnActivated(GGoalBase _linkedGoal)
    {
        base.OnActivated(_linkedGoal);
        Manager = gameObject.AddComponent<GetFoodManager>();
        Manager.OnActivated();
    }

    public override void OnTick()
    {
        base.OnTick();
        Manager.OnTick();
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        Manager.OnDeactivated();
        Destroy(Manager);
    }

}
