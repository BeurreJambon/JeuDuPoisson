using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Playercontroller : MonoBehaviour
{
    //Autre
    public InputAction jumpPoisson;
    private Rigidbody2D rb;
    public int vieRestante;
    float cooldownLimiteHauteur;
    float cooldownObstacle;
    float cooldownAirCraft;
    public static float tempsFinal;

    //Varaiables qui permettent de gérer la vitesse du joueur
    public float vitesseSaut;

    // Variables qui permettent de gérer le saut
    float espacePresse;

    // Référence 

    public GameObject referenceVieManager;
    public GameObject referenceWaterDrop;
    public GameObject camera;
    public GameObject timer; 
    VieManager refVieManager;
    WaterDropSpawner refWaterDrop;
    CameraManager refCameraManager;
    Timer referenceTimer;

    // Vector2 deplacementDroite = new Vector2(1,0); -> pour assigner un vecteur a la main il faut mettre le mot clé new (comme exemple), pas besoin pour Vector2.right par exemple.

    void Start()
    {
        // On commence à vérifier si une touche est pressée
        jumpPoisson.Enable();

        rb = GetComponent<Rigidbody2D>();
        // On créée une référence pour pouvoir accéder au composant rigidbody de notre gameObjet.

        // IMPORTANT! Il faut le faire de cette manière on ne peut pas déclarer une variable puis lui assigner le composant Rigidbody parce que Unity créée lui aussi une variable du même nom et ça créée une erreur pour le compilateur qui ne comprend pas de quelle variable on parle.
        // + La nouvelle version de Unity ne permet plus de faire:
        //rigidbody2D.velocity
        //-> (confusion) parce que on a l'impression que rigidbody et un champs(une variable), alors qu'en réalité il fait juste un GetComponent, il ne stocke donc aucune valeur
        //->On doit toujours créer une nouvelle référence se qui utilise des capacités de calculs.

        refVieManager = referenceVieManager.GetComponent<VieManager>();
        refWaterDrop = referenceWaterDrop.GetComponent<WaterDropSpawner>();
        refCameraManager = camera.GetComponent<CameraManager>();
        referenceTimer = timer.GetComponent<Timer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // On détermine la valeur pour la direction. Cette valeur s'écrit sous la forme d'un Vector2 ((0.00, 1.00) -> aller à droite).
        espacePresse = jumpPoisson.ReadValue<float>();
        GererSaut();
        VerifierSiObjetDepasseScene();
        cooldownLimiteHauteur -= Time.deltaTime;
        cooldownObstacle -= Time.deltaTime;
        cooldownAirCraft -= Time.deltaTime;
        Debug.Log(vieRestante);
    }

    void GererSaut()
    {
        if (jumpPoisson.triggered) // Si on appuie avec 1 click
        {
            rb.linearVelocity = new Vector2(0,0); // Permet de remettre la Vitesse à 0 pour ne pas additionner les forces
            rb.AddForce(Vector2.up * vitesseSaut, ForceMode2D.Impulse);
            
        }
    }
     void VerifierSiObjetDepasseScene()
    { 
       if (rb.position.y > refCameraManager.HauteurScene/2f || rb.position.y < -(refCameraManager.HauteurScene/2f) )
        {
            if (cooldownLimiteHauteur <= 0)
            {
                vieRestante -= 1;
                refVieManager.AjusterNombreDeCoeur(-1);
                cooldownLimiteHauteur = 2;
            }

            if (vieRestante <= 0)
            {
                tempsFinal = referenceTimer.temps;
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) // other = l'object qui est détecter par le joueur
    {
        if (other.CompareTag("Obstacle"))
        {
            if (cooldownObstacle <= 0)
            {
            vieRestante -= 1;
            refVieManager.AjusterNombreDeCoeur(-1);
            cooldownObstacle = 1;
            }
            if (vieRestante<=0)
            {
                tempsFinal = referenceTimer.temps;
                SceneManager.LoadScene("GameOver");
            }
        }

        if (other.CompareTag("Aircraft"))
        {

            if(vieRestante > 2 && cooldownAirCraft <= 0)
            {

                refVieManager.AjusterNombreDeCoeur(-2);
            }

            if (cooldownAirCraft <= 0)
            {
                vieRestante -= 2;
                cooldownAirCraft = 1;
            }

            if (vieRestante <= 0)
            {
                tempsFinal = referenceTimer.temps;
                SceneManager.LoadScene("GameOver");
            }


        }

        if (other.CompareTag("WaterDrop"))
        {
            
            if (vieRestante < 5)
            {    
                vieRestante += 1;
                Debug.Log(125);
                refVieManager.AjusterNombreDeCoeur(1);
            }

            if (refWaterDrop.waterDrops.Count > 0)
            {
                refWaterDrop.waterDrops.RemoveAt(refWaterDrop.waterDrops.Count - 1);
                Destroy(other.gameObject);
            }
        }

        
    }

}