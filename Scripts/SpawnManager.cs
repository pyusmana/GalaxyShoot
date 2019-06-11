using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefeb;
    [SerializeField]
    private GameObject[] powerUps;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void StartSpawnRoutine()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PoweupSpawn());
    }
    IEnumerator EnemySpawn()
    {
        while(_gameManager.Game_Over==false)
        {
            Instantiate(_enemyPrefeb,new Vector3(Random.Range(-8f,8f),7f,0),Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }
    IEnumerator PoweupSpawn()
    {
       
            while(_gameManager.Game_Over==false)
            {
            int x = Random.Range(0, 3);
            Instantiate(powerUps[x],new Vector3(Random.Range(-8f,8f),7f) , Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
            }
    }
}
