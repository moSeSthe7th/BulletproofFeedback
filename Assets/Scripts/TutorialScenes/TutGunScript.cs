using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutGunScript : MonoBehaviour
{
    private float offset;

    private float z; // ball plus shooter pos

    void Start()
    {
        offset = transform.position.z - TutPlayer.instance.transform.position.z;
    }

    void LateUpdate()
    {
        z = TutPlayer.instance.transform.position.z + offset;
        this.transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    public GameObject bulletOnTheWay(Vector3 bulletPos, Vector3 bulletRot, float speed)
    {
        GameObject pooledBullet = TutObjectPooler.SharedInstance.GetPooledObject("Bullet", GameObject.FindWithTag("ALLPLATFORM").transform);
        if (pooledBullet != null)
        {
            pooledBullet.transform.localPosition = bulletPos;
            pooledBullet.transform.localRotation = Quaternion.Euler(bulletRot);
            pooledBullet.GetComponent<TutBulletScript>().bulletSpeed = new Vector3(0f, 0f, speed);
            pooledBullet.SetActive(true);
        }
        return pooledBullet;
    }
}

/* void Update()
 {
     if (passedBlock < TutorialManager.instance.blockNumber + 1 && TutorialManager.instance.blocks[passedBlock].transform.position.z - 40f <= this.transform.position.z && !TutorialManager.instance.blocks[passedBlock].activeInHierarchy)//&&
     {
         TutorialManager.instance.blocks[passedBlock].SetActive(true);
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
      while (TutorialManager.instance.isGameOn)//(TutorialManager.instance.isGameOn)
      {
          if (!TutorialManager.instance.isReverseLevel && blockNumb <= TutorialManager.instance.blockNumber - 1 && this.gameObject.transform.position.z > TutorialManager.instance.blocks[0].gameObject.transform.position.z) //&& TutPlayer.instance.mode ==(int) TutPlayer.Mode.normal) //this.gameObject.transform.position.z >= TutorialManager.instance.blocks[0].gameObject.transform.position.z + TutorialManager.instance.BlockDistUpdate) //this.gameObject.transform.position.z >= TutorialManager.instance.blocks[0].gameObject.transform.position.z + TutorialManager.instance.BlockDistUpdate)
          {
              bulletPos = RandomPos.RandomPosition(bulletPos, 2);
              float barrelPos = bulletPositionSelector[bulletPos];
              float rand = Random.Range(minRand, maxRand);

              uiScript.materialChangetoRed(barrelSelector(barrelPos));
              yield return new WaitUntil(() => this.gameObject.transform.position.z >= TutorialManager.instance.blocks[blockNumb].gameObject.transform.position.z + TutorialManager.instance.BlockDistUpdate + rand);// shootInterval); //+ TutorialManager.instance.BlockDistUpdate);
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
     tempTimer += TutPlayer.instance.BPModeTime;
 }*/

/* private void SendPowerUp(Vector3 powerUpPos)
  {
      Rigidbody powerUpClone = (Rigidbody)Instantiate(powerUp, powerUpPos, transform.rotation);
  }*/


/*
IEnumerator sendBullet()
{
    Debug.LogError("Entered shoot mode");
    while (TutorialManager.instance.isGameOn)
    {
        float r = Random.Range(-0.5f, 0.5f);
        if (TutorialManager.instance.isLevel && !TutorialManager.instance.isReverseLevel)
        {
            //Debug.LogWarning("sending bullet");
            bulletPos = RandomPos.RandomPosition(bulletPos, 2);
            float barrelPos = bulletPositionSelector[bulletPos];
            Vector3 bulletPosition = new Vector3(barrelPos, -3f, transform.position.z);
            if (!shootOrPower)
            {
                if (TutPlayer.instance.mode == (int)TutPlayer.Mode.normal)
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
      while (TutorialManager.instance.isGameOn)
      {
          yield return new WaitForSeconds(shootTime);
          float r = Random.Range(-0.5f, 0.5f);
          if (TutorialManager.instance.isLevel && !TutorialManager.instance.isReverseLevel)
          {
              //Debug.LogWarning("sending bullet");
              bulletPos = RandomPos.RandomPosition(bulletPos, 2);
              float barrelPos = bulletPositionSelector[bulletPos];
              Vector3 bulletPosition = new Vector3(barrelPos, -3f, transform.position.z);
               if (!shootOrPower)
                  {
                      if (TutPlayer.instance.mode == (int)TutPlayer.Mode.normal)
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




