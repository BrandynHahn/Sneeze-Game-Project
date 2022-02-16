using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool;
    public GameObject screenbounds;
    public GameObject AdultPrefab;
    public GameObject ChildPrefab;
    public GameObject OldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Adult = Instantiate(AdultPrefab) as GameObject;
        GameObject Old = Instantiate(OldPrefab) as GameObject;
        GameObject Child = Instantiate(ChildPrefab) as GameObject;
    }

    public void spawnObject()
    {
        Random.Range(5, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
