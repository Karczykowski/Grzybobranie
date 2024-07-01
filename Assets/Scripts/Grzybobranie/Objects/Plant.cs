using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grzybobranie.Objects
{
    public class Plant : MonoBehaviour
    {
        public void GenerateMushroomsUnderTree(GameObject mushroom, float spawnRadius, Transform mushroomParent, Sprite mushroomSprite)
        {
            int mushroomAmount = Random.Range(0, 2);
            for (int i = 0; i < mushroomAmount; i++)
            {
                float randomAngle = Random.Range(0f, Mathf.PI * 2f);
                spawnRadius = spawnRadius * Random.Range(0.9f, 1.3f);

                float spawnX = transform.position.x + Mathf.Cos(randomAngle) * spawnRadius;
                float spawnY = transform.position.y + Mathf.Sin(randomAngle) * spawnRadius;

                Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

                Debug.Log("Spawned Mushroom");
                GameObject newMushroom = Instantiate(mushroom, spawnPosition, Quaternion.identity, mushroomParent);
                newMushroom.GetComponent<SpriteRenderer>().sprite = mushroomSprite;
            }
        }
    }

}
