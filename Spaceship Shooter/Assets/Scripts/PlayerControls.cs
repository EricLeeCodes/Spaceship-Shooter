using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    //New way ***
    //[SerializeField] InputAction movement;


    [Header("General Setup Settings")]

    [Tooltip("How fast ship moves up and down based on player input")] 
    [SerializeField] float movementSpeed = 30f;

    [Tooltip("How far horizontal movement goes")]
    [SerializeField] float xRange = 10f;

    [Tooltip("How far vertical movement goes")]
    [SerializeField] float yRange = 7f;

    [Header("Screen Position Based Tuning")]
    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float controlPitchFactor = -2f;

    [Header("Player input based tuning")]
    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float controlRollFactor = -20f;


    [Header("Laser gun array")]
    [Tooltip("Add all player laser here")]
    [SerializeField] GameObject[] lasers;

    float xThrow;
    float yThrow;

    //New way ***
    //private void OnEnable()
    //{
    //    movement.Enable();
    //}

    //private void OnDisable()
    //{
    //    movement.Disable();
    //}


    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }



    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }


    private void ProcessTranslation()
    {
        //New way ***
        //xThrow = movement.ReadValue<Vector2>().x;
        //yThrow = movement.ReadValue<Vector2>().y;


        //Old way
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        //movement going right
        float xOffset = xThrow * Time.deltaTime * movementSpeed;
        float rawXPosition = transform.localPosition.x + xOffset;
        float clampedXPosition = Mathf.Clamp(rawXPosition, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * movementSpeed;
        float rawYPosition = transform.localPosition.y + yOffset;
        float clampedYPosition = Mathf.Clamp(rawYPosition, -yRange, yRange);


        transform.localPosition = new Vector3(
            clampedXPosition,
            clampedYPosition,
            transform.localPosition.z);


       
    }


    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float rollDueToControlThrow = xThrow * controlRollFactor;


        float pitch = pitchDueToPosition * pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
