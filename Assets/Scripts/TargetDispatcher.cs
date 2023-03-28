using System.Collections.Generic;
using UnityEngine;

public class TargetDispatcher : MonoBehaviour
{
    [SerializeField] private GameObject targetParent;
    private List<GameObject> targetList;
    [SerializeField] private List<AgentController> agents;

    void Start()
    {   
        
        targetList = new List<GameObject>();
        foreach(Transform child in targetParent.transform)
        {
            targetList.Add(child.gameObject);
        }

        foreach(AgentController agent in agents)
        {
            agent.OnTargetReached += Agent_OnTargetReached;
            GiveTarget(agent);
        }
    }

    private void Agent_OnTargetReached(object sender, System.EventArgs e)
    {
        GiveTarget((AgentController)sender);
    }

    private void GiveTarget(AgentController agent)
    {
        var nextTarget = GetRandomTarget();
        if (nextTarget != agent.GetTarget())
        {
            agent.AssignTarget(nextTarget);
        } else if (targetList.Count > 1)
        {
            GiveTarget(agent);
        } else
        {
            throw new System.Exception("Not enough targets");
        }
    }

    private GameObject GetRandomTarget()
    {
        return targetList[Random.Range(0, targetList.Count)];
    }

}
