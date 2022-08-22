using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] movementTransforms;
    [Header("Set Camera Smoothness")]
    [SerializeField] private float menuSmoothness;
    [SerializeField] private float preliminarySmoothness;
    [SerializeField] private float gameSmoothness;
    [Header("Set Difficulty Camera Rotation")]
    [SerializeField] private Vector3 easyCamRotation;
    [SerializeField] private Vector3 mediumCamRotation;
    [SerializeField] private Vector3 hardCamRotation;
    [Header("Set Difficulty Camera Position Offset")]
    [SerializeField] private Vector3 easyPlayerOffset;
    [SerializeField] private Vector3 mediumPlayerOffset;
    [SerializeField] private Vector3 hardPlayerOffset;
    [Header("Set Camera Difficulty Score")]
    [SerializeField] private float mediumScore;
    [SerializeField] private float hardScore;

    private GameObject player;
    private int currentTransform;
    private bool isReadyPosition;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER);
    }
    public enum MovementType
    {
        StartMovement,
        PrepareMovement,
        InGameMovemet
    }
    private MovementType movementType;
    private void Start()
    {
        movementType = MovementType.StartMovement;
        transform.rotation = Quaternion.Euler(easyCamRotation);
    }
    void LateUpdate()
    {
        switch (movementType)
        {
            case MovementType.StartMovement: MenuMovement();
                break;
            case MovementType.PrepareMovement: PrepareMovement();
                break;
            case MovementType.InGameMovemet: InGameMovement();
                break;
        }

        SetCameraPosition();
    }
    #region Movement Types
    public void MenuMovement()
    {
        transform.position = Vector3.Slerp(transform.position, movementTransforms[currentTransform].position, menuSmoothness * Time.deltaTime);
        transform.LookAt(player.transform.position);

        float distance = Vector3.Distance(transform.position, movementTransforms[currentTransform].position);
        float maxDistance = 1f;

        if(distance < maxDistance)
        {
            currentTransform++;

            if (currentTransform > movementTransforms.Length - 1)
                currentTransform = 0;
        }
    }
    public void PrepareMovement()
    {
        if (isReadyPosition == false)
        {
            GoGamePosition();
        }
        else
        {
            SetCamRotation();
        }
    }
    public void GoGamePosition()
    {
        transform.LookAt(player.transform.position);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + easyPlayerOffset, preliminarySmoothness * Time.deltaTime);
        float nearbyZ = -5.1f;

        if (transform.localPosition.z >= nearbyZ)
        {
            transform.position = player.transform.position + easyPlayerOffset;
            isReadyPosition = true;
        }
    }
    public void SetCamRotation()
    {
        Quaternion desiredCamRotation = Quaternion.Euler(easyCamRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredCamRotation, preliminarySmoothness * Time.deltaTime);
        float nearbyX = 35.2f;

        if (transform.localEulerAngles.x <= nearbyX)
        {
            transform.rotation = Quaternion.Euler(easyCamRotation);
            GameManager.Instance.currentState = GameManager.GameStates.InGame;
            movementType = MovementType.InGameMovemet;
        }
    }
    public enum SectorCameraPosition
    {
        Easy,
        Medium,
        Hard
    }

    public SectorCameraPosition sectorCameraPosition;
    public void InGameMovement()
    {
        switch(sectorCameraPosition)
        {
            case SectorCameraPosition.Easy: EasyCameraPosition();
                break;
            case SectorCameraPosition.Medium: MediumCameraPosition();
                break;
            case SectorCameraPosition.Hard: HardCameraPosition(); 
                break;
        }
    }
    #endregion

    #region DifficultyPositions
    public void EasyCameraPosition()
    {
        GameManager.Instance.currentState = GameManager.GameStates.InGame;

        transform.LookAt(null);
        transform.rotation = Quaternion.Euler(easyCamRotation);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + easyPlayerOffset, gameSmoothness * Time.deltaTime);
    }

    public void MediumCameraPosition()
    {
        transform.rotation = Quaternion.Euler(mediumCamRotation);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + mediumPlayerOffset, gameSmoothness * Time.deltaTime);
    }

    public void HardCameraPosition()
    {
        transform.rotation = Quaternion.Euler(hardCamRotation);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + hardPlayerOffset, gameSmoothness * Time.deltaTime);
    }
    #endregion

    public void SetCameraPosition()
    {
        if(ScoreManager.Instance.currentScore == mediumScore)
        {
            sectorCameraPosition = SectorCameraPosition.Medium;
            GameManager.Instance.modeText.text = "M E D I U M";
        }
        else if(ScoreManager.Instance.currentScore == hardScore)
        {
            sectorCameraPosition = SectorCameraPosition.Hard;
            GameManager.Instance.modeText.text = "H A R D";
        }
    }
    public void ClickStartButton()
    {
        isReadyPosition = false;
        movementType = MovementType.PrepareMovement;
    }


}
