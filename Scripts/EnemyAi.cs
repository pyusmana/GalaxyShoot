using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
       
    [SerializeField]
    private float _v = 2.0f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    private UIManager _uIManager;
    [SerializeField]
    private AudioClip _clip;
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();


    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    private void Movement()
    {           
            transform.Translate(Vector3.down  * Time.deltaTime * _v);

           
            if(transform.position.y<-7)
        {
            transform.position = new Vector3(Random.Range(-8, 8), 7,0);
        }
     
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy Colided with: " + other.name);
         if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            
            if(_uIManager!=null)
            _uIManager.UpdateScore();
        }
        else if (other.tag=="Player")        
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                p.Destroy();
                
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            
            if (_uIManager != null)
                _uIManager.UpdateScore();
        }
        
    }
}
