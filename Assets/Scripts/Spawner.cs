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
    [SerializeField] Scrollbar delayBar;
    [SerializeField] Scrollbar speedBar;
    [SerializeField] Scrollbar distanceBar;

    bool spawn = true;
    int count = 0;
    public float interval = 5;
    float timer;

    private void Start()
    {
        timeToSpawn.onValueChanged.AddListener(delegate { GetDelay(); });
        speedForCubes.onValueChanged.AddListener(delegate { GetSpeed(); });
        distanceForCubes.onValueChanged.AddListener(delegate { GetDistance(); });
    }
    void Update()
    {
        SpawnCube(cube);
        //GiveVelocity(cube);
    }

    public float GetDelay()
    {
        float.TryParse(timeToSpawn.text, out float result);
        //Debug.Log(result);
        return result;
    }

    public float GetSpeed()
    {
        float.TryParse(speedForCubes.text, out float result);
        //Debug.Log(result);
        return result;
    }

    public float GetDistance()
    {
        float.TryParse(distanceForCubes.text, out float result);
        //Debug.Log(result);
        return result;
    }

    public void GiveVelocity(GameObject gameObject)
    {
        if (spawnControl.isOn)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, GiveDistance(), Time.deltaTime * GetSpeed());
        }
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*GetSpeed());
    }

    public Vector3 GiveDistance()
    {
        Vector3 distance = new Vector3(0, 0, GetDistance());
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
                count++;
                countOfCubes.text = count.ToString();
            }
            timer -= GetDelay();
        }
    }
}


