using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject gemPrefab;
    public GameObject potionPrefab;
    [Header("Spawn Settings")]
    [SerializeField] private float firstArrowSpawnTime;
    [SerializeField] private float firstGemSpawnTime;
    [SerializeField] private float firstPotionSpawnTime;
    [SerializeField] private float spawnArrowRate;
    [SerializeField] private float spawnGemRate;
    [SerializeField] private float spawnPotionRate;
    [Header("Spawn Positions")]
    [SerializeField] private float spawnPositionZ;
    [SerializeField] private float minimumX;
    [SerializeField] private float maximumX;

    private GameObject player;
    private Vector3 spawnArrowPoint;
    private Vector3 spawnObjectPoint;
    private Vector3 spawnRotation;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER);
    }
    void Start()
    {
        InvokeRepeating(nameof(SpawnArrow), firstArrowSpawnTime, spawnArrowRate);
        InvokeRepeating(nameof(SpawnGem), firstGemSpawnTime, spawnGemRate);
        InvokeRepeating(nameof(SpawnPotion), firstPotionSpawnTime, spawnPotionRate);
    }
    public void SpawnArrow()
    {
        Instantiate(arrowPrefab, SpawnPositionArrow(), Quaternion.identity);
    }
    public void SpawnGem()
    {
        Instantiate(gemPrefab, SpawnPositionObject(), Quaternion.identity);
    }
    public void SpawnPotion()
    {
        Instantiate(potionPrefab, SpawnPositionObject(), Quaternion.identity);
    }

    private Vector3 SpawnPositionArrow()
    {
        float randomX = Random.Range(minimumX, maximumX);
        float spawnY = 1f;
        spawnArrowPoint = new Vector3(randomX, spawnY, player.transform.position.z + spawnPositionZ);

        return spawnArrowPoint;
    }

    private Vector3 SpawnPositionObject()
    {
        float randomX = Random.Range(minimumX, maximumX);
        float minimumZ = 0f;
        float maximumZ = 4f;
        float randomZ = Random.Range(minimumZ,maximumZ);

        float spawnY = 0.2f;
        spawnObjectPoint = new Vector3(randomX, spawnY, randomZ);
        return spawnObjectPoint;
    }

    public void StopAllSpawning()
    {
        CancelInvoke(nameof(SpawnArrow));
        CancelInvoke(nameof(SpawnGem));
        CancelInvoke(nameof(SpawnPotion));
    }

    
}
