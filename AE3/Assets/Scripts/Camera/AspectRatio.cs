using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    // This script was given by Salvatore.

    public float Width = 5.848888f;
    public float Height = 2.82f;

    void Start()
    {
        Camera.main.aspect = Width / Height;

    }
}
