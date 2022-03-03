using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class shakeVirtual : MonoBehaviour
{
    //public static CinemachineShake Instance { get; private set;}
    
	float shakeTimer;

    public CinemachineVirtualCamera cv;
	
	void Awake()
	{
        //Instance=this;
	}
	
	public void ShakeCamera(float intensity, float time)
	{
		CinemachineBasicMultiChannelPerlin cvBase= cv.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	
        cvBase.m_AmplitudeGain=intensity;
        shakeTimer=time;
    }

	void Update()
	{
		if (shakeTimer > 0)
		{
			shakeTimer-=Time.deltaTime;
            if(shakeTimer<=0f)
            {
                CinemachineBasicMultiChannelPerlin cvBase= cv.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cvBase.m_AmplitudeGain=0f;
            }
		}
	}
}
