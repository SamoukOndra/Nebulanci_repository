using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnSomething : MonoBehaviour
{
    public bool randomSpawn;
    private Vector3 position;

    [SerializeField] List <GameObject> somethings;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Spawn(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Spawn(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Spawn(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Spawn(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Spawn(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Spawn(5);
        }
    }


    private void Spawn(int index)
    {
        if (index >= somethings.Count) return;

        if (randomSpawn)
        {
            position = Util.GetRandomSpawnPosition();
        }
        else position = Vector3.zero;

        Instantiate(somethings[index], position, Quaternion.identity);
    }
}
