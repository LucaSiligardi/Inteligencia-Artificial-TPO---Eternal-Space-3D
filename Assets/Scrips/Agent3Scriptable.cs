using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Agent3Data", menuName = "Enemies/Agent3Data")]
public class Agent3Scriptable : ScriptableObject
{
    public float speed = 3f;
    public float arriveSlowRadius = 2f;
    public float waypointReachedDistance = 1f;
    public float pathUpdateRate = 1f;
    public int pointsOnDeath = 100;
}