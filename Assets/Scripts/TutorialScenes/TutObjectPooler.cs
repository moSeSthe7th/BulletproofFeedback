using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutObjectPooler : MonoBehaviour
{

    public static TutObjectPooler SharedInstance;

    public List<GameObject> pooledObjects;

    /* private GameObject objectToPool;
     private int amountToPool;

     private bool shouldExpand;*/

    public List<ObjectPoolItem> itemsToPool;

    [System.Serializable]
    public class ObjectPoolItem
    {
        public int amountToPool;
        public GameObject objectToPool;
        public bool shouldExpand;
        public string objectTag;
    }

    ObjectPoolItem bullet;
    ObjectPoolItem block;
    ObjectPoolItem edge;
    ObjectPoolItem hollow;
    ObjectPoolItem fullHollows;

    void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        GameObject Bullet = GameObject.FindWithTag("Bullet");
        GameObject Block = GameObject.FindWithTag("Blocks");
        GameObject Edge = GameObject.FindWithTag("Edge");
        GameObject Hollow = GameObject.FindWithTag("HollowFill");
        GameObject LevelHollows = GameObject.FindWithTag("LevelHollows");

        bullet = new ObjectPoolItem();

        bullet.shouldExpand = true;
        bullet.amountToPool = 3;
        bullet.objectToPool = Bullet;
        bullet.objectTag = Bullet.tag;

        block = new ObjectPoolItem();

        block.shouldExpand = false;
        block.amountToPool = TutorialManager.instance.blockNumber;
        block.objectToPool = Block;
        block.objectTag = Block.tag;
        TutorialManager.instance.blocks[0] = Block;
        Block.SetActive(false);

        edge = new ObjectPoolItem();

        edge.shouldExpand = true;
        edge.amountToPool = 17;
        edge.objectToPool = Edge;
        edge.objectTag = Edge.tag;

        hollow = new ObjectPoolItem();

        //float distHollow = 0f;
        if (Hollow != null)
        {
            hollow.shouldExpand = false;
            hollow.amountToPool = 3;
            hollow.objectToPool = Hollow;
            hollow.objectTag = Hollow.tag;
            TutorialManager.instance.Hollows.Add(hollow.objectToPool);
            TutorialManager.instance.Hollows[0].GetComponent<TutHollowFillScript>().SetQueNumber(0);
            //distHollow = TutorialManager.instance.hollowUpdateDist;
            Hollow.SetActive(false);
        }
        /* else
         {
             hollow.amountToPool = 14;
             hollow.objectToPool = GameObject.FindWithTag("Hollow");
             distHollow = 50f;
         }*/

        fullHollows = new ObjectPoolItem();

        fullHollows.shouldExpand = true;
        fullHollows.amountToPool = 50;
        fullHollows.objectToPool = LevelHollows.transform.GetChild(0).gameObject;
        fullHollows.objectTag = LevelHollows.transform.GetChild(0).gameObject.tag;


        itemsToPool.Add(bullet);
        itemsToPool.Add(block);
        itemsToPool.Add(edge);
        itemsToPool.Add(hollow);
        itemsToPool.Add(fullHollows);

        pooledObjects = new List<GameObject>();
        //Debug.Log("Object Pooler created pooled objects");
        float distEdge = 20f;
        float fullDist = 10f;

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                if (item == fullHollows)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool, LevelHollows.transform);
                    //obj.SetActive(false);
                    pooledObjects.Add(obj);
                    TutorialManager.instance.LevelupHollows.Add(obj);
                    fullDist = LevelHollowGetDistance.SetHollow(TutorialManager.instance.LevelupHollows[i], fullDist);
                }
                else
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool, GameObject.FindWithTag("ALLPLATFORM").transform);

                    pooledObjects.Add(obj);
                    if (item == block)
                    {
                        TutorialManager.instance.blocks[i + 1] = obj;
                    }
                    if (item == edge)
                    {
                        TutorialManager.instance.Edges.Add(obj);
                        distEdge = PlatformScript.SetPlatform(TutorialManager.instance.Edges[i], distEdge);
                    }
                    if (item == hollow)
                    {
                        obj.SetActive(false);
                        TutorialManager.instance.Hollows.Add(obj);
                        TutorialManager.instance.Hollows[i + 1].GetComponent<TutHollowFillScript>().SetQueNumber(i + 1);
                    }
                }
            }
        }
    }

    public GameObject GetPooledObject(string tag, Transform parent)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectTag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool, parent);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
