using System;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject homeFrog;

    private void OnEnable()
    {
        homeFrog.SetActive(true);
    }
    
    private void OnDisable()
    {
        homeFrog.SetActive(false);
    }
}
