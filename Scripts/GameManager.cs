using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool Game_Over = true;
    [SerializeField]
    private GameObject _playerPrefab;
    private UIManager _uIManager;
    private SpawnManager _spawnManager;
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    void Update()
    {
        if(Game_Over==true)
        {

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                Game_Over = false;
                Instantiate(_playerPrefab,Vector3.zero,Quaternion.identity);
                _uIManager.HideTitleScreen();
                

            }
        }
    }

}
