using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPlatformTurnScript : Rotatable
{

    public static TutPlatformTurnScript instance;

    private Coroutine SwipePlatformCs;

    public List<Vector3> BlockPos;
    public List<Vector3> BlockRot;
    public List<Vector3> CoinAndBulletPos;
    public List<Vector3> BulletRot;

    private float turnSpeed;

    public bool changing;

    public CurrentLocation currentLocation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        changing = false;
        currentLocation = CurrentLocation.middle;
        turnSpeed = 10f; //increase to slow

        GameObject edgeLeft = this.transform.GetChild(0).transform.GetChild(0).gameObject;
        GameObject edgeMiddle = this.transform.GetChild(0).transform.GetChild(1).gameObject;
        GameObject edgeRight = this.transform.GetChild(0).transform.GetChild(2).gameObject;

        GameObject block = this.transform.GetChild(1).transform.GetChild(0).gameObject;

        GameObject bullet = this.transform.GetChild(2).gameObject;
        //Debug.LogWarning(edgeRight.transform.up.normalized);

        edgeLeft.transform.RotateAround(Vector3.zero, Vector3.back, 60f);
        edgeRight.transform.RotateAround(Vector3.zero, Vector3.forward, 60f);

        //Vector3 left = new Vector3(-8.8f,-5f,0f);
        Vector3 middle = block.transform.position;
        Vector3 middleR = new Vector3(0f, 0f, 0f);
        //Vector3 right = new Vector3(8.8f, -5f, 0f);
        block.transform.RotateAround(Vector3.zero, Vector3.back, 60f);

        Vector3 leftUp = block.transform.position;
        Vector3 leftUpR = new Vector3(0f, 0f, -60f);
        block.transform.RotateAround(Vector3.zero, Vector3.forward, 120f);

        Vector3 rightUp = block.transform.position;
        Vector3 rightUpR = new Vector3(0f, 0f, 60f);
        BlockPos.Add(leftUp);
        BlockPos.Add(middle);
        BlockPos.Add(rightUp);

        BlockRot.Add(leftUpR);
        BlockRot.Add(middleR);
        //BlockRot.Add(rightR);
        BlockRot.Add(rightUpR);
        //BlockRot.Add(middleUpR);
        //BlockRot.Add(leftUpR);
        bullet.transform.position = new Vector3(0f, TutPlayer.instance.transform.position.y, -30f);
        Vector3 bulletPosMiddle = bullet.transform.position;
        bullet.transform.RotateAround(Vector3.zero, Vector3.back, 60f);

        Vector3 bulletPosLeft = bullet.transform.position;
        bullet.transform.RotateAround(Vector3.zero, Vector3.forward, 120f);

        Vector3 bulletPosRight = bullet.transform.position;
        //Vector3 coinPosMiddleUp = new Vector3(0f, -TutPlayer.instance.transform.position.y, 0f);
        CoinAndBulletPos.Add(bulletPosLeft);
        CoinAndBulletPos.Add(bulletPosMiddle);
        CoinAndBulletPos.Add(bulletPosRight);
        BulletRot.Add(new Vector3(0, 0, 30f));
        BulletRot.Add(new Vector3(0, 0, 90f));
        BulletRot.Add(new Vector3(0, 0, 150f));
    }

    public void SwipePlatform(int dirr)
    {
        float angle = (transform.eulerAngles.z < 180f) ? transform.eulerAngles.z : transform.eulerAngles.z - 360;

        if (dirr == (int)InputManager.direction.left && !changing && angle < 60f && TutPlayer.instance.state != (int)TutPlayer.State.down)//angle <= 0f)
        {
            changing = true;
            TutPlayer.instance.SetPlayerRight();
            SwipePlatformCs = StartCoroutine(PlatformTurner(dirr, currentLocation, turnSpeed));
        }
        else if (dirr == (int)InputManager.direction.right && !changing && angle > -60f && TutPlayer.instance.state != (int)TutPlayer.State.down)//angle >= 0f)
        {
            changing = true;
            TutPlayer.instance.SetPlayerLeft();
            SwipePlatformCs = StartCoroutine(PlatformTurner(dirr, currentLocation, turnSpeed));
        }
        else
            Debug.LogError("dönüyor veya uçta çeviremem bi daha aqq " + this.transform.eulerAngles + " angle ");
    }

    private void Setter(int platform)
    {
        if (platform == (int)CurrentLocation.left)
        {
            currentLocation = CurrentLocation.left;
            this.transform.localRotation = (this.transform.localRotation != Quaternion.Euler(0f, 0f, 60f)) ? Quaternion.Euler(0f, 0f, 60f) : this.transform.localRotation;
            rot = transform.localRotation.eulerAngles;
        }
        else if (platform == (int)CurrentLocation.middle)
        {
            currentLocation = CurrentLocation.middle;
            this.transform.localRotation = (this.transform.localRotation != Quaternion.Euler(0f, 0f, 0f)) ? Quaternion.Euler(0f, 0f, 0f) : this.transform.localRotation;
            rot = transform.localRotation.eulerAngles;
        }
        else if (platform == (int)CurrentLocation.right)
        {
            currentLocation = CurrentLocation.right;
            this.transform.localRotation = (this.transform.localRotation != Quaternion.Euler(0f, 0f, 300f)) ? Quaternion.Euler(0f, 0f, 300f) : this.transform.localRotation;
            rot = transform.localRotation.eulerAngles;
        }
        changing = false;
    }


    private IEnumerator PlatformTurner(int direction, CurrentLocation platformPosition, float speed)
    {
        if (direction == (int)InputManager.direction.left)
        {
            rot = this.transform.localRotation.eulerAngles;

            float init = 0;
            float updated = init;
            while (updated < init + 60f)
            {
                rot += Degree60(speed);
                this.transform.localRotation = Quaternion.Euler(rot);
                updated += Degree60(speed).z;
                yield return new WaitForFixedUpdate();
            }

            TutPlayer.instance.SetPlayerStateForward();

            Setter((int)platformPosition - 1);
        }
        else if (direction == (int)InputManager.direction.right)
        {
            rot = this.transform.localRotation.eulerAngles;
            float init = 0f;
            float updated = init;
            while (updated < init + 60f)
            {
                rot -= Degree60(speed);
                this.transform.localRotation = Quaternion.Euler(rot);
                updated += Degree60(speed).z;
                yield return new WaitForFixedUpdate();
            }

            TutPlayer.instance.SetPlayerStateForward();

            Setter((int)platformPosition + 1);
        }

        StopCoroutine(SwipePlatformCs);

    }

}
