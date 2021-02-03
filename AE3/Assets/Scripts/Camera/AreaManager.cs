using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [Header("Area 1 Settings")]
    [SerializeField] private GameObject Area1;

    [Header("Area 2 Settings")]
    [SerializeField] private GameObject Area2;

    private GameObject PreviousArea;

    public void EnableNextArea()
    {
        if (Area1.activeInHierarchy)
        {
            Area2.SetActive(true);
            PreviousArea = Area1;
        }
        else
        {
            Area1.SetActive(true);
            PreviousArea = Area2;
        }
    }

    public void DisablePreviousArea()
    {
        if (PreviousArea.activeInHierarchy)
        {
            PreviousArea.SetActive(false);
        }
    }

}
