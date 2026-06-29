using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgent2State
{
    void UpdateState(SteeringAgent2 agent, float deltaTime);
}
