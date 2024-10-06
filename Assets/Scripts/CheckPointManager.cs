using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private Vector3 playerSpawnStart;
    [SerializeField] private Vector3[] playerCheckPoints;
    [SerializeField] public int currentCheckPointIndex;
    [SerializeField] private GameObject player;

    public static CheckPointManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerSpawnStart = playerCheckPoints[0];
    }

    public void MovePlayerToCheckPoint()
    {
        player.transform.position = playerCheckPoints[currentCheckPointIndex];
    }
}
