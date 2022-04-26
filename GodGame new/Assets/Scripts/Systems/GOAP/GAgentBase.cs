using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAgentBase : MonoBehaviour
{
    protected NavMeshAgent Agent;
    public bool ReachedDestination = false;
    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        CalculateReachedDestination();
    }

    private void CalculateReachedDestination()
    {
        ReachedDestination = false;
        // Can be disabled by VR interaction
        if(Agent.enabled && Agent.isOnNavMesh)
        {
            // If agent isnt calculating path
            if (!Agent.pathPending && !ReachedDestination){
                // If the agent is close enough to the destination
                if (Agent.remainingDistance <= Agent.stoppingDistance){
                    // If the agent has a path or osnt moving
                    // Sometimes if agent is to close to destination it wouldnt create path,
                    // thats why we test his velocity
                    if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f){
                        ReachedDestination = true;
                    }
                }
            }
                       
            if (ReachedDestination)
            {
                Agent.updateRotation = false;
                Agent.isStopped = true;
            }
        }
    }

    public bool GoToDestination(Vector3 destination)
    {
        bool retVal = Agent.SetDestination(destination);
        CalculateReachedDestination();
        Agent.updateRotation = true;
        Agent.isStopped = false;
        return retVal;
    }

    public bool GoToDestination(GameObject destinationObject)
    {
        Collider collider = destinationObject.GetComponentInChildren<Collider>();
        if (collider == null)
            return GoToDestination(destinationObject.transform.position);

        // Closest point on destination object
        Vector3 closestPoint = collider.ClosestPointOnBounds(gameObject.transform.position);
        return GoToDestination(closestPoint);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public T GetClosestObject<T>(T[] objects) where T : MonoBehaviour
    {
        T closestObject = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = gameObject.transform.position;
        foreach (T o in objects)
        {
            if (o != null)
            {
                float dist = Vector3.Distance(o.transform.position, currentPos);
                if (dist < minDist)
                {
                    closestObject = o;
                    minDist = dist;
                }
            }
        }

        return closestObject;
    }

    public void StopMovement()
    {
        if (Agent.isOnNavMesh)
        {
           Agent.isStopped = true; 
        }
    }

    protected virtual void OnDestroy() {
        
    }
}
