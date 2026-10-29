using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class VieManager : MonoBehaviour
{
    public List<GameObject> coeurs = new List<GameObject>();
    public GameObject PrefabCoeur;
    public Transform heartsContainer;
    void Start()
    {
        //for(int i = 0; i<5; i++)
        //{
            AjusterNombreDeCoeur(5);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    public void AjusterNombreDeCoeur(int nombreDeCoeurGagneOuPerdu)
    {
        if (nombreDeCoeurGagneOuPerdu > 0)
        {
            for (int x = 0; x < nombreDeCoeurGagneOuPerdu; x++)
            {
                GameObject nouveauCoeur = Instantiate(PrefabCoeur, heartsContainer);
                //heartsContainer est de Type Transform et ce n'est pas grave même si c'est pas un rectTransform, car ici on juste besoin d'un parent pour que le coeur sache où se positionner
                // Transform à la propriété de conaitre son parent et la liste de ses enfants.
                //Dans notre scipt on a besoin d'un parent qui puisse contenir des enfant, on crée donc une variable public qui fait référence a cette objet.
                coeurs.Add(nouveauCoeur);
                MettreAJourPositions();
            }
        }

        if (nombreDeCoeurGagneOuPerdu < 0)
        {
            for (int y=0; y>nombreDeCoeurGagneOuPerdu; y--)
            {
                Destroy(coeurs[coeurs.Count-1]);
                coeurs.Remove(coeurs[coeurs.Count-1]);
            }
        }
    }


    void MettreAJourPositions()
    {
        for (int i = 0; i < coeurs.Count; i++) //On parcourt la liste de coeurs
        {
            // On accède au composant RectTransform de notre object pour avoir accès à la position
            // On stocke le composant (la position) dans une variable 
            RectTransform rt = coeurs[i].GetComponent<RectTransform>(); 

            // anchoredPosition -> position UI par apport à l'ancrage du parent (ici notre heartContainer)
            // On l'espace de la longeur de notre coeur
            rt.anchoredPosition = new Vector2(i * 18f, 0);
        }
    }
    
}
