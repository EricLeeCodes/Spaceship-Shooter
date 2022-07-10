using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float deathDelay = 2f;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] MeshRenderer[] renderers = null;

    bool isTransitioning;
    bool collisionDisabled;


    void DeactivateRenderers()
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
             
             break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
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
