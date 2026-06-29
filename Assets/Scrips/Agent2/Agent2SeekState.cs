using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2SeekState : IAgent2State
{
    public void UpdateState(SteeringAgent2 agent, float deltaTime)
    {
        agent.transform.Translate(Vector3.back * agent.data.speed * deltaTime, Space.World);

        Vector3 direction = SteeringBehaviours.Seek(
            agent.transform,
            agent.PlayerTransform.position
        );

        agent.transform.position += direction * agent.data.speed * deltaTime;

        if (direction != Vector3.zero)
            agent.transform.forward = direction;

        
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
            > agent.data.detectionRange)
        {
            agent.ChangeState(new Agent2WanderState(agent.transform.forward));
        }
    }
}