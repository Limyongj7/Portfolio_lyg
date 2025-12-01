using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    List<GameObject>[] ePool;
    
    public GameObject[] weaponPrefabs;
    List<GameObject>[] wPool;

    private void Awake()
    {
        ePool = new List<GameObject>[enemyPrefabs.Length];
        wPool = new List<GameObject>[weaponPrefabs.Length];

        for (int i = 0; i < ePool.Length; i++)
        {
            ePool[i] = new List<GameObject>();
        }

        for (int i = 0; i < wPool.Length; i++)
        {
            wPool[i] = new List<GameObject>();
        }


    }

    public GameObject Eget(int index) //  몬스터 풀링
    {
        GameObject select = null;


        foreach (GameObject item in ePool[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }    

        if (!select)
        {
            select = Instantiate(enemyPrefabs[index], transform);
            ePool[index].Add(select);
        }


        return select;
    }
    public GameObject Wget(int index)  //  총알 풀링
    {
        GameObject select = null;


        foreach (GameObject item in wPool[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(weaponPrefabs[index], transform);
            wPool[index].Add(select);
        }


        return select;
    }
}
