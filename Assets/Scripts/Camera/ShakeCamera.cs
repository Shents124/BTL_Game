using UnityEngine;
using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer;

    // Start is called before the first frame update
    private void Awake() {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    private void Update() {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <=0)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void Shake(float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1f;
        shakeTimer = time;
    }
}
