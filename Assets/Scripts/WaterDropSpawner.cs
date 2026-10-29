using UnityEngine;
using System.Collections.Generic;
using System.Security.AccessControl;

public class WaterDropSpawner : MonoBehaviour
{
    public GameObject prefabWaterDrop;
    public float cooldown;
    public float tempsCooldown;
    public float vitesse;
    public List<WaterDrop> waterDrops = new List<WaterDrop>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            cooldown = tempsCooldown;
            WaterDrop nouvelleGoute = new WaterDrop(this);
            waterDrops.Add(nouvelleGoute);
        }
        cooldown -= Time.deltaTime;

        for (int i = 0; i < waterDrops.Count ; i++)
        {
            waterDrops[i].MiseAJourPosition();
            waterDrops[i].DestruireWaterDrop();
        }
    }

    public class WaterDrop
    {
        WaterDropSpawner refWaterDropSpawner;
        public GameObject instance;
        public WaterDrop(WaterDropSpawner parent)
        {
            refWaterDropSpawner = parent; // Voir ObstacleSpawner pour presque tout comprendre
            instance = Instantiate(refWaterDropSpawner.prefabWaterDrop, DeterminerPositionWaterDrop() ,Quaternion.identity);
        }
        public Vector2 DeterminerPositionWaterDrop()
        {
            Vector2 positionWaterDrop = new Vector2(Random.Range(1f,5f), 6f);
            return positionWaterDrop;
        }

        public void MiseAJourPosition()
        {
            instance.transform.position += new Vector3(-1f ,-2f, 0) * refWaterDropSpawner.vitesse * Time.deltaTime;
        }

        public void DestruireWaterDrop()
        {
            if (instance.transform.position.y < -6)
            {
                Destroy(instance);
                refWaterDropSpawner.waterDrops.Remove(this);
                // ici this ce trouve dans la classe WaterDrop, donc this est l'instance actuelle de WaterDrop
            }
        }
    }


}
