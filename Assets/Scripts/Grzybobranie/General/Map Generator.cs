using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grzybobranie.General
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject treePrefab;
        [SerializeField] private int treeCount;
        [SerializeField] private Transform treeParent;
        private List<GameObject> generatedTrees;

        [SerializeField] private GameObject bushPrefab;
        [SerializeField] private int bushCount;
        [SerializeField] private Transform bushParent;
        private List<GameObject> generatedBushes;

        [SerializeField] private GameObject[] mushrooms;
        [SerializeField] private List<GameObject> mushroomsToGenerate;
        [SerializeField] private float mushroomSpawnRadius;
        [SerializeField] private Transform mushroomParent;

        [SerializeField] private Transform mushroomScale;
        [SerializeField] private float xBoundry;
        [SerializeField] private float yBoundry;
        [SerializeField] private Player.PlayerMovement playerMovement;

        [SerializeField] private UI.Objective objective;

        [SerializeField] private Sprite[] treeSprites;
        [SerializeField] private Sprite[] bushSprites;
        [SerializeField] private Sprite[] mushroomSprites;

        private Dictionary<int, List<GameObject>> spawnedMushrooms;
        void Start()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            ClearMap();
            generatedTrees = new List<GameObject>();
            generatedBushes = new List<GameObject>();
            spawnedMushrooms = new Dictionary<int, List<GameObject>>();
            for (int i = 0; i <= mushrooms.Length; i++)
            {
                spawnedMushrooms[i] = new List<GameObject>();
            }
            playerMovement.ResetPlayerPosition();
            GenerateTrees();
            GenerateAllMushrooms();
            GenerateBushes();
        }

        private void GenerateTrees()
        {
            for(int i = 0; i < treeCount; i++)
            {
                GameObject newTree = Instantiate(treePrefab, GenerateRandomUnoccupiedLocation(), Quaternion.identity, treeParent);
                generatedTrees.Add(newTree);
                newTree.GetComponent<SpriteRenderer>().sprite = treeSprites[UnityEngine.Random.Range(0,treeSprites.Length)];
            }
        }

        private void GenerateBushes()
        {
            for (int i = 0; i < bushCount; i++)
            {
                GameObject newBush = Instantiate(bushPrefab, GenerateRandomUnoccupiedLocation(), Quaternion.identity, bushParent);
                newBush.GetComponent<SpriteRenderer>().sprite = bushSprites[UnityEngine.Random.Range(0, bushSprites.Length)];
                newBush.GetComponent<Objects.Bush>().SetPlayerMovement(playerMovement);
                generatedBushes.Add(newBush);
            }
        }

        private void GenerateAllMushrooms()
        {
            for(int i = 0; i < mushrooms.Length; i++)
            {
                GenerateSpecificMushroom(i);
            }
        }

        public void GenerateSpecificMushroom(int mushroomIndex)
        {
            
            for(int i = 0; i < generatedTrees.Count; i++)
            {
                //int mushroomIndex = Random.Range(0, mushroomsToGenerate.Length);
                GameObject newMushroom = generatedTrees[i].GetComponent<Objects.Plant>().GenerateMushroomsUnderTree(mushrooms[mushroomIndex], mushroomSpawnRadius, mushroomParent, mushroomSprites[UnityEngine.Random.Range(0, mushroomSprites.Length)]);
                if(newMushroom != null)
                {
                    spawnedMushrooms[mushroomIndex].Add(newMushroom);
                    newMushroom.SetActive(false);
                }
            }
        }

        public void EnableSpecificMushroom(int mushroomIndex)
        {
            foreach(GameObject mushroom in spawnedMushrooms[mushroomIndex])
            {
                if(mushroom != null)
                {
                    mushroom.SetActive(true);
                }
            }
        }

        private bool isPointInCollider(Vector2 point)
        {
            Vector2 pos1 = new Vector2(point.x - mushroomScale.position.x / 2, point.y - mushroomScale.position.x / 2);
            Vector2 pos2 = new Vector2(point.x + mushroomScale.position.x / 2, point.y + mushroomScale.position.x / 2);
            Collider2D colliderHit = Physics2D.OverlapArea(pos1, pos2);

            if(colliderHit != null)
            {
                return true;
            }
            return false;
        }

        private Vector2 GenerateRandomUnoccupiedLocation()
        {
            int checker = 0;
            Vector2 location = new Vector2();
            do
            {
                location = new Vector2(UnityEngine.Random.Range(-xBoundry, xBoundry), UnityEngine.Random.Range(-yBoundry, yBoundry));
                checker++;
                if (checker > 100)
                {
                    Debug.Log("Nie znaleziono miejsca na spawn");
                    break;
                }
            }
            while (isPointInCollider(location));
            return location;
        }

        private void ClearMap()
        {
            if(generatedTrees != null)
            {
                for (int i = 0; i < generatedTrees.Count; i++)
                {
                    foreach (GameObject tree in generatedTrees)
                    {
                        Destroy(tree);
                    }
                }
            }
            if(generatedBushes != null)
            {
                for (int i = 0; i < generatedBushes.Count; i++)
                {
                    foreach (GameObject bush in generatedBushes)
                    {
                        Destroy(bush);
                    }
                }
            }
            
            if(spawnedMushrooms != null)
            {
                for (int i = 0; i < spawnedMushrooms.Count; i++)
                {
                    foreach (GameObject mushroom in spawnedMushrooms[i])
                    {
                        if (mushroom != null)
                        {
                            Destroy(mushroom.gameObject);
                        }
                    }
                }
            }
        }
    }

}