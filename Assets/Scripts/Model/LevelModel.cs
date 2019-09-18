using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModel : MonoBehaviour
{
    public int _enemyCount;
    public Transform[] _enemySpawnPositions;
    public GameObject[] _enemyList;
    
    public Transform[] getEnemySpawnPositions() => _enemySpawnPositions;  
    public int getEnemyCount() => _enemyCount;
    public GameObject[] getEnemyList() => _enemyList;
}
