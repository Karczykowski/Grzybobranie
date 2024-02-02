using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grzybobranie.General
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject treePrefab;
        [SerializeField] private int treeCount;
        [SerializeField] private Transform treeParent;

        [SerializeField] private GameObject bushPrefab;
        [SerializeField] private int bushCount;
        [SerializeField] private Transform bushParent;

        [SerializeField] private GameObject[] mushrooms;
        [SerializeField] private float mushroomSpawnRadius;
        [SerializeField] private Transform mushroomParent;

        [SerializeField] private Transform mushroomScale;
        [SerializeField] private float xBoundry;
        [SerializeField] private float yBoundry;
        [SerializeField] private Player.PlayerMovement playerMovement;

        void Start()
        {
            GenerateTrees();
            GenerateBushes();
        }

        private void GenerateTrees()
        {
            for(int i = 0; i < treeCount; i++)
            {
                GameObject newTree = Instantiate(treePrefab, GenerateRandomUnoccupiedLocation(), Quaternion.identity, treeParent);
                GenerateMushrooms(newTree);
            }
        }

        private void GenerateBushes()
        {
            for (int i = 0; i < bushCount; i++)
            {
                GameObject newBush = Instantiate(bushPrefab, GenerateRandomUnoccupiedLocation(), Quaternion.identity, bushParent);
                newBush.GetComponent<Objects.Bush>().SetPlayerMovement(playerMovement);
            }
        }

        private void GenerateMushrooms(GameObject tree)
        {
            int mushroomIndex = Random.Range(0, mushrooms.Length);
            tree.GetComponent<Objects.Tree>().GenerateMushroomsUnderTree(mushrooms[mushroomIndex], mushroomSpawnRadius, mushroomParent);
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
                location = new Vector2(Random.Range(-xBoundry, xBoundry), Random.Range(-yBoundry, yBoundry));
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
    }

}