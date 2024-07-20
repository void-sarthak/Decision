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
    public float minGap = 5.0f; // Minimum gap between trees

    private List<Vector3> treePositions = new List<Vector3>(); // List to store tree positions

    private void Start()
    {
        SpawnTrees();
    }

    private void SpawnTrees()
    {
        GameObject treesParent = new GameObject("Trees"); // Create an empty GameObject to parent the trees

        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector3 spawnPos = GetValidSpawnPosition();

            if (spawnPos != Vector3.zero) // Only spawn if a valid position is found
            {
                GameObject selectedTreePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)]; // Choose a random tree prefab

                GameObject tree = Instantiate(selectedTreePrefab, spawnPos, Quaternion.identity); // Spawn the tree at the selected position
                tree.transform.parent = treesParent.transform; // Set the tree's parent to the Trees GameObject

                treePositions.Add(spawnPos); // Add the new tree position to the list
            }
        }

        treesParent.transform.parent = road.transform; // Set the Trees GameObject as a child of the road
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int attempts = 0; attempts < 100; attempts++) // Try 100 times to find a valid position
        {
            Vector3 leftPos = leftRoadEdge.position;
            Vector3 rightPos = rightRoadEdge.position;

            Vector3 spawnPos = Vector3.Lerp(leftPos, rightPos, Random.value); // Random position between left and right road edges
            spawnPos += Vector3.up * yOffset; // Offset to lift the tree slightly above the ground

            bool validPosition = true;

            foreach (Vector3 treePos in treePositions)
            {
                if (Vector3.Distance(spawnPos, treePos) < minGap)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
            {
                return spawnPos;
            }
        }

        return Vector3.zero; // Return zero vector if no valid position is found
    }
}
