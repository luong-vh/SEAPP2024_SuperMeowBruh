using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera mCam;
    private float mShakeTime = 0.2f;
    private float ShakeIntensity = 3f;
    private float timer;
    private void Awake()
    {
        mCam = GetComponent<CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StopShake();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) StopShake();
        }
    }
    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin mMultiChannelPerlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        mMultiChannelPerlin.m_AmplitudeGain = ShakeIntensity;
        timer = mShakeTime;
    }
    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin mMultiChannelPerlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        mMultiChannelPerlin.m_AmplitudeGain = 0f;
        timer = 0f;
    }
}
