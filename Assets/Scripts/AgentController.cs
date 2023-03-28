using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private readonly static string NAV_TAG = "Navigation";

    public event EventHandler OnTargetReached;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject target;

    public GameObject GetTarget() { return target; }


    public void AssignTarget(GameObject newTarget)
    {
        this.target = newTarget;
        agent.destination = target.transform.position;
    }

    void Start()
    {
        if (agent != null && target != null)
        {
            agent.destination = target.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(NAV_TAG) && other.gameObject.Equals(target))
        {
            OnTargetReached(this, EventArgs.Empty);
        }
        
    }
}
