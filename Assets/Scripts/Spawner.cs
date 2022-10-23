using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] TMP_InputField timeToSpawn;
    [SerializeField] TMP_InputField speedForCubes;
    [SerializeField] TMP_InputField distanceForCubes;
    [SerializeField] TMP_InputField countOfCubes;
    [SerializeField] Toggle spawnControl;

    int count = 0;
    float timer;

    private List<GameObject> runtimeCreatedObjects;

    private void Awake()
    {
        runtimeCreatedObjects = new List<GameObject>();
    }

    private void Start()
    {
        timeToSpawn.onValueChanged.AddListener(delegate { GetDelay(); });
        speedForCubes.onValueChanged.AddListener(delegate { GetSpeed(); });
        distanceForCubes.onValueChanged.AddListener(delegate { GetDistance(); });
    }
    void Update()
    {
        SpawnCube(cube);
        GameObject[] ttt = GameObject.FindGameObjectsWithTag("Cube");
        try
        {
            foreach (GameObject items in runtimeCreatedObjects)
            {
                if (items != null)
                {
                    GiveVelocity(items);
                }
                else continue;
            }
        }
        catch (InvalidOperationException)
        {
            Debug.Log("Ну пусто и пусто");
        }
    }

    public float GetDelay()
    {
        float.TryParse(timeToSpawn.text, out float result);
        if(result<0)
        {
            timeToSpawn.text = "0";
        }
        return result;
    }

    public float GetSpeed()
    {
        float.TryParse(speedForCubes.text, out float result);
        if(result<0)
        {
            timeToSpawn.text = "0";
        }
        return result;
    }

    public float GetDistance()
    {
        float.TryParse(distanceForCubes.text, out float result);
        if(result<0)
        {
            timeToSpawn.text = "0";
        }
        return result;
    }

    public void GiveVelocity(GameObject gameObject)
    {
        if (spawnControl.isOn)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, GiveDistance(), Time.deltaTime * GetSpeed());
            if (gameObject.transform.position == GiveDistance())
            {
                runtimeCreatedObjects.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    public Vector3 GiveDistance()
    {
        Vector3 distance = new Vector3(0, 0.5f, GetDistance());
        return distance;
    }

    public void SpawnCube(GameObject gameObject)
    {
        timer += Time.deltaTime;
        if (timer >= GetDelay())
        {
            if (spawnControl.isOn)
            {
                gameObject = Instantiate(cube, transform.position, transform.rotation);
                runtimeCreatedObjects.Add(gameObject);
                count++;
                countOfCubes.text = count.ToString();
            }
            timer -= GetDelay();
        }
    }
}


