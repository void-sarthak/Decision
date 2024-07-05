using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject[] treePrefabs; // Array of tree prefabs to choose from
    public Transform leftRoadEdge;  // Transform of the left road edge
    public Transform rightRoadEdge; // Transform of the right road edge
    public int numberOfTrees = 10; // Number of trees to spawn
    public float yOffset = 0.5f; // Offset to lift the trees slightly above the ground
    public GameObject road; // Reference to the road object

    private void Start()
    {
        SpawnTrees();
    }

    private void SpawnTrees()
    {
        GameObject treesParent = new GameObject("Trees"); // Create an empty GameObject to parent the trees

        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector3 leftPos = leftRoadEdge.position;
            Vector3 rightPos = rightRoadEdge.position;

            Vector3 spawnPos = Vector3.Lerp(leftPos, rightPos, Random.value); // Random position between left and right road edges
            spawnPos += Vector3.up * yOffset; // Offset to lift the tree slightly above the ground

            GameObject selectedTreePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)]; // Choose a random tree prefab

            GameObject tree = Instantiate(selectedTreePrefab, spawnPos, Quaternion.identity); // Spawn the tree at the selected position
            tree.transform.parent = treesParent.transform; // Set the tree's parent to the Trees GameObject
        }

        treesParent.transform.parent = road.transform; // Set the Trees GameObject as a child of the road
    }
}
