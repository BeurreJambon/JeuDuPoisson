using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipale : MonoBehaviour
{
    void Start()
    {
        //LancerMenuDeDebut();
    }
    public void PlayGame(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
        // SceneManager est une classe fournie par le namespace SceneManagement de Unity
    }

    public void QuitApp()
    {
        Application.Quit(); //Ne marche pas dans l'éditeur, ne marche que dans un build
        Debug.Log("Application has Quit");
    }

    /*public void LancerMenuDeDebut()
    {
            SceneManager.LoadScene("SceneUI");
    }*/
}
