using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public int NumOfFruit;
    public GameObject FruitPrefab;
    public GameObject[] FruitSpawnLocation = new GameObject[5];
    public Transform[] SpawnPlace = new Transform[5];

    private void Start()
    {
        for (int i = 0; i < FruitSpawnLocation.Length-1; i++)
        {
            SpawnPlace[i] = FruitSpawnLocation[i].transform;
        }
    }

    public void BreakChest()
    {
        for (int i = 0; i < NumOfFruit; i++)
        {
            Instantiate(FruitPrefab, SpawnPlace[i].position, transform.rotation);
        }
        this.gameObject.SetActive(false);
    }
}
