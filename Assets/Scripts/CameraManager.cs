using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float HauteurScene;
    void Awake()
    {
        HauteurScene = Camera.main.orthographicSize *  2f;
    }
}
