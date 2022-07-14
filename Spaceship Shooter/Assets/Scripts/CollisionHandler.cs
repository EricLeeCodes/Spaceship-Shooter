using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float deathDelay = 2f;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] MeshRenderer[] renderers = null;


    void DeactivateRenderers()
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashParticle.Play();
        DeactivateRenderers();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", deathDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
