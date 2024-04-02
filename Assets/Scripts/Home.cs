using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject homeFrog;

    private void OnEnable()
    {
        homeFrog.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            homeFrog.SetActive(true);
    }
    
    private void OnDisable()
    {
        homeFrog.SetActive(false);
    }
}
