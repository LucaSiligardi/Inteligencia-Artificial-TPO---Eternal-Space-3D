using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2FleeState : IAgent2State
{
    private float _timer;
    private float _fleeDuration;

    public Agent2FleeState(float fleeDuration)
    {
        _fleeDuration = fleeDuration;
        _timer = 0f;
    }

    public void UpdateState(SteeringAgent2 agent, float deltaTime)
    {
  
        agent.transform.Translate(Vector3.back * agent.data.speed * deltaTime, Space.World);

        // Buscar bala 
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        Vector3 fleeTarget = Vector3.zero;
        float closestDist = float.MaxValue;
        bool foundBullet = false;

        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy) continue;
            float dist = Vector3.Distance(agent.transform.position, bullet.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                fleeTarget = bullet.transform.position;
                foundBullet = true;
            }
        }

        if (foundBullet)
        {
           
            Vector3 fleeDir = SteeringBehaviours.Flee(agent.transform, fleeTarget);

           
            Vector3 movement = new Vector3(fleeDir.x, fleeDir.y, 0f)
                * agent.data.speed * 2f * deltaTime;
            agent.transform.position += movement;
        }

       
        _timer += deltaTime;
        if (_timer >= _fleeDuration)
        {
            agent.ChangeState(new Agent2WanderState(agent.transform.forward));
        }
    }
}
