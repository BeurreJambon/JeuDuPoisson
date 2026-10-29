using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    //public GameObject player;
    //Playercontroller refPlayercontroller;
    public TMP_Text textAfficher;
    float tempsAAfficherEnFloat;
    void Start()
    {
        //refPlayercontroller = player.GetComponent<Playercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        AfficherScore();
    }

     void AfficherScore()
    {
        tempsAAfficherEnFloat = Playercontroller.tempsFinal;
        textAfficher.text = $"temps final : {FormatTemps(tempsAAfficherEnFloat)}";
    }
    string FormatTemps(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60);
        int secondes = Mathf.FloorToInt(t % 60);
        return minutes.ToString("00") + ":" + secondes.ToString("00");
    }
}
