using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 3f;
    public float jump = 7f;
    public float sprintTreshold = 10f;

    [HideInInspector]
    public float minVolume, maxVolume;

    [HideInInspector]
    public float stepDistance;

    [SerializeField]
    private AudioClip[] footSteps;

    [SerializeField]
    private Image staminaStats;

    private AudioSource footStepsSound;
    private Transform cameraTransform;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;
    private bool isCrouching;
    private CharacterController characterController;
    private Vector3 moveDir;
    private float gravity = 20f;
    private float verticalVelocity;
    private float accumulatedDistance;
    private float sprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float walkVolumeMin = 0.2f;
    private float walkVolumeMax = 0.6f;
    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;
    private float sprintValue = 100f;

    private void Awake()
    {
        cameraTransform = transform.GetChild(0);
        characterController = GetComponent<CharacterController>();
        footStepsSound = GetComponent<AudioSource>();

    }

    private void Start()
    {
        minVolume = walkVolumeMin;
        maxVolume = walkVolumeMax;
        stepDistance = walkStepDistance;
    }

    private void Update()
    {
        Sprint();
        MovePlayer();
        Crouch();
        BoolWalkSound();
        QuitToMenu();
    }

    void MovePlayer()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        moveDir = transform.TransformDirection(moveDir);
        moveDir *= speed * Time.deltaTime;

        ApplyGravity();

        characterController.Move(moveDir);
    }

    void ApplyGravity()
    {

        verticalVelocity -= gravity * Time.deltaTime;

        PlayerJump();

        moveDir.y = verticalVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jump;
        }
    }

    void Sprint()
    {
        if(sprintValue > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                speed = sprintSpeed;

                stepDistance = sprintStepDistance;
                minVolume = sprintVolume;
                maxVolume = sprintVolume;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            speed = 5f;

            stepDistance = walkStepDistance;
            minVolume = walkVolumeMin;
            maxVolume = walkVolumeMax;
        }   

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprintValue -= sprintTreshold * Time.deltaTime;

            if (sprintValue <= 0f)
            {
                sprintValue = 0f;
                speed = 5f;
                stepDistance = walkStepDistance;
                minVolume = walkVolumeMin;
                maxVolume = walkVolumeMax;
            }
            DisplayStaminaStats(sprintValue);
        }
        else
        {
            if (sprintValue != 100f)
            {
                sprintValue += (sprintTreshold / 2f) * Time.deltaTime;

                DisplayStaminaStats(sprintValue);

                if (sprintValue > 100f)
                {
                    sprintValue = 100f;
                }
            }
        }
    }       

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                cameraTransform.localPosition = new Vector3(0f, standHeight, 0f);
                speed = 5f;

                stepDistance = walkStepDistance;
                minVolume = walkVolumeMin;
                maxVolume = walkVolumeMax;

                isCrouching = false;
            }
            else
            {
                cameraTransform.localPosition = new Vector3(0f, crouchHeight, 0f);
                speed = crouchSpeed;

                stepDistance = crouchStepDistance;
                minVolume = crouchVolume;
                maxVolume = crouchVolume;

                isCrouching = true;
            }
        }
    }

    void BoolWalkSound()
    {
        if (!characterController.isGrounded)
            return;

        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;

            if (accumulatedDistance > stepDistance)
            {
                footStepsSound.volume = Random.Range(minVolume, maxVolume);
                footStepsSound.clip = footSteps[Random.Range(0, footSteps.Length)];
                footStepsSound.Play();

                accumulatedDistance = 0f;
            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
    }

    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100;

        staminaStats.fillAmount = staminaValue;
    }

    public void QuitToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }
}
