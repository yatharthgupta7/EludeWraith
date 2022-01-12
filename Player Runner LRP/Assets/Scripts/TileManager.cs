using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] GameObject[] tilePrefabs;
    //[SerializeField] GameObject coinPrefab;
    //GameObject[] coinSpawnPoints;

    Transform playerTransform;
    [SerializeField] Transform[] lookAtAr;
    int index;
    float spawnZ = -10.0f;
    float tileLength = 10.0f;
    int amnTilesOnScreen = 7;
    float safeZone = 15.0f;
    int lastPrefabIndex = 0;

    List<GameObject> activeTiles;
    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        activeTiles = new List<GameObject>();
        playerTransform = lookAtAr[index].transform;
        //coinSpawnPoints = GameObject.FindGameObjectsWithTag("CoinPosition");
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int x=0;x<amnTilesOnScreen;x++)
        {
            if(x<=2)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z-safeZone>(spawnZ-amnTilesOnScreen*tileLength))
        {
            SpawnTile();
            //SpawnCoins();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex=-1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go= Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
            /*for (int x = 0; x < coinSpawnPoints.Length; x++)
            {
                GameObject coin=Instantiate(coinPrefab, coinSpawnPoints[x].transform) as GameObject;
                coin.transform.SetParent(go.transform);
            }*/
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    /*void SpawnCoins()
    {
        int coinsTOSpawn = 10;// Random.Range(0, 10);
        for(int x=0;x<coinsTOSpawn; x++)
        {
            GameObject temp = Instantiate(coinPrefab);
            temp.transform.position = GetRandomSpawnPoint(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomSpawnPoint(Collider collider)
    {
        Vector3 point = new Vector3(
            (Random.Range(collider.bounds.min.x, collider.bounds.max.x)),
            (Random.Range(collider.bounds.min.y, collider.bounds.max.y)),
            (Random.Range(collider.bounds.min.z, collider.bounds.max.z)));
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomSpawnPoint(collider);
        }

        point.y = 1;
        return point;
    }*/

}
