using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private float _v = 3.0f;
    [SerializeField]
    private int powerupId;
    [SerializeField]
    private AudioClip _clip;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _v * Time.deltaTime);
       if(transform.position.y<-7)
        {
            Destroy(this.gameObject);
        }
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                if (powerupId == 0)
                {
                    Debug.Log("Triple Shot Powerup Collided with :" + other.name);
                    p.TripleShotPowerOn();
                }
                else if (powerupId==1)
                {
                    Debug.Log("Speed Boost Powerup Collided with :" + other.name);
                    p.SpeedBoostPowerOn();
                }
                else if(powerupId==2)
                {
                    Debug.Log("Shield Powerup Collided with:" + other.name);
                    p.ShieldPowerOn();
                }
            }
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
