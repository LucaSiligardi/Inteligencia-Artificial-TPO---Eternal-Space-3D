using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2WanderState : IAgent2State
{
    private Vector3 _wanderDirection;

    public Agent2WanderState(Vector3 initialDirection)
    {
        _wanderDirection = initialDirection == Vector3.zero
            ? Vector3.forward
            : initialDirection;
    }

    public void UpdateState(SteeringAgent2 agent, float deltaTime)
    {
        agent.transform.Translate(Vector3.back * agent.data.speed * deltaTime, Space.World);

        _wanderDirection = SteeringBehaviours.Wander(
            _wanderDirection,
            agent.data.wanderAngleChange * deltaTime
        );

        agent.transform.position += _wanderDirection * agent.data.speed * deltaTime;

        if (_wanderDirection != Vector3.zero)
            agent.transform.forward = _wanderDirection;

      
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (bullet.activeInHierarchy &&
                Vector3.Distance(agent.transform.position, bullet.transform.position)
                <= agent.data.fleeRadius)
            {
                agent.ChangeState(new Agent2FleeState(agent.data.fleeDuration));
                return;
            }
        }

     
        if (Vector3.Distance(agent.transform.position, agent.PlayerTransform.position)
            <= agent.data.detectionRange)
        {
            agent.ChangeState(new Agent2SeekState());
        }
    }
}