using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject _level;
    public float _fStartDelay = 3f;
    public float _fEndDelay = 3f;
    public float _enemySpawnTime = 4;

    //Counters
    private static int _iTotalEnemyCount;
    private static int _iEnemyCount;

    private static UIManager _uIManager;
    private LevelModel _levelModel;

    private int _roundNumber;
    private WaitForSeconds _waitStartWait;
    private WaitForSeconds _waitEndWait;
    private static bool _isWining;
    private static bool _isLoosing;
    private Transform[] _enemySpawnPositions;
    private float[] _enemySpawnCooldown;
    private GameObject[] _enemyList;

    // Start is called before the first frame update
    void Start()
    {
        _uIManager = _level.GetComponent<UIManager>();
        _levelModel = _level.GetComponent<LevelModel>();
        _enemySpawnPositions = _levelModel.getEnemySpawnPositions();
        _enemySpawnCooldown = new float[_levelModel.getEnemySpawnPositions().Length];
        _enemyList = _levelModel.getEnemyList();

        EnableTankControl(false);

        _waitStartWait = new WaitForSeconds(_fStartDelay);
        _waitEndWait = new WaitForSeconds(_fEndDelay);

        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (_isLoosing || _isWining)
        {
            StopCoroutine("SpawnEnemy");
        }
    }

    // Update is called once per frame
    public void StartLevel()
    {
        _roundNumber++;
        switch (_roundNumber)
        {
            case 1: 
                //instansion level
                ResetLevel();
                break;
            default: GameOver(); break;
        }            
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (_isWining)
        {
            SceneManager.LoadScene("Shop");
            //ResetLevel();
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        //_roundNumber++;
        StartLevel();
        //ResetLevel();
        //ResetAllTanks();
        //DisableTankControl();

        //m_CameraControl.SetStartPositionAndSize();
        
        //_uIManager.UpdateLevel(_roundNumber);
        //m_MessageText.text = "ROUND " + m_RoundNumber;

        yield return _waitStartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableTankControl(true);
        StartCoroutine("SpawnEnemy");
        //m_MessageText.text = "";
        _uIManager.UpdateLevelStatus("");
        while (!(_isLoosing || _isWining))
        {
            yield return null;
        }

    }


    private IEnumerator RoundEnding()
    {
        EnableTankControl(false);
        if (_isLoosing) GameOver();

        yield return _waitEndWait;
    }

    private void ResetLevel()
    {
        //init level

        //_uIManager = _level.GetComponent<UIManager>();
        //_enemySpawnPositions = levelModel.getEnemySpawnPositions();
        _isLoosing = false;
        _isWining = false;
        _iEnemyCount = _levelModel.getEnemyCount();
        _uIManager.UpdateLevel(_roundNumber);
        _uIManager.UpdateCounters(_iEnemyCount, _iTotalEnemyCount);
        _uIManager.UpdateLevelStatus("LEVEL " + _roundNumber + " START!");
    }

    private void EnableTankControl(bool permision)
    {
        TankManager.EnableMovingAll(permision);
        TankManager.EnableShootingAll(permision);
    }

    public static void RoundResult (bool result)
    {
        if (result) _isWining = true;
        else _isLoosing = true;
        //StopCoroutine("SpawnEnemy");
    }

    public static void CountEnemy()
    {
        _iEnemyCount--;
        _iTotalEnemyCount++;
        _uIManager.UpdateCounters(_iEnemyCount, _iTotalEnemyCount);
        if (_iEnemyCount == 0) WinLevel();
    }

    IEnumerator SpawnEnemy()
    {
        int i = 0, j = 0, enemyCount = _iEnemyCount;

        for (i = 0; i < enemyCount; i++)
        {
            GameObject hazard = _enemyList[Random.Range(0, _enemyList.Length)];
            //Vector3 spawnPosition =
            //    new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Vector3 spawnPosition;
            for (; ;)
            {
                j = Random.Range(0, _enemySpawnPositions.Length);
                if ((_enemySpawnCooldown[j] += Time.deltaTime) > _enemySpawnTime)
                {
                    _enemySpawnCooldown[j] = 0;
                    spawnPosition = _enemySpawnPositions[j].position;
                    break;
                }
            }
            Quaternion spqwnRotation = Quaternion.identity;
            //Quaternion spqwnRotation = Quaternion.Euler(0, 180, 0);
            Instantiate(hazard, spawnPosition, spqwnRotation);
            yield return new WaitForSeconds(_enemySpawnTime);
        }
    }

    private static void WinLevel()
    {
        _uIManager.UpdateLevelStatus("You win!");
    }

    void GameOver()
    {
        _isLoosing = _isWining = false;
        _uIManager.UpdateLevelStatus("Game over");
    }
}
