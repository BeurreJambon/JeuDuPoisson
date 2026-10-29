using System.ComponentModel;
//using System.Diagnostics;
using System.Collections.Generic;
//using System.Numerics;
using System.Security.Cryptography.X509Certificates;

//using System.Numerics;
using Microsoft.VisualBasic;
using UnityEngine;
//using System.Numerics;
//using System.Diagnostics;

public class NuageSpawner : MonoBehaviour
{
    public GameObject[] tableauNuages;
    public GameObject nuageChoisis;
    float cooldown;
    private List<Nuage> nuages = new List<Nuage>(); //Les listes peuvent changer de taille contrairement aux tableaux.
    public Vector2 positionNuage;

    void Start()
    {
        
    }
    
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            ChoisirObjet();
            ChoisirObjetPosition();
            Nuage nouveauNuages = new Nuage(nuageChoisis, positionNuage, this /* this-> référence à la classe */  );
            nuages.Add(nouveauNuages);
            cooldown = Random.Range(3.0f, 7.0f);
        }
        
        for (int i = nuages.Count -1; i>=0 ; i--)
        {
            nuages[i].MiseAJourPosition();
            nuages[i].DetruireNuage();
        }
    }

    public void ChoisirObjet()
    {
        int index = Random.Range(0, tableauNuages.Length);
        nuageChoisis = tableauNuages[index];
    }

    public void ChoisirObjetPosition()
    {
        positionNuage = new Vector2( 20.0f, Random.Range(-5f, 5f));
    }

    public class Nuage
    {
        public float vitesse;
        GameObject instance;
        NuageSpawner refNuageSpawner;
       
       //le constructeur s'exécute dés que la classe est appelée
        public Nuage(GameObject prefab, Vector2 position, NuageSpawner spawnerNuage)
        {
            instance = Instantiate(prefab, position, Quaternion.identity);
            refNuageSpawner = spawnerNuage;
            vitesse = Random.Range(4f,6f);
        }

        public void MiseAJourPosition()
        {
            instance.transform.position += Vector3.left * vitesse * Time.deltaTime;
        } 

        public void DetruireNuage()
        {
            if (instance.transform.position.x < -15)
            {
                refNuageSpawner.nuages.Remove(this);
                                
                Object.Destroy(instance);
                // Destroy ne détruie que les games objects, c'est pourquoi on utilise instance et pas qqc de type Nuage
            }
        }
    }
}
