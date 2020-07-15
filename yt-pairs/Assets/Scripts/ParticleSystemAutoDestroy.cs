using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private ParticleSystem ps;
    private void Awake()
    {
        if (!TryGetComponent<ParticleSystem>(out ps))
            Destroy(this);
    }

    private void Start()
    {
        Destroy(gameObject, ps.main.duration);      
    }
}



