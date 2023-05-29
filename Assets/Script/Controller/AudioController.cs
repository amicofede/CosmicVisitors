using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource[] Sources;

    public AudioClip OnVisitorKilled;
    public AudioClip BossLaserPhaseTwo;
    public AudioClip BossSolarBeamCharge;
    public AudioClip BossSolarBeamShoot;

    public AudioClip SpaceshipShoot;

    #region UnityMessages
    private void Awake()
    {
        Sources = GetComponents<AudioSource>();
    }

    private void OnEnable()
    {
        EventController.VisitorKilled += PlayClipVisitorKilled;
        EventController.BossLaserPhaseTwo += PlayClipBossLaserPhaseTwo;
        EventController.BossSolarBeamCharge += PlayClipBossSolarBeamCharge;
        EventController.BossSolarBeamShoot += PlayClipBossSolarBeamShoot;

        EventController.SpaceshipShooted += PlayClipSpaceshipShoot;
    }

    private void OnDisable()
    {
        EventController.VisitorKilled -= PlayClipVisitorKilled;
        EventController.BossLaserPhaseTwo -= PlayClipBossLaserPhaseTwo;
        EventController.BossSolarBeamCharge -= PlayClipBossSolarBeamCharge;
        EventController.BossSolarBeamShoot -= PlayClipBossSolarBeamShoot;

        EventController.SpaceshipShooted -= PlayClipSpaceshipShoot;
    }
    #endregion

    private void PlayClipAudio(AudioClip _clip)
    {
        for (int i = 0; i < Sources.Length; i++)
        {
            if (!Sources[i].isPlaying)
            {
                Sources[i].clip = _clip;
                Sources[i].Play();
                break;
            }
        }
    }

    private void PlayClipVisitorKilled()
    {
        PlayClipAudio(OnVisitorKilled);
    }
    private void PlayClipBossLaserPhaseTwo()
    {
        PlayClipAudio(BossLaserPhaseTwo);
    }
    private void PlayClipBossSolarBeamCharge()
    {
        PlayClipAudio(BossSolarBeamCharge);
    }
    private void PlayClipBossSolarBeamShoot()
    {
        PlayClipAudio(BossSolarBeamShoot);
    }
    private void PlayClipSpaceshipShoot()
    {
        PlayClipAudio(SpaceshipShoot);
    }
}
