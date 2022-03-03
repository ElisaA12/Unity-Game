using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

[RequireComponent(typeof(CinemachineFreeLook))]
public class SimpleCameraShake : MonoBehaviour {

    
    CinemachineFreeLook cmFreeCam;
    public float amplitudeGain; public float frequemcyGain;
    public float shakeDuration;

    void Start(){
        cmFreeCam=GetComponent<CinemachineFreeLook>();

    }

       public void DoShake()
 {
     StartCoroutine(Shake());
 }
 public IEnumerator Shake()
 {
     Noise2(amplitudeGain, frequemcyGain);
     yield return new WaitForSeconds(shakeDuration);
     Noise2(0,0);
 }
 void Noise2(float amplitude,float frequency)
 {
     cmFreeCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
     cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
     cmFreeCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
     cmFreeCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
     cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
     cmFreeCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
 }

   
}