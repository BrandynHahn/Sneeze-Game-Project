
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Manages spawning bots and market
/// </summary>
public class PandemicArea : MonoBehaviour
{
    [Tooltip("Range of the area")]
    public float range;

    [Header("Bot settings")]
    public GameObject dummyBot;
    public int healthyBotCount;
    public int infectedBotCount;

    //List of DummyBots
    private List<GameObject> dummyBotList = new List<GameObject>();


    [Header("Infection Settings")]
    [Tooltip("The maximum possible distance for exposure to occur aka radius (Default 8f)")]
    public float exposureRadius = 8f;

    [Tooltip("Propability of getting infected is divided byinfectionCoeff. (1 is most infectious 100 is minimum infectious)")]
    [Range(500f, 1f)]
    public float infectionCoeff = 50f;

    [Tooltip("Recovery time after the infection starts")]
    public float recoverTime = 50f;

    [Header("SIR Model")]
    [System.NonSerialized]
    public int healthyCounter;
    [System.NonSerialized]
    public int infectedCounter = 0;
    [System.NonSerialized]
    public int recoveredCounter = 0;

    /// <summary>
    /// Creates objects in random position at given amount
    /// </summary>
    /// <param name="obj">The object which will be initialized</param>
    /// <param name="num">Number of objects </param>
    public void CreateObjectAtRandomPosition(GameObject obj, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject f = Instantiate(obj, ChooseRandomPosition(), Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f)));
        }
    }
    /// <summary>
    /// Creates objets in random position with given amount of healthy and infected agents
    /// </summary>
    /// <param name="obj"> The object which will be instantiated</param>
    /// <param name="goodNum">The number of healthy agents</param>
    /// <param name="infectedNum">The number of infected agents</param>
    public void CreateObjectAtRandomPosition(GameObject obj, int healthyNum, int infectedNum)
    {
        //Add default healthy bots
        for (int i = 0; i < healthyNum; i++)
        {
            //Instantiate the dummyBot with choosenRandom,Position and choosenRandom rotation inside of the Pandemic Area Object
            GameObject f = Instantiate(obj, ChooseRandomPosition(), Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f)), transform);
            f.GetComponent<SphereCollider>().radius = exposureRadius;
            f.GetComponent<DummyBot>().infectionCoeff = infectionCoeff;
            dummyBotList.Add(f);

        }
        //Add default starter infected bots
        for (int i = 0; i < infectedNum; i++)
        {
            //Instantiate the dummyBot with choosenRandom,Position and choosenRandom rotation inside of the Pandemic Area Object
            GameObject b = Instantiate(obj, ChooseRandomPosition(), Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f)), transform);
            b.GetComponent<DummyBot>().m_InfectionStatus = DummyBot.agentStatus.INFECTED;
            b.GetComponent<DummyBot>().changeAgentStatus();
            b.GetComponent<SphereCollider>().radius = exposureRadius;
            dummyBotList.Add(b);
        }
    }
    public Vector3 ChooseRandomPosition()
    {
        return new Vector3(Random.Range(-range, range), 1f,
                Random.Range(-range, range)) + transform.position;
    }


    public void ResetPandemicArea()
    {
        //Reset infectedCounter and healthyCounter
        infectedCounter = 0;
        healthyCounter = healthyBotCount + infectedBotCount; //Count all of them and infected ones will be removed from DummyBot.cs

        //If its first time then List should be empty, Check if it empty
        if (dummyBotList.Count == 0)
        {
            CreateObjectAtRandomPosition(dummyBot, healthyBotCount, infectedBotCount);
        }
        else
        {
            //Reset every dummyBot in the list
            for (int i = 0; i < dummyBotList.Count; i++)
            {

                if (i < healthyBotCount)
                {
                    dummyBotList[i].GetComponent<DummyBot>().m_InfectionStatus = DummyBot.agentStatus.HEALTHY;
                    dummyBotList[i].GetComponent<DummyBot>().changeAgentStatus();
                    dummyBotList[i].transform.position = ChooseRandomPosition();
                    dummyBotList[i].GetComponent<DummyBot>().nextActionTime = -1f;
                    dummyBotList[i].GetComponent<DummyBot>().recoverTime = recoverTime; //Reset the recoverTime also
                }
                else
                {
                    dummyBotList[i].GetComponent<DummyBot>().m_InfectionStatus = DummyBot.agentStatus.INFECTED;
                    dummyBotList[i].GetComponent<DummyBot>().changeAgentStatus();
                    dummyBotList[i].transform.position = ChooseRandomPosition();
                    dummyBotList[i].GetComponent<DummyBot>().nextActionTime = -1f;
                    dummyBotList[i].GetComponent<DummyBot>().recoverTime = recoverTime; //Reset the recoverTime also
                }

            }
        }

    }

    public void Awake()
    {
        ResetPandemicArea();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPandemicArea();
        }
    }

}