﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConst : MonoBehaviour
{

    public static GameConst instance;

    //public static bool isReady; Use if gamconst starts scene
    public bool isGameOn;
    public bool isLevel;
    public bool isReverseLevel;

    public static int Level;

    public int gatheredPower;
    public int totalPowerToBP;

    public bool secondPushed;

    public GameObject[] blocks;
    public List<GameObject> Edges;
    public List<GameObject> Hollows;
    public List<GameObject> LevelupHollows;

    public float hollowUpdateDist;

    public float İnitBlockDist;
    public float BlockDistUpdate; // change this to decrease or increase distance between blocks
    public int points;
    public float LastPosOfArray;

    public float gameTime;


    public float energyTime;

    //public bool createNewLevel;

    public bool isEnergyFinished;

    public int SameLineBlock; // In order to change block pos of x

    public GameObject lastHit; //to change back color back

    public int blockNumber;

    //private CameraScript cameraScript;
    //private GameObject block;
    private GunScript gunScript;
   // private GameObject gun;

    //  private BlockScript blockScript;

    private UIScript uIScript;
    //    private BulletScript bulletScript;

    public static int playedTime;

    public int shifterBlock; // set 3 for 2 blocks ahead, Set 2 for block ahead

    private bool isStarted;

    public bool playerLock;

   /* private Coroutine PlayerSwipeLeft;
    private Coroutine PlayerSwipeRight;

    private Coroutine BlockSwipeLeft;
    private Coroutine BlockSwipeRight;

    private Coroutine ChildBlockSwipeLeft;
    private Coroutine ChildBlockSwipeRight;*/

    public void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
        {
            Debug.Log("game created");
            instance = this;
        }
        else
        {
            Debug.LogError("New game destroyed");
            Destroy(this.gameObject);
        }
        Time.timeScale = 0f;
        isGameOn = false;

        if (PlayerPrefs.HasKey("PlayedTime"))
        {
            playedTime = PlayerPrefs.GetInt("PlayedTime");
        }
        else
        {
            playedTime = 0;
            PlayerPrefs.SetInt("PlayedTime", playedTime);
        }

    }

    public void Start()
    {
        isStarted = false;

        isLevel = false;

        isReverseLevel = false;

        isEnergyFinished = false;

        //block = GameObject.FindWithTag("block");

       // gun = GameObject.FindWithTag("Gun");

        //cameraScript = FindObjectOfType(typeof(CameraScript)) as CameraScript;

        gunScript = FindObjectOfType(typeof(GunScript)) as GunScript;

        //        bulletScript = FindObjectOfType(typeof(BulletScript)) as BulletScript;

        uIScript = FindObjectOfType(typeof(UIScript)) as UIScript;

        //    blockScript = FindObjectOfType(typeof(BlockScript)) as BlockScript;

        LastPosOfArray = 0f;

        blockNumber = 15; // aslında 16 tane oluyor ilk blok da olduğu için

        blocks = new GameObject[blockNumber + 1];
        //createNewLevel = false;

        Level = 1;

        gameTime = 0f;

        lastHit = null;

        İnitBlockDist = 100f;

        BlockDistUpdate = 90f; //Distance between blocks

        SameLineBlock = 0;

        points = 0;

        totalPowerToBP = 1;

        gatheredPower = 0;

        energyTime = 100f;

        shifterBlock = 3; // bi sonraki blogun kayması için 2 iki block sonrası için 3 yap

        secondPushed = false;

        playerLock = false;

        hollowUpdateDist = BlockDistUpdate * 2; // arada 3 blok için 3 le çarp etc. 

        StartCoroutine(LevelChangeState(Player.instance.speed.z + 150f,Player.instance.speed.z));
    }


    private IEnumerator LevelChangeState(float PlayerSpeedLimit, float PlayerSpeed)
    {
        if (!isStarted)
        {
            yield return new WaitUntil(() => blocks[0] != null);
            isStarted = true;
            isGameOn = true;
            uIScript.startGame();
            Time.timeScale = 1f;
        }
        else
        {
            yield return new WaitForSeconds(.3f);
        }
        playerLock = true;
        BlockChanger();
        HollowSetter(blocks[0].gameObject.transform.position.z - 2f);
        gunScript.passedBlock = 0;
        //Debug.Log("Top hızlanmaya baslıyor. Top hızı :" + Player.instance.speed.z);
        float speedRate = 5f;
        float waitTime = .01f;
        while(!PlayerAccelerator.PlayerFastEnough(Player.instance.speed.z,PlayerSpeedLimit))
        {
            Player.instance.speed.z += speedRate;
            yield return new WaitForSeconds(waitTime);
        }

        yield return new WaitUntil(() => Player.instance.transform.position.z >= GameObject.FindWithTag("LevelHollows").transform.GetChild(GameObject.FindWithTag("LevelHollows").transform.childCount - 12).transform.position.z);

        while(!PlayerAccelerator.PlayerNormalSpeed(Player.instance.speed.z,PlayerSpeed))
        {
            if (PlayerSpeed + 70f >= Player.instance.speed.z && !gunScript.isShooting)
            {
                gunScript.StartGun();
                gunScript.blockNumb = 0;
                gunScript.totBulletSended = 0;
                waitTime = .03f;
            } 
            Player.instance.speed.z -= speedRate;
            yield return new WaitForSeconds(waitTime);
        }
        Player.instance.bulletProofSpeed = PlayerAccelerator.GetPlayerNormalSpeed() + (Player.instance.modeSpeedIncrease) + ((Player.instance.strikeConstant -1) * 2f);
        int m = Player.instance.mode;
        Player.instance.speed.z = (m == (int)Player.Mode.normal) ? PlayerAccelerator.GetPlayerNormalSpeed() : Player.instance.bulletProofSpeed;
        //Player.instance.RayCalculator(0);

        yield return new WaitUntil(() => Player.instance.gameObject.transform.position.z >= blocks[0].gameObject.transform.position.z - 35f);
        playerLock = false;
        yield return new WaitUntil(() => Player.instance.gameObject.transform.position.z >= blocks[0].gameObject.transform.position.z - 15f); //Player.instance.gameObject.transform.position.z >= blocks[0].gameObject.transform.position.z - 10f);
        isLevel = true;
        GameObject.FindWithTag("LevelHollows").transform.position = new Vector3(0f, 0f, LastPosOfArray + 80f);
        CoinsScript.IncreaseAndActivateCoins(GameObject.FindWithTag("Coins"));
        AddLevelHollow(2);
        //createNewLevel = false;
        StopCoroutine(LevelChangeState(PlayerSpeedLimit,PlayerSpeed));
    }

    private void BlockChanger()
    {
        //Debug.Log("entered block creater");
        //İnitBlockDist += Player.instance.transform.position.z - blocks[blocks.Length - 1].gameObject.transform.position.z + 75f;
        İnitBlockDist = LevelHollowGetDistance.GetLastHollowPos(GameObject.FindWithTag("LevelHollows").transform) + 100f;
        int doubleActivatedBlock = 0;
        BlockCreater.beforeActivated = false;
        foreach (GameObject b in blocks)
        {
            BlockCreater.SetDistance(b);
            doubleActivatedBlock = BlockCreater.CreateBlock(b, doubleActivatedBlock);
        }
        //LastPosOfArray = blocks[blocks.Length - 1].transform.position.z;
    }
    private void HollowSetter(float firstBlockPos)
    {
        for (int i = 0; i < Hollows.Capacity; i++)
        {
            if (!Hollows[i].gameObject.activeInHierarchy)
            {
                Hollows[i].gameObject.SetActive(true);
            }
            Hollows[i].transform.position = new Vector3(Hollows[i].transform.position.x, Hollows[i].transform.position.y, firstBlockPos);
            //Hollows[i].transform.Rotate(new Vector3(0, 0, -60f), Space.Self);
            firstBlockPos += hollowUpdateDist;
        }
    }

  /*  public void ReverseLevel()
    {
        cameraScript.changeCameraAngle();
    }*/

    public void PointDecider(bool strike)
    {
        int p = 1;
        if (!strike)
        {
            p = 1;
        }
        else if (strike)
        {
            p = Player.instance.pointIndex;
        }
        points += p;
        uIScript.setPointText();
        StartCoroutine(uIScript.SetGainedPointText(p));
    }

    private void AddLevelHollow(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newCylinder = (GameObject)Instantiate(GameObject.FindWithTag("LevelHollows").transform.GetChild(GameObject.FindWithTag("LevelHollows").transform.childCount - 1).gameObject, GameObject.FindWithTag("LevelHollows").transform);
            LevelupHollows.Add(newCylinder);
            LevelHollowGetDistance.SetHollow(newCylinder, 10f);
        }
    }

    public void LevelChanger(float playerMAX)
    {
        gunScript.StopGun();
        Level = LevelDesigner.levelUp(Level);

        Player.instance.speed.z = LevelDesigner.speedUp(Player.instance.speed.z);
        Player.instance.normalSpeed = Player.instance.speed.z;
      
        StartCoroutine(LevelChangeState(playerMAX,Player.instance.speed.z));
    }


    private void Update()
    {
        if (isLevel)
        {
            gameTime += Time.deltaTime;
        }
    }

	public IEnumerator CountDown()
	{
		int timer = 3;
		uIScript.countDown.gameObject.SetActive(true);
		while (timer >= 1)
		{
			uIScript.countDown.text = timer.ToString();
			yield return new WaitForSecondsRealtime(1f);;
			timer--;
			yield return null;
		}
		uIScript.countDown.gameObject.SetActive(false);
		if (!isStarted)
		{
			isStarted = true;
			isGameOn = true;
			gunScript.StartGun();
			Time.timeScale = 1f;
			uIScript.startGame();
			StopCoroutine(CountDown());
			yield break;
		}
		else
		{
			Time.timeScale = 1f;
			isGameOn = true;
            GameObject.FindWithTag("Explode").GetComponent<ExplosionParticleScript>().StopExplosion();
            gunScript.StartGun();
			StopCoroutine(uIScript.AdvertiseReward());
			StopCoroutine(CountDown());
			if (uIScript.secondChance.gameObject.activeInHierarchy)
			{
				uIScript.secondChance.gameObject.SetActive(false);
			}
		}
	}
}




/* public IEnumerator CountDown()
   {
       int timer = 3;
       uIScript.countDown.gameObject.SetActive(true);
       while (timer >= 1)
       {
           uIScript.countDown.text = timer.ToString();
           yield return new WaitForSecondsRealtime(1f);;
           timer--;
           yield return null;
       }
       uIScript.countDown.gameObject.SetActive(false);
       if (!isStarted)
       {
           isStarted = true;
           isGameOn = true;
           gunScript.StartGun();
           Time.timeScale = 1f;
           uIScript.startGame();
           StopCoroutine(CountDown());
           yield break;
       }
       else
       {
           Time.timeScale = 1f;
           isGameOn = true;
           StopCoroutine(uIScript.AdvertiseReward());
           StopCoroutine(CountDown());
           if (uIScript.secondChance.gameObject.activeInHierarchy)
           {
               uIScript.secondChance.gameObject.SetActive(false);
           }
       }
   }

   public void SwipePlayerLeft()
   {
       PlayerSwipeLeft = StartCoroutine(SwipeLeft(Player.instance.gameObject, true, 2f, 10f));
   }

   public void SwipePlayerRight()
   {
       PlayerSwipeRight = StartCoroutine(SwipeRight(Player.instance.gameObject, true, 2f, 10f));
   }

   public void SwipeBlockLeft(GameObject go)
   {
       BlockSwipeLeft = StartCoroutine(SwipeLeft(go, false, 1f, 10f));
   }

   public void SwipeBlockRight(GameObject go)
   {
       BlockSwipeRight = StartCoroutine(SwipeRight(go, false, 1f, 10f));
   }

    public void SwipeChildBlockLeft(GameObject go)
   {
       ChildBlockSwipeLeft = StartCoroutine(SwipeLeft(go, false, 1f, 9f));
   }

   public void SwipeChildBlockRight(GameObject go)
   {
       ChildBlockSwipeRight = StartCoroutine(SwipeRight(go, false, 1f, 9f));
   }


   private IEnumerator SwipeLeft(GameObject tForm, bool isPlayer, float rate, float dist)
   {
       Vector3 initialPosition = tForm.transform.position;
       Vector3 tempPos;
       while (tForm.transform.position.x > initialPosition.x - dist)
       {
           tempPos = tForm.transform.position;
           tempPos.x -= rate;
           tForm.transform.position = tempPos;
           yield return new WaitForSeconds(.00001f);
       }
       if (isPlayer)
       {
           Player.instance.SetPlayerStateForward();
       }

       StopCoroutine(SwipeLeft(tForm, isPlayer, rate, dist));
   }

   private IEnumerator SwipeRight(GameObject tForm, bool isPlayer, float rate, float dist)
   {
       Vector3 initialPosition = tForm.transform.position;
       Vector3 tempPos;
       while (tForm.transform.position.x < initialPosition.x + dist)
       {
           tempPos = tForm.transform.position;
           tempPos.x += rate;
           tForm.transform.position = tempPos;
           yield return new WaitForSeconds(.00001f);
       }
       if (isPlayer)
       {
           Player.instance.SetPlayerStateForward();
       }
       StopCoroutine(SwipeRight(tForm, isPlayer, rate, dist));
   }*/