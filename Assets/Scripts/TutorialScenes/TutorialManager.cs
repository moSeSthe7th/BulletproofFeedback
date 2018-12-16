using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    //public static bool isReady; Use if gamconst starts scene
    public bool isGameOn;
    public bool isLevel;

    public static int Level;

    public bool secondPushed;

    public GameObject[] blocks;
    public List<GameObject> Edges;
    public List<GameObject> Hollows;
    public List<GameObject> LevelupHollows;
    public GameObject bull;

    public float hollowUpdateDist;

    public float İnitBlockDist;
    public float BlockDistUpdate; // change this to decrease or increase distance between blocks
    public int points;
    public float LastPosOfArray;

    public float gameTime;


    public float energyTime;

    //public bool createNewLevel;

    public GameObject lastHit; //to change back color back

    public int blockNumber;

    //private CameraScript cameraScript;
    //private GameObject block;
    private TutGunScript gunScript;
    // private GameObject gun;

    //  private BlockScript blockScript;

    private TutUIScript uIScript;
    //    private BulletScript bulletScript;

    public static int playedTime;

    public int shifterBlock; // set 3 for 2 blocks ahead, Set 2 for block ahead
   

    public bool playerLock;

    public bool collid;

    public bool playerLockTurn;

    public bool didTurn;

    /* private Coroutine TutPlayerSwipeLeft;
     private Coroutine TutPlayerSwipeRight;

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

    }

    public void Start()
    {
        isLevel = false;
        gunScript = FindObjectOfType(typeof(TutGunScript)) as TutGunScript;

        uIScript = FindObjectOfType(typeof(TutUIScript)) as TutUIScript;

        blockNumber = 3; // aslında 16 tane oluyor ilk blok da olduğu için

        blocks = new GameObject[blockNumber + 1];

        Level = 1;

        gameTime = 0f;

        lastHit = null;

        İnitBlockDist = 100f;

        BlockDistUpdate = 90f; //Distance between block

        points = 0;
        energyTime = 100f;


        playerLock = false;
        playerLockTurn = false;
        collid = false;
        didTurn = false;

        hollowUpdateDist = BlockDistUpdate * 2; // arada 3 blok için 3 le çarp etc. 

        GameObject.FindWithTag("LevelHollows").transform.position = new Vector3(0f, 0f, (hollowUpdateDist * 4 ) + 180f);

        StartCoroutine(TutorialLoop());

    }

    private IEnumerator TutorialLoop()
    {
        yield return new WaitUntil(() => blocks[0] != null);
        isGameOn = true;
        playerLock = true;
        playerLockTurn = true;
        uIScript.startGame();
        Time.timeScale = 1f;
        HollowSetter(hollowUpdateDist);
        blocks[0].SetActive(true);
        blocks[0].transform.transform.position = new Vector3(0f,0f,hollowUpdateDist );
        blocks[0].transform.GetChild(0).transform.localPosition = TutPlatformTurnScript.instance.BlockPos[1];
        blocks[0].transform.GetChild(0).transform.localRotation = Quaternion.Euler(TutPlatformTurnScript.instance.BlockRot[1]);
        blocks[0].transform.GetChild(1).gameObject.SetActive(false);
        blocks[1].SetActive(true);
        blocks[1].transform.transform.position = new Vector3(0f, 0f, hollowUpdateDist * 2);
        blocks[1].transform.GetChild(0).transform.localPosition = TutPlatformTurnScript.instance.BlockPos[0];
        blocks[1].transform.GetChild(0).transform.localRotation = Quaternion.Euler(TutPlatformTurnScript.instance.BlockRot[0]);
        blocks[1].transform.GetChild(1).gameObject.SetActive(false);
        blocks[2].SetActive(true);
        blocks[2].transform.transform.position = new Vector3(0f, 0f, hollowUpdateDist * 3);
        blocks[2].transform.GetChild(0).transform.localPosition = TutPlatformTurnScript.instance.BlockPos[2];
        blocks[2].transform.GetChild(0).transform.localRotation = Quaternion.Euler(TutPlatformTurnScript.instance.BlockRot[2]);
        blocks[2].transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitUntil(() => TutPlayer.instance.transform.position.z >= blocks[0].transform.position.z - 100f);

        string f = "SWIPE DOWN";
        StartCoroutine(uIScript.SetTutorialTextIE(f));

        yield return new WaitUntil(() => TutPlayer.instance.transform.position.z >= blocks[0].transform.position.z - 40f);
        while (TutPlayer.instance.speed.z >= 0 && !collid){
            if(TutPlayer.instance.speed.z <= 5f){
                TutPlayer.instance.speed.z = 0;
            }
            else{
                TutPlayer.instance.speed.z -= 5f;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
        TutPlayer.instance.speed.z = TutPlayer.instance.initialSpeed;
        collid = false;
        playerLock = true;
        StartCoroutine(uIScript.DefaultTutorialTextIE());
        yield return new WaitUntil(() => TutPlayer.instance.transform.position.z >= blocks[1].transform.position.z - 120f);
        playerLockTurn = false;
        string s = "SWIPE LEFT";
        StartCoroutine(uIScript.SetTutorialTextIE(s));

        yield return new WaitUntil(() => TutPlayer.instance.transform.position.z >= blocks[1].transform.position.z - 60f);

        while (TutPlayer.instance.speed.z >= 20 && !collid)
        {
            if (TutPlayer.instance.speed.z <= 20f)
            {
                TutPlayer.instance.speed.z = 20f;
            }
            else
            {
                TutPlayer.instance.speed.z -= 5f;
            }
            if (TutPlayer.instance.transform.position.z >= (blocks[1].transform.localPosition.z + 10f))
            {
                collid = true;
            }
            yield return new WaitForSecondsRealtime(0.03f);
        } 
        TutPlayer.instance.speed.z = TutPlayer.instance.initialSpeed;
        collid = false;
        playerLock = true;
        StartCoroutine(uIScript.DefaultTutorialTextIE());
        yield return new WaitUntil(() => TutPlayer.instance.transform.position.z >= blocks[2].transform.position.z - 120f);
        string t = "SWIPE RIGHT";
        StartCoroutine(uIScript.SetTutorialTextIE(t));

        yield return new WaitUntil(() => TutPlayer.instance.transform.position.z >= blocks[2].transform.position.z - 70f);
        while (TutPlayer.instance.speed.z >= 20 && !collid)
        {
            if (TutPlayer.instance.speed.z <= 20f)
            {
                TutPlayer.instance.speed.z = 20f;
            }
            else
            {
                TutPlayer.instance.speed.z -= 5f;
            }
            if (TutPlayer.instance.transform.position.z >= (blocks[2].transform.localPosition.z + 10f))
            {
                collid = true;
            }
            yield return new WaitForSecondsRealtime(0.03f);
        }
        TutPlayer.instance.speed.z = TutPlayer.instance.initialSpeed;
        collid = false;
        playerLock = true;
        StartCoroutine(uIScript.DefaultTutorialTextIE());

        yield return new WaitForSeconds(.7f);
        playerLockTurn = true;
        yield return new WaitForSeconds(.3f);

        if(TutPlatformTurnScript.instance.currentLocation == Rotatable.CurrentLocation.right){
            TutPlatformTurnScript.instance.SwipePlatform((int)InputManager.direction.left);
        }
        else if (TutPlatformTurnScript.instance.currentLocation == Rotatable.CurrentLocation.left)
        {
            TutPlatformTurnScript.instance.SwipePlatform((int)InputManager.direction.right);
        }
        Vector3 bulletPos = new Vector3(TutPlatformTurnScript.instance.CoinAndBulletPos[1].x, TutPlatformTurnScript.instance.CoinAndBulletPos[1].y, TutPlayer.instance.transform.position.z + 150f);
        bull = gunScript.bulletOnTheWay(bulletPos, TutPlatformTurnScript.instance.BulletRot[1], -1*TutPlayer.instance.speed.z / 2);
        string b = "DODGE BULLET";
        StartCoroutine(uIScript.SetTutorialTextIE(b));

        yield return new WaitUntil(() => Vector3.Distance(TutPlayer.instance.transform.position,bull.transform.position) <= 50f);
        playerLockTurn = false;
        didTurn = false;
        while (TutPlayer.instance.speed.z >= 0 && !didTurn)
        {
            if (TutPlayer.instance.speed.z <= 5f)
            {
                TutPlayer.instance.speed.z = 0;
            }
            else
            {
                TutPlayer.instance.speed.z -= 5f;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
        StartCoroutine(uIScript.DefaultTutorialTextIE());
        while (TutPlayer.instance.speed.z < 150f)
        {
            if (TutPlayer.instance.speed.z >= 140f)
            {
                TutPlayer.instance.speed.z = 150f;
            }
            else
            {
                TutPlayer.instance.speed.z += 10f;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
        TutPlayer.instance.speed.z = 150f;
        collid = false;
        playerLock = true;
		FB.LogAppEvent (AppEventName.CompletedTutorial);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        StopCoroutine(TutorialLoop());
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

    private void Update()
    {
        if (isLevel)
        {
            gameTime += Time.deltaTime;
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

   public void SwipeTutPlayerLeft()
   {
       TutPlayerSwipeLeft = StartCoroutine(SwipeLeft(TutPlayer.instance.gameObject, true, 2f, 10f));
   }

   public void SwipeTutPlayerRight()
   {
       TutPlayerSwipeRight = StartCoroutine(SwipeRight(TutPlayer.instance.gameObject, true, 2f, 10f));
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


   private IEnumerator SwipeLeft(GameObject tForm, bool isTutPlayer, float rate, float dist)
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
       if (isTutPlayer)
       {
           TutPlayer.instance.SetTutPlayerStateForward();
       }

       StopCoroutine(SwipeLeft(tForm, isTutPlayer, rate, dist));
   }

   private IEnumerator SwipeRight(GameObject tForm, bool isTutPlayer, float rate, float dist)
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
       if (isTutPlayer)
       {
           TutPlayer.instance.SetTutPlayerStateForward();
       }
       StopCoroutine(SwipeRight(tForm, isTutPlayer, rate, dist));
   }*/