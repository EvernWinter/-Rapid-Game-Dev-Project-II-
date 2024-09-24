using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> specialBlocksList;
    [SerializeField] private List<GameObject> specialBlockSpawnerPosition;

    [SerializeField] private GameObject ordinaryBlock;
    [SerializeField] private int blockCount = 5;
    [SerializeField] private List<GameObject> ordinaryBlockSpawnerPosition;

    void Start()
    {
        for(int i = 0; i < specialBlocksList.Count; i++)
        {
            int random = Random.Range(0, specialBlockSpawnerPosition.Count);
            Instantiate(specialBlocksList[i], specialBlockSpawnerPosition[random].transform);
            specialBlockSpawnerPosition.RemoveAt(random);
        }

        for (int i = 0; i < blockCount; i++)
        {
            int random = Random.Range(0, ordinaryBlockSpawnerPosition.Count);
            Instantiate(ordinaryBlock, ordinaryBlockSpawnerPosition[random].transform);
            ordinaryBlockSpawnerPosition.RemoveAt(random);
        }
    }
}
