using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot=false;
    public bool canSpeedBoost = false;
    public bool canShield = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
   
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _fireRate=0.25f;
    private float _nextFire=0.0f;
    [SerializeField]
    private float _v = 5.0f;
    [SerializeField]
    private GameObject _playerExplosionPrefab;
    [SerializeField]
    private GameObject[] _engines;
    [SerializeField]
    private GameObject _shieldsGameObject;
    private UIManager _uIManager;
    private GameManager _gameManager;
    public SpawnManager _spawnManager;
    private AudioSource _audioSource;
    private int hitCount;

    public int lives = 3;
   
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uIManager!=null)
        {
            _uIManager.UpdateLives(lives);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager)
        _spawnManager.StartSpawnRoutine();
        _audioSource = GetComponent<AudioSource>();
        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }        

    }
    private void Shoot()
    {
        if(Time.time > _nextFire && canTripleShot == true)
        {
            _audioSource.Play();
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            _nextFire = Time.time + _fireRate;
        }
        else if (Time.time > _nextFire)
         {
            _audioSource.Play();
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            _nextFire = Time.time + _fireRate;
         }
        
    }
    private void Movement()
    {
        if (canSpeedBoost == true)
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * _v*2);

            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * _v*2);
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * _v);


            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * _v);
        }
        if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }

        else if (transform.position.y < -4.2)
        {
            transform.position = new Vector3(transform.position.x, (-4.2f), 0);
        }
        else if (transform.position.y > 1)
        {
            transform.position = new Vector3(transform.position.x, 1, 0);
        }
    }
    public void Destroy()
    {
        lives--;

        hitCount++;
        if (canShield==true)
        {
            lives += 1;
            canShield = false;
            _shieldsGameObject.SetActive(false);
            hitCount--;
        }
        
        

        else if (lives<1)
        {
            Instantiate(_playerExplosionPrefab, transform.position,Quaternion.identity);
            _gameManager.Game_Over = true;
            _uIManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }


        if (hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            _engines[1].SetActive(true);
        }

        _uIManager.UpdateLives(lives);
    }
    public void TripleShotPowerOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownTime());
    }
    public IEnumerator TripleShotPowerDownTime()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
        
     
    }
    public void SpeedBoostPowerOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownTime());
    }
    public IEnumerator SpeedBoostPowerDownTime()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    
    }
    public void ShieldPowerOn()
    {
        canShield = true;
        _shieldsGameObject.SetActive(true);

    }
   
}
        
