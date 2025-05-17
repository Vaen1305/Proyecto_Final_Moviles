using UnityEngine;
using System.Collections.Generic;

public class DinamicObjectPooling : MonoBehaviour
{
    public GameObject prefab;
    private List<GameObject> pool = new List<GameObject>();

    public GameObject GetObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
