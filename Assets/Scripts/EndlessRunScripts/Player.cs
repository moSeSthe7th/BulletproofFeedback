using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //variables
    private Rigidbody rb;

    //private Vector3 tempPos;

    public float initialSpeed;
    public Vector3 speed;
    public float bulletProofSpeed;
    public float normalSpeed;

    public int state;
    public int mode;
    public int strikeConstant;
    public int modeSpeedIncrease;

    private bool canCollide;
    private bool collidedNow;
    private bool enteredCollision;

    private float goDownSpeed;
    private float goUpSpeed;

    private int blockLayerIndex; // 9 layer of block
    private LayerMask blockLayerMask;
    private int coinLayerIndex;
    private int edgeLayerIndex;
    private RaycastHit hit;//understand if hit

    private Vector3 rayVector;//shooted rays direction
    private UIScript uIScript;

    public static Player instance;

    private Vector3 strecthXAmount;
    private Vector3 initialScale;

    //private bool isCollidedWBlock;

    public int lastPassedBlock;
    //private bool didHit;

    public float energyTot;
    public float energyDecreaseRate;
    private float gainedEnergy;
    private float bulletImpactEnergyLost;

    public int strike; // set double point if strike is 5

    public bool isOnStrike;
    private int vibrationInt;
    private bool isVibrationOn;

    public Color HittableBlockColor;

    private StrikeParticleScript strikeParticleScript;

    public CameraShake cameraShake;

    //private Color orgBlockColor;

    public int bulletHits;

    public float height;

    private float swipeDownIncreaseRate;

    public int pointIndex;

    public enum State
    {
        swipingRight,
        swipingLeft,
        down,
        ToUp,
        forward
    }

    public enum Mode
    {
        normal,
        bulletProof
    }


    //_________________________METHODS___________________________

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Player created1");
            instance = this;
        }
        else
        {
            Debug.LogError("New player destroyed");
            Destroy(this.gameObject);
        }
        initialSpeed = 85f;

        speed = new Vector3(0f, 0f, PlayerAccelerator.GetPlayerNormalSpeed(initialSpeed));
        mode = (int)Mode.normal;
    }


    void Start()
    {
        state = (int)State.forward;

        modeSpeedIncrease = 4;
        RayCalculator(modeSpeedIncrease);

        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        blockLayerIndex = LayerMask.NameToLayer("block");
        blockLayerMask.value = (int)(1 << blockLayerIndex);
        coinLayerIndex = LayerMask.NameToLayer("coin");
        edgeLayerIndex = LayerMask.NameToLayer("edge");
        uIScript = FindObjectOfType(typeof(UIScript)) as UIScript;

        mode = (int)Mode.normal;
        normalSpeed = speed.z;
        bulletProofSpeed = speed.z;
        strike = 0;
        strikeConstant = 0;
        isOnStrike = false;

        vibrationInt = PlayerPrefs.GetInt("Vibration", 1);

        if (vibrationInt == 1)
        {
            isVibrationOn = true;
        }
        else
        {
            isVibrationOn = false;
        }

        initialScale = transform.localScale;
        strecthXAmount = new Vector3(1.8f, 1.2f, 1.8f);

        canCollide = false;
        collidedNow = false;

        lastPassedBlock = -1;

        energyTot = (GameConst.instance.energyTime) / 3;
        energyDecreaseRate = 10f; // for default
        gainedEnergy = (GameConst.instance.energyTime - energyTot) / 5;
        bulletImpactEnergyLost = (GameConst.instance.energyTime - energyTot) / 3;

        HittableBlockColor = Color.white;

        strikeParticleScript = FindObjectOfType(typeof(StrikeParticleScript)) as StrikeParticleScript;

        cameraShake = FindObjectOfType(typeof(CameraShake)) as CameraShake;

        bulletHits = 0;

        height = this.transform.position.y;

        pointIndex = 1;

        //enteredCollision = false;
    }

    public void Update()
    {

        if (GameConst.instance.isGameOn)
        {
            if(mode == (int)Mode.normal)
            {
                this.transform.Rotate(new Vector3(speed.z * 8, 0, 0) * Time.deltaTime, Space.Self);
            }
            else
            {
                this.transform.Rotate(new Vector3(speed.z * 11, 0, 0) * Time.deltaTime, Space.Self);
            }
          
            //_____________________________For making blocks change COLOR_________________________________________//
            if (Physics.Raycast(transform.position, rayVector, out hit, 20f, blockLayerMask.value))
            {
                if (hit.collider.gameObject.layer == blockLayerIndex && hit.normal == hit.collider.gameObject.transform.up && !canCollide && state == (int)State.forward)
                {
                    canCollide = true;
                    GameConst.instance.lastHit = hit.collider.gameObject;
                    //orgBlockColor = hit.collider.GetComponent<Renderer>().material.GetColor("_Color");
                    Color color = Color.Lerp(GameConst.instance.lastHit.GetComponentInParent<BlockColorChanger>().GetColor(), HittableBlockColor, .4f);
                    hit.collider.GetComponent<Renderer>().material.SetColor("_Color", color);
                }
            }
            else if (GameConst.instance.lastHit != null && canCollide)
            {
                canCollide = false;
                GameConst.instance.lastHit.GetComponent<Renderer>().material.SetColor("_Color", GameConst.instance.lastHit.GetComponentInParent<BlockColorChanger>().GetColor());
            }

            // Energy Düsürme ve 0 sa bölüm sonlandırma ________________

            if (energyTot <= GameConst.instance.energyTime / 3 && GameConst.instance.isLevel && strike <= 0)
            {
                energyTot -= Time.deltaTime * energyDecreaseRate;
            }

            if ((energyTot <= 0 && !GameConst.instance.isEnergyFinished))
            {
                GameConst.instance.isEnergyFinished = true;
                StartCoroutine(Player.instance.EnergyFinish());
            }


            // ____________________BulletProof Hız ayarlama _______________________________________

            if(mode == (int)Mode.bulletProof && !PlayerAccelerator.PlayerFastEnough(speed.z, bulletProofSpeed) && !GameConst.instance.playerLock)
            {
                speed.z += .5f;
                if(PlayerAccelerator.PlayerFastEnough(speed.z, bulletProofSpeed) && strikeConstant == 1){
                    RayCalculator(modeSpeedIncrease);
                }
            }
            else if(mode == (int)Mode.normal && !PlayerAccelerator.PlayerNormalSpeed(speed.z, normalSpeed) && !GameConst.instance.playerLock)
            {
                speed.z -= .5f;
                if (PlayerAccelerator.PlayerNormalSpeed(speed.z, normalSpeed)){
                    RayCalculator(modeSpeedIncrease);
                }
            }


            //____________________________ Passed Block Calculation && Calculation of if not collided end bulletmode ______________________//

            if (lastPassedBlock <= GameConst.instance.blockNumber - 1 && GameConst.instance.blocks[lastPassedBlock + 1].transform.position.z + 15f <= this.transform.position.z && GameConst.instance.isLevel)
            {
                lastPassedBlock += 1;
                //Debug.Log(lastPassedBlock);
                if(!GameConst.instance.blocks[lastPassedBlock].gameObject.GetComponent<BlockScript>().didCollided)
                {
                    if(strike > 0)
                    {
                        energyTot = GameConst.instance.energyTime / 3;
                        strike = 0;
                    }

                    if (mode == (int)Mode.bulletProof)
                    {
                        changeToNormal();
                    }
                }
            }

            //______________________________ END OF LEVEL ______________________________//

            if (transform.position.z >= GameConst.instance.LastPosOfArray + 20f && GameConst.instance.isLevel) //&& GameConst.instance.destroyedBlockNumber >= GameConst.instance.BlockDestroyLimit)
            {
                Debug.Log("Bölüm Geçildi !!!!!!!!!!!!!!!!!!!!");
                GameConst.instance.isLevel = false;
                GameConst.instance.LevelChanger(speed.z + 180f);
                lastPassedBlock = -1;
            }

            //___________________ END OF KAFA ATMA MODU  3 mermi yedin go_______________________//

            if (mode == (int)Mode.bulletProof && bulletHits >= 3)
            {
                changeToNormal();
            }

        }
    }

    void FixedUpdate()
    {
        //movement
        if (GameConst.instance.isGameOn )
        {
            rb.velocity = speed;
        }
    }

    public void SwipeDownInitiate()
    {
        if(state == (int)State.forward && !GameConst.instance.playerLock)
        {
            state = (int)State.down;
            StartCoroutine(swipeDown());
        }
    }

    public void SetPlayerStateForward()
    {
        state = (int)State.forward;
    }
    public void SetPlayerRight()
    {
        state = (int)State.swipingRight;
    }
    public void SetPlayerLeft()
    {
        state = (int)State.swipingLeft;
    }

    private void SetEnergy()
    {
        if(energyTot <= GameConst.instance.energyTime / 3)
        {
            energyTot = (GameConst.instance.energyTime / 3) + gainedEnergy;
        }
        else
        {
            energyTot += gainedEnergy;
        }
     /*   else if (energyTot <= GameConst.instance.energyTime)
        {
            energyTot = GameConst.instance.energyTime;
        }*/
    }

    private void BlockCollision(Collision col)
    {
        StartCoroutine(ImpactEffect());

        uIScript.PlayHitSound();

        strikeParticleScript.setStrikePSPos(col.contacts[0].point);

        // ______________ *********** Strike Calc ************** _____________

        GameConst.instance.blocks[lastPassedBlock + 1].gameObject.GetComponent<BlockScript>().didCollided = true;
        strike++;
      
        if (!isOnStrike && strike >= 5)
        {
            ChangeToBersek();
        }
        else if(strike >= 5 && strike % 5 == 0)
        {
            bulletProofSpeed += 2f;
            pointIndex += 1;
            strikeConstant += 1;
            //RayCalculator();
        }
        //****** _________________________ Will Block Shift Calc _______________________ ****** //

        //GameConst.instance.shifterBlock = (Random.Range(0, GameConst.Level) < 4) ? 3 : 2;

        if (lastPassedBlock <= GameConst.instance.blocks.Length - GameConst.instance.shifterBlock - 1)
        {
            GameConst.instance.blocks[lastPassedBlock + GameConst.instance.shifterBlock].gameObject.GetComponent<BlockScript>().Shift();
        }

        this.transform.Rotate(new Vector3(speed.z, 0, 0), Space.Self);

        GameConst.instance.PointDecider(isOnStrike);
        if (isVibrationOn)
        {
            VibrationPop.vibrateforDuration(1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == blockLayerIndex)
        {
            Vector3 normal = collision.contacts[0].normal;
            if(canCollide || normal == collision.gameObject.transform.up /*&& !enteredCollision*/)
            {
                collidedNow = true;
                //enteredCollision = true;
                if (mode == (int)Mode.normal)
                {
                    SetEnergy();
                }
                BlockCollision(collision);
            }
            else
            {
                if (GameConst.instance.isGameOn)
                {
                    GameConst.instance.isGameOn = false;
                    this.speed = Vector3.zero;
                    uIScript.StartCoroutine(uIScript.AdvertiseReward());
                    if (isVibrationOn)
                    {
                        VibrationPop.vibrateforDuration(2);
                    }
                }
            }
        }
        else if(collision.gameObject.layer == coinLayerIndex)
        {
            int gold = PlayerPrefs.GetInt("Gold", 0);
            gold += 3;
            PlayerPrefs.SetInt("Gold", gold);
            collision.gameObject.SetActive(false);
            this.transform.position = new Vector3(this.transform.position.x, height, this.transform.position.z);
            if (isVibrationOn)
            {
                VibrationPop.vibrateforDuration(1);
            }
        }
        else if(collision.gameObject.layer == edgeLayerIndex)
        {
            if (GameConst.instance.isGameOn)
            {
                GameConst.instance.isGameOn = false;
                this.speed = Vector3.zero;
                uIScript.StartCoroutine(uIScript.AdvertiseReward());
                if (isVibrationOn)
                {
                    VibrationPop.vibrateforDuration(2);
                }
            }
        }   
    }

   /* private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == blockLayerIndex)
        {
            enteredCollision = false;  
        }
    }*/

    public void ChangeToBersek()
    {
        isOnStrike = true;
        mode = (int)Mode.bulletProof;
        bulletProofSpeed = PlayerAccelerator.GetPlayerNormalSpeed(initialSpeed) + modeSpeedIncrease;
        GameConst.instance.gatheredPower = 0;
        pointIndex += 1;
        strikeConstant += 1;
        //RayCalculator(modeSpeedIncrease);
    }

    public void changeToNormal()
    {
        isOnStrike = false;
        normalSpeed = PlayerAccelerator.GetPlayerNormalSpeed(initialSpeed);
        mode = (int)Mode.normal;
        energyTot = GameConst.instance.energyTime / 3;
        strike = 0;
        strikeConstant = 0;
        pointIndex = 1;
        bulletHits = 0;

        //RayCalculator(modeSpeedIncrease);
    }

    public void BulletImpactOutput(float amount, float time)
    {
        this.energyTot -= bulletImpactEnergyLost;
       if (isVibrationOn)
       {    
           VibrationPop.vibrateforDuration(2);
       }      
       cameraShake.ShakeCamera(amount, time);
    }

    IEnumerator goUp()
    {
        state = (int)State.ToUp;
        speed.y = goUpSpeed; //* 3 / 4;
        //yield return new WaitUntil(() => transform.position.y > -3.0f);
        while (transform.position.y < height - 0.1f)
        {
            if (speed.y >= 5f)
            {
                speed.y -= 2.5f;
                //Debug.Log(speed.y);
            }
            yield return new WaitForSeconds(.01f);
        }
        state = (int)State.forward;
       // Debug.Log("Minimum y hızı yukarı " + speed.y);
        speed.y = 0f;
        this.transform.position = new Vector3(this.transform.position.x, height, this.transform.position.z);
        StopCoroutine(goUp());
    }

    IEnumerator swipeDown()
    {
        speed.y = goDownSpeed;
        while (!collidedNow)
        {
            speed.y -= swipeDownIncreaseRate;
            yield return new WaitForSeconds(.01f);
        }
        //Debug.Log("Maximum y hızı aşağı " + speed.y);       
        state = (int)State.ToUp;
        collidedNow = false;
        StartCoroutine(goUp());
        StopCoroutine(swipeDown());
    }

    IEnumerator ImpactEffect()
    {
        while (Vector3.Distance(initialScale, transform.localScale) < Vector3.Distance(initialScale, strecthXAmount))
        {
            transform.localScale += new Vector3(0.05f, -0.05f, 0.05f);
            yield return new WaitForSeconds(.001f);
        }
        yield return new WaitForSeconds(0.1f);
        while (Vector3.Distance(transform.localScale, initialScale) > 0f)
        {
            transform.localScale += new Vector3(-0.05f, 0.05f, -0.05f);
            yield return new WaitForSeconds(.001f);
        }

        StopCoroutine(ImpactEffect());
    }

    public IEnumerator EnergyFinish()
    {
        float orgSpeedZ = speed.z;
        //float orgSpeedY = speed.y;
        //yield return new WaitForSeconds(1f);
        while (energyTot <= 0 && GameConst.instance.isEnergyFinished)
        {
            if (speed.z > 0)
                speed.z -= 5f;
            speed.y -= 1f;
            yield return new WaitForSeconds(.1f);
            if (energyTot >= 0)
            {
                GameConst.instance.isEnergyFinished = false;
                speed.z = orgSpeedZ;
                // speed.y = orgSpeedY;
            }
        }
        Debug.Log("exiting energy");
        StopCoroutine(EnergyFinish());
    }

    public void RayCalculator(int modeConst)
    {
        goDownSpeed = ((speed.z * 1 / 4) + (strikeConstant * modeConst)) * -1;
        swipeDownIncreaseRate = 5f;
        goUpSpeed = speed.z + (strikeConstant * modeConst);

        float MaxGoDownSpeed = goDownSpeed - (11 * swipeDownIncreaseRate);
        float allSpeedsTot = ((goDownSpeed + MaxGoDownSpeed) * (goDownSpeed - MaxGoDownSpeed + swipeDownIncreaseRate)) / (swipeDownIncreaseRate * 2);
        float Totnumber = ((goDownSpeed - MaxGoDownSpeed) / swipeDownIncreaseRate) + 1;

        rayVector = new Vector3(0f, allSpeedsTot / Totnumber, speed.z);
    }

    public void StopAll(){
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        StopAllCoroutines();
        this.enabled = false;
        speed = Vector3.zero;
    }
    public void StartAll(){
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        this.enabled = true;
        float z = transform.position.z - 15f;
        transform.position = new Vector3(0, height, z);
        transform.localScale = initialScale;
        speed = new Vector3(0f, 0f, PlayerAccelerator.GetPlayerNormalSpeed(initialSpeed));
        changeToNormal();
        SetPlayerStateForward();
        energyTot = GameConst.instance.energyTime / 3f;
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}


/*private void swipeDown()
    {
        speed.y = speed.z * -3 / 4;
    }*/

/* void swipeUp()
   {
       speed.y = speed.z;
   }*/

/* private void changeSpeed()
{
    if(GameConst.gameTime >= speedInterval)
    {
        speed.z =  speed.z + 1f;
        speedInterval += 10f;
    }
}*/

/*IEnumerator swipeRight()
{
    Vector3 initialPosition = transform.position;
    while (transform.position.x < initialPosition.x + 10)
    {
        tempPos = transform.position;
        tempPos.x += 2f;
        transform.position = tempPos;
        yield return new WaitForSeconds(.00001f);
    }
    state = (int)State.forward;
    StopCoroutine(swipeRight());
}*/


/*  IEnumerator _swipeDown()
  {
      //speed.y = speed.z * 3 / 4 * -1;
      //  yield return new WaitUntil(() => didCollide = true;);
      Debug.LogError("entered down");
      while (didCollide == false)
      {
          Debug.Log(speed.y);
          speed.y -= 1f;
          yield return new WaitForSeconds(.00001f);
      }
      speed.y = 0f;
      didCollide = false;
      //state = (int)State.forward;
      //StopCoroutine(GoUpEffect());
      Debug.LogError("exiting down");
      StopCoroutine(_swipeDown());
  }*/

/*IEnumerator swipeLeft()
{
    Vector3 initialPosition = transform.position;
    while (transform.position.x > initialPosition.x - 10)
    {
        tempPos = transform.position;
        tempPos.x -= 2f;
        transform.position = tempPos;
        yield return new WaitForSeconds(.00001f);
    }
    state = (int)State.forward;
    StopCoroutine(swipeLeft());
}*/
