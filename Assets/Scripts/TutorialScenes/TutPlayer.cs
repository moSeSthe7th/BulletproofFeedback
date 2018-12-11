using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPlayer : MonoBehaviour
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
    private int edgeLayerIndex;
    private RaycastHit hit;//understand if hit

    private Vector3 rayVector;//shooted rays direction
    private TutUIScript uIScript;

    public static TutPlayer instance;

    private Vector3 strecthXAmount;
    private Vector3 initialScale;

    //private bool isCollidedWBlock;

    public int lastPassedBlock;
    //private bool didHit;

    public float energyTot;
    public float energyDecreaseRate;
    private float gainedEnergy;

    public int strike; // set double point if strike is 5

    public bool isOnStrike;
    private int vibrationInt;
    private bool isVibrationOn;

    public Color HittableBlockColor;

    private StrikeParticleScript strikeParticleScript;

    public CameraShake cameraShake;

    private Color orgBlockColor;

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
        speed = new Vector3(0f, 0f, initialSpeed);
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
        edgeLayerIndex = LayerMask.NameToLayer("edge");
        uIScript = FindObjectOfType(typeof(TutUIScript)) as TutUIScript;

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

        energyTot = (TutorialManager.instance.energyTime) / 3;
        energyDecreaseRate = 10f; // for default
        gainedEnergy = (TutorialManager.instance.energyTime - energyTot) / 5;

        HittableBlockColor = Color.white;

        strikeParticleScript = FindObjectOfType(typeof(StrikeParticleScript)) as StrikeParticleScript;

        height = this.transform.position.y;

        pointIndex = 1;

        //enteredCollision = false;
    }

    public void Update()
    {

        if (TutorialManager.instance.isGameOn)
        {
            if (mode == (int)Mode.normal)
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
                    TutorialManager.instance.playerLock = false;
                    canCollide = true;
                    TutorialManager.instance.lastHit = hit.collider.gameObject;
                    orgBlockColor = hit.collider.GetComponent<Renderer>().material.GetColor("_Color");
                    Color color = Color.Lerp(orgBlockColor, HittableBlockColor, .4f);
                    hit.collider.GetComponent<Renderer>().material.SetColor("_Color", color);
                }
            }
            else if (TutorialManager.instance.lastHit != null && canCollide)
            {
                canCollide = false;
                TutorialManager.instance.lastHit.GetComponent<Renderer>().material.SetColor("_Color", orgBlockColor);
            }

        }
    }

    void FixedUpdate()
    {
        //movement
        if (TutorialManager.instance.isGameOn)
        {
            rb.velocity = speed;
        }
    }

    public void SwipeDownInitiate()
    {
        if (state == (int)State.forward && !TutorialManager.instance.playerLock)
        {
            speed.z = initialSpeed;
            TutorialManager.instance.collid = true;
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
        if (energyTot <= TutorialManager.instance.energyTime / 3)
        {
            energyTot = (TutorialManager.instance.energyTime / 3) + gainedEnergy;
        }
        else
        {
            energyTot += gainedEnergy;
        }
        /*   else if (energyTot <= TutorialManager.instance.energyTime)
           {
               energyTot = TutorialManager.instance.energyTime;
           }*/
    }

    private void BlockCollision(Collision col)
    {
        StartCoroutine(ImpactEffect());

        uIScript.PlayHitSound();

        if (isVibrationOn)
        {
            VibrationPop.vibrateforDuration(1);
        }

        strikeParticleScript.setStrikePSPos(col.contacts[0].point);

        // ______________ *********** Strike Calc ************** _____________

        this.transform.Rotate(new Vector3(speed.z, 0, 0), Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == blockLayerIndex)
        {
            Vector3 normal = collision.contacts[0].normal;
            if (canCollide || normal == collision.gameObject.transform.up /*&& !enteredCollision*/)
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
                if (TutorialManager.instance.isGameOn)
                {
                    TutorialManager.instance.isGameOn = false;
                    this.speed = Vector3.zero;
                }
            }
        }
        else if (collision.gameObject.layer == edgeLayerIndex)
        {
            if (TutorialManager.instance.isGameOn)
            {
                TutorialManager.instance.isGameOn = false;
                this.speed = Vector3.zero;
            }
        }
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
    if(TutorialManager.gameTime >= speedInterval)
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
