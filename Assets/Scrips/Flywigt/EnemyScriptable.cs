using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Flywight/EnemyData", order =1)]
public class EnemyScriptable : ScriptableObject
{
    public float speed = 5f;
    public int pointsOnDeath = 100;

}
