using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{


    //public GameObject player;
    //private Rigidbody bullet;
    public Vector3 bulletSpeed;
    private UIScript uIScript;

    private void Start()
    {
        uIScript = FindObjectOfType(typeof(UIScript)) as UIScript;
    }

    private void FixedUpdate()
    {
       this.transform.Translate(bulletSpeed * Time.deltaTime, Space.Self);
    }

    void Update()
    {
        if (GameConst.instance.isGameOn)
        {
            if (transform.position.z < Player.instance.transform.position.z - 20f)
            {
                // trail.enabled = false;
                // this.gameObject.GetComponent<TrailRenderer>().enabled = false;
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            //bulletSpeed = new Vector3(0,0,0);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (Player.instance.mode == (int)Player.Mode.normal)
            {
                //collision.gameObject.SetActive(false);
                if(GameConst.instance.isGameOn)
                {
                    GameConst.instance.isGameOn = false;
                    Player.instance.speed = Vector3.zero; 
                    uIScript.StartCoroutine(uIScript.AdvertiseReward());
                    Player.instance.BulletImpactOutput(5f, .75f);
                }
                this.gameObject.SetActive(false);
            }
            else if (Player.instance.mode == (int)Player.Mode.bulletProof)
            {
				this.gameObject.SetActive (false);
                Player.instance.BulletImpactOutput(3f, 1.5f);
				Player.instance.bulletHits += 1;
            }
        }
    }


}
