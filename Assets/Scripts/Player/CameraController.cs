using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem SpeedupParticleSystem;
    [SerializeField] float MinFOV = 20f;
    [SerializeField] float MaxFOV = 120f;
    [SerializeField] float ZoomDuration = 1f;
    [SerializeField] float ZoomSpeedModifier = 5f;
    

    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float SpeedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(SpeedAmount));

        if(SpeedAmount > 0)
        {
            SpeedupParticleSystem.Play(); 
        }
    }

    IEnumerator ChangeFOVRoutine(float SpeedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float TargetFOV = Mathf.Clamp(startFOV + SpeedAmount * ZoomSpeedModifier,MinFOV,MaxFOV);
        
        float ElapsedTime = 0f;
        while (ElapsedTime < ZoomDuration)
        {
            float t = ElapsedTime/ZoomDuration;
            ElapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV,TargetFOV,t);
            yield return null;
        }
        
        cinemachineCamera.Lens.FieldOfView = TargetFOV;
    }
}
