using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutBulletScript : MonoBehaviour
{


    //public GameObject player;
    //private Rigidbody bullet;
    public Vector3 bulletSpeed;

    private void FixedUpdate()
    {
        this.transform.Translate(bulletSpeed * Time.deltaTime, Space.Self);
    }

    void Update()
    {
        bulletSpeed =(-1 * TutPlayer.instance.speed) / 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (Player.instance.mode == (int)Player.Mode.normal)
            {
                //collision.gameObject.SetActive(false);
                if (TutorialManager.instance.isGameOn)
                {
                    TutorialManager.instance.isGameOn = false;
                    Player.instance.speed = Vector3.zero;
                }

                this.gameObject.SetActive(false);
            }
            else if (Player.instance.mode == (int)Player.Mode.bulletProof)
            {
                this.gameObject.SetActive(false);
                Player.instance.BulletImpactOutput(3f, 1.5f);
                Player.instance.bulletHits += 1;
            }
        }
    }


}
