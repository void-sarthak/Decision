using System.Collections;
using UnityEngine;

public class RoadSpwaner : MonoBehaviour
{
    public GameObject objectPrefab; // The object prefab to spawn
    public float spawnInterval = 1f; // Interval between spawns
    public Vector3 spawPositon = new Vector3(0f, 0f, 20f);

    private float timer = 0f;

    private void Start()
    {
        Instantiate(objectPrefab, spawPositon, Quaternion.identity);
    }

    private void Update()
    {
        if(timer > spawnInterval)
        {
            timer = 0f;
            Instantiate(objectPrefab, spawPositon, Quaternion.identity);
        }
        timer += Time.deltaTime;
    }


}
