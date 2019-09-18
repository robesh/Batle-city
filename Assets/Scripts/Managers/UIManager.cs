using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public Text _tEnemyCount;
    
    public Text _tTotalEnemyCount;
    public Text _statusText;
    public Text _roundNumber;
    // Start is called before the first frame update
    void Start()
    {
        //_tEnemyCount.text = _iEnemyCount.ToString() + " enemy left";
        //_tTotalEnemyCount.text = _iTotalEnemyCount.ToString() + " total enemy";
    }

    public void UpdateCounters (int enemyCount, int totalCount)
    {
        _tEnemyCount.text = enemyCount.ToString() + " enemy left";
        _tTotalEnemyCount.text = totalCount.ToString() + " total enemy";
        //if (_iEnemyCount == 0)
    }

    public void UpdateLevelStatus (string status)
    {
        _statusText.text = status;
    }

    public void UpdateLevel(int level)
    {
        _roundNumber.text = "Round number: " + level.ToString();
    }
}
