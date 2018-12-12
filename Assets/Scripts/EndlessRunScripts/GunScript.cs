using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{  
    private int bulletPos;

    private float offset;

    private float z; // ball plus shooter pos

    public float shootTime;

    public int blockNumb;

    public int totBulletSended;

    //public float shootInterval;

    //public float minRand, maxRand;

    public int passedBlock;

    private Coroutine shootLoop;

    public bool isShooting;

    private float bulletSpeedOnTop;
    private float bulletSpeedOnMiddle;


    void Start()
    {
        isShooting = false;
        blockNumb = 0;
        //shootInterval = 1f;
       // tempTimer = 25f;
        //bulletScript = FindObjectOfType (typeof(BulletScript)) as BulletScript;
        shootTime = .5f;
        offset = transform.position.z - Player.instance.transform.position.z;
        bulletPos = 3;
       // randomForPower = Random.Range(-3f, 3f);
        //minRand = 0f;
        //maxRand = 0f;
        passedBlock = 30;

        bulletSpeedOnTop = 1f;//GameConst.instance.BlockDistUpdate / (offset - GameConst.instance.BlockDistUpdate);
        bulletSpeedOnMiddle = 2/5f;
    }

    void LateUpdate()
    {
        z = Player.instance.transform.position.z + offset;
        this.transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    public void StartGun()
    {
        isShooting = true;
        shootLoop = StartCoroutine(ShooterLoop());
    }

    public void StopGun()
    {
        isShooting = false;
        StopCoroutine(shootLoop);
        //uiScript.materialChangetoWhite(barrelSelector(-10));
        //uiScript.materialChangetoWhite(barrelSelector(0));
        //uiScript.materialChangetoWhite(barrelSelector(10));
    }

    private IEnumerator ShooterLoop()
    {
        //bullet hızı ayarla sadece !!!!!!!!!!!!!!!
        //********************
        while (GameConst.instance.isGameOn)//(GameConst.instance.isGameOn)
        {
            if (blockNumb <= GameConst.instance.blockNumber && this.gameObject.transform.position.z > GameConst.instance.blocks[0].gameObject.transform.position.z && this.gameObject.transform.position.z < GameConst.instance.LastPosOfArray + GameConst.instance.BlockDistUpdate)
            {
                bulletPos = RandomPos.RandomPosition(bulletPos, 2);
                //float barrelPos = bulletPositionSelector[bulletPos];
                //float rand = Random.Range(minRand, maxRand);
                //uiScript.materialChangetoRed(barrelSelector(barrelPos));
                yield return new WaitUntil(() => this.gameObject.transform.position.z >= GameConst.instance.blocks[blockNumb].gameObject.transform.position.z + GameConst.instance.BlockDistUpdate);// shootInterval); //+ GameConst.instance.BlockDistUpdate);
                //Debug.Log("Sıktım benim konum : " + this.transform.position.z + "Baktıgım blok konumu " + GameConst.instance.blocks[blockNumb].gameObject.transform.position.z );

                float spd = (GameConst.Level == 1) ? (Random.Range(0, 3) > 1) ? bulletSpeedOnTop : bulletSpeedOnMiddle :(Random.Range(0,2) == 0) ? bulletSpeedOnMiddle : bulletSpeedOnTop; 
                Vector3 bulletPosition = new Vector3(PlatformTurnScript.instance.CoinAndBulletPos[bulletPos].x, PlatformTurnScript.instance.CoinAndBulletPos[bulletPos].y, this.transform.position.z);

                if(blockNumb < GameConst.instance.blockNumber)
                {
                    bulletOnTheWay(bulletPosition, PlatformTurnScript.instance.BulletRot[bulletPos], (totBulletSended != 0) ? -1 * spd * Player.instance.speed.z : -1 * spd * PlayerAccelerator.GetPlayerNormalSpeed(Player.instance.initialSpeed));
                    totBulletSended += 1;
                    blockNumb += 1;
                }
                else if(blockNumb == GameConst.instance.blockNumber && Mathf.Approximately(spd,bulletSpeedOnTop))
                {
                    bulletOnTheWay(bulletPosition, PlatformTurnScript.instance.BulletRot[bulletPos],-1 * spd * Player.instance.speed.z);
                    totBulletSended += 1;
                    blockNumb += 1;
                }


                yield return new WaitForSecondsRealtime(0.5f);

                if(Player.instance.mode == (int)Player.Mode.bulletProof && Random.Range(0, Player.instance.strikeConstant + 2) >= 2 && blockNumb < GameConst.instance.blockNumber){
                    bulletPos = RandomPos.RandomPosition(bulletPos, 2);
                    float Bspd = (GameConst.Level == 1) ? (Random.Range(0, 3) > 1) ? bulletSpeedOnTop : bulletSpeedOnMiddle : (Random.Range(0, 2) == 0) ? bulletSpeedOnMiddle : bulletSpeedOnTop;
                    Vector3 BbulletPosition = new Vector3(PlatformTurnScript.instance.CoinAndBulletPos[bulletPos].x, PlatformTurnScript.instance.CoinAndBulletPos[bulletPos].y, this.transform.position.z);

                    bulletOnTheWay(BbulletPosition,PlatformTurnScript.instance.BulletRot[bulletPos], -1 * Bspd * Player.instance.speed.z );
                    totBulletSended += 1;
                }
                //uiScript.materialChangetoWhite(barrelSelector(barrelPos));
            }
            yield return null;
        }
        StopGun();
    }

    public void bulletOnTheWay(Vector3 bulletPos,Vector3 bulletRot,float speed)
    {
        GameObject pooledBullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet",GameObject.FindWithTag("ALLPLATFORM").transform);
        if (pooledBullet != null)
        {
            pooledBullet.transform.localPosition = bulletPos;
            pooledBullet.transform.localRotation = Quaternion.Euler(bulletRot);
            pooledBullet.GetComponent<BulletScript>().bulletSpeed = new Vector3(0f, 0f, speed);
            pooledBullet.SetActive(true);
        }
    }
}

/* void Update()
 {
     if (passedBlock < GameConst.instance.blockNumber + 1 && GameConst.instance.blocks[passedBlock].transform.position.z - 40f <= this.transform.position.z && !GameConst.instance.blocks[passedBlock].activeInHierarchy)//&&
     {
         GameConst.instance.blocks[passedBlock].SetActive(true);
         passedBlock++;
     }
 }*/

/* private GameObject barrelSelector(float pos)
   {
       if (Mathf.Approximately(pos,10f))
       {
           return GameObject.FindGameObjectWithTag("rightGunCylinder");
       }
       if (Mathf.Approximately(pos, 0f))
       {
           return GameObject.FindGameObjectWithTag("middleGunCylinder");
       }
       if (Mathf.Approximately(pos, -10f))
       {
           return GameObject.FindGameObjectWithTag("leftGunCylinder");
       }
       else
       {
           return null;
       }
   }*/

/*  private IEnumerator _sendBullet()
  {
      //Debug.LogWarning("Entered shoot mode");
      while (GameConst.instance.isGameOn)//(GameConst.instance.isGameOn)
      {
          if (!GameConst.instance.isReverseLevel && blockNumb <= GameConst.instance.blockNumber - 1 && this.gameObject.transform.position.z > GameConst.instance.blocks[0].gameObject.transform.position.z) //&& Player.instance.mode ==(int) Player.Mode.normal) //this.gameObject.transform.position.z >= GameConst.instance.blocks[0].gameObject.transform.position.z + GameConst.instance.BlockDistUpdate) //this.gameObject.transform.position.z >= GameConst.instance.blocks[0].gameObject.transform.position.z + GameConst.instance.BlockDistUpdate)
          {
              bulletPos = RandomPos.RandomPosition(bulletPos, 2);
              float barrelPos = bulletPositionSelector[bulletPos];
              float rand = Random.Range(minRand, maxRand);

              uiScript.materialChangetoRed(barrelSelector(barrelPos));
              yield return new WaitUntil(() => this.gameObject.transform.position.z >= GameConst.instance.blocks[blockNumb].gameObject.transform.position.z + GameConst.instance.BlockDistUpdate + rand);// shootInterval); //+ GameConst.instance.BlockDistUpdate);
              totBulletSended += 1;
              Vector3 bulletPosition = new Vector3(PlatformTurnScript.instance.CoinAndBulletPos[bulletPos].x, PlatformTurnScript.instance.CoinAndBulletPos[bulletPos].y, transform.position.z + 5f);
              bulletOnTheWay(bulletPosition,20f);
              //}
              yield return new WaitForSecondsRealtime(0.5f);
              uiScript.materialChangetoWhite(barrelSelector(barrelPos));
              //Debug.LogWarning(totBulletSended % shootInterval);

              if (totBulletSended % shootInterval <= 1)
              {
                  blockNumb += 1;
              }
          }
          yield return null;
      }
  }*/

/* public static void TempIncreaser()
 {
     tempTimer += Player.instance.BPModeTime;
 }*/

/* private void SendPowerUp(Vector3 powerUpPos)
  {
      Rigidbody powerUpClone = (Rigidbody)Instantiate(powerUp, powerUpPos, transform.rotation);
  }*/


/*
IEnumerator sendBullet()
{
    Debug.LogError("Entered shoot mode");
    while (GameConst.instance.isGameOn)
    {
        float r = Random.Range(-0.5f, 0.5f);
        if (GameConst.instance.isLevel && !GameConst.instance.isReverseLevel)
        {
            //Debug.LogWarning("sending bullet");
            bulletPos = RandomPos.RandomPosition(bulletPos, 2);
            float barrelPos = bulletPositionSelector[bulletPos];
            Vector3 bulletPosition = new Vector3(barrelPos, -3f, transform.position.z);
            if (!shootOrPower)
            {
                if (Player.instance.mode == (int)Player.Mode.normal)
                {
                    uiScript.materialChangetoBlue(barrelSelector(barrelPos));
                    yield return new WaitForSeconds(0.5f);
                    SendPowerUp(bulletPosition);
                }
                shootOrPower = true; // set to shoot again
            }
            else if (shootOrPower)
            {
                uiScript.materialChangetoRed(barrelSelector(barrelPos));
                yield return new WaitForSeconds(0.5f);
                bulletOnTheWay(bulletPosition);
            }
            yield return new WaitForSeconds(shootTime);
            uiScript.materialChangetoWhite(barrelSelector(barrelPos));
        }
        yield return new WaitForSeconds(r);
    }
    Debug.LogError("Exiting shoot mode");
    StopCoroutine(sendBullet());
}

*/


/* IEnumerator sendBullet()
  {
      Debug.LogError("Entered shoot mode");
      while (GameConst.instance.isGameOn)
      {
          yield return new WaitForSeconds(shootTime);
          float r = Random.Range(-0.5f, 0.5f);
          if (GameConst.instance.isLevel && !GameConst.instance.isReverseLevel)
          {
              //Debug.LogWarning("sending bullet");
              bulletPos = RandomPos.RandomPosition(bulletPos, 2);
              float barrelPos = bulletPositionSelector[bulletPos];
              Vector3 bulletPosition = new Vector3(barrelPos, -3f, transform.position.z);
               if (!shootOrPower)
                  {
                      if (Player.instance.mode == (int)Player.Mode.normal)
                      {
                          uiScript.materialChangetoBlue(barrelSelector(barrelPos));
                          yield return new WaitForSeconds(0.5f);
                          totBulletSended += 1;
                          SendPowerUp(bulletPosition);
                      }

                      shootOrPower = true; // set to shoot again
                  }
              //else if (shootOrPower)
              //{
              uiScript.materialChangetoRed(barrelSelector(barrelPos));
              yield return new WaitForSeconds(0.5f);
              totBulletSended += 1;
              bulletOnTheWay(bulletPosition);
              //}
              yield return new WaitForSeconds(shootTime);
              uiScript.materialChangetoWhite(barrelSelector(barrelPos));
          }
          yield return new WaitForSeconds(r);
      }
      Debug.LogError("Exiting shoot mode");
      StopCoroutine(sendBullet());
  }*/




