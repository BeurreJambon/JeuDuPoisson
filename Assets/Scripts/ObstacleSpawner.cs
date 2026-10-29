using UnityEngine;
using System.Collections.Generic;
public class ObstacleSpawner : MonoBehaviour
{
float cooldown  = 0.5f;
public List<Oiseau> listeDeObstacle = new List<Oiseau>();
public GameObject[] tableauObstacles;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            Oiseau nouveauOiseau = new Oiseau(this);

            //!! Comment utiliser "this" et a quoi il sert !!
            //1. this correspond a l'instance actuelle de la classe où ON SE TROUVE
            //Ex: je suis dans la classe Oiseau -> je suis l'instace actuelle de l'oiseau qui exécute une action...
            //Ex: je suis dans la classe Obstacle -> je suis l'instance actuelle de spawner et on peut m'utiliser comme référence avec une variable (ici parent).
            //Actuellement "this" est une référence à la classe ObstacleSpawner, d'ailleur on la stocke dans la variable parent (l.48) lors de l'instantiation.

            //Pourquoi il vaut mieux que chaque oiseau ait sa propre référence au spawner?
            //1. Si il y a plusieurs Object qui Contiennent le script ca peut créer un probème.
            //2.l'instance de Oiseau dépend d’un objet global
            //3. ...

            listeDeObstacle.Add(nouveauOiseau);
            cooldown = 1.0f;
        }

        for (int i = listeDeObstacle.Count -1; i>=0 ;i--)
        {
           listeDeObstacle[i].MiseAJourPosition();
           listeDeObstacle[i].DetruireNuage();
        }

        cooldown -= Time.deltaTime;
    }

    public class Oiseau //Il est important de mettre la classe public pour avoir accès en dehors
    {
        //public GameObject[] tableauObstacles;

        public GameObject instance;

        ObstacleSpawner referenceClasseObstacle;
        public float vitesse = Random.Range(7.0f,10.0f);
        public Oiseau(ObstacleSpawner parent) // Le constructeur n'a pas de type (int, string...) ni de void.
        {
            //  On a besoin de déclarer une variable pour stocker "parent" 
            //  (qui lui même contient la référence de "ObstacleSpawner"),
            //  car parent n'existe que dans le constructeur car il est une variable
            //  qui est déclarer dans le paramètre du constructeur.
            referenceClasseObstacle = parent; 
            instance = Instantiate(ChoisirOiseau(), ChoisirOiseauPosition(), Quaternion.identity);  
        }

        public GameObject ChoisirOiseau()
        {
            int index = Random.Range(0,referenceClasseObstacle.tableauObstacles.Length);
            GameObject oiseauChoisis = referenceClasseObstacle.tableauObstacles[index];
            if (oiseauChoisis.CompareTag("Aircraft"))
            {
                vitesse = 15f;
            }
            return oiseauChoisis;
        }

        public Vector2 ChoisirOiseauPosition()
        {
            Vector2 positionOiseau = new Vector2(15f, Random.Range(-5f,5f));
            return positionOiseau;
        }

        public Vector2 MiseAJourPosition()
        {
            return instance.transform.position +=  Vector3.left * vitesse * Time.deltaTime;
        }

        public void DetruireNuage()
        {
            if (instance.transform.position.x < -15.0f)
            {
                referenceClasseObstacle.listeDeObstacle.Remove(this);
                Destroy(instance);
            }
        }
    }
}
