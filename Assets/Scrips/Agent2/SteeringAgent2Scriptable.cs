using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Agent2Data", menuName = "Enemies/Agent2Data")]
public class SteeringAgent2Scriptable : ScriptableObject
{
    public float speed = 4f;
    public float detectionRange = 12f;
    public float wanderAngleChange = 45f;  
    public int pointsOnDeath = 75;
    public float fleeRadius = 5f;    
    public float fleeDuration = 2f;  
}