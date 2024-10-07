using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource useStaffFireSound;
    [SerializeField] private AudioSource useStaffMoveItemSound;
    [SerializeField] private AudioSource lanternSound;

    [SerializeField] private AudioSource fireImpactSound;

    [SerializeField] private AudioSource player_JumpSound;
    [SerializeField] private AudioSource player_FootStepSound;
    [SerializeField] private AudioSource player_HurtSound;
    [SerializeField] private AudioClip[] player_HurtSoundClips;

    [SerializeField] private AudioSource clickingUISound;
    [SerializeField] private AudioSource collectKeyItemSound;

    [SerializeField] private AudioSource gameWinSound;
    [SerializeField] private AudioSource gameOverSound;

    private bool isFootStepSoundAlreadyPlaying = false;
    private bool isGameWinSoundPlayOnce = false;
    private bool isGameOverSoundPlayOnce = false;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerHurtSound()
    {
        player_HurtSound.clip = player_HurtSoundClips[Random.Range(0, player_HurtSoundClips.Length)];
        PlaySound(player_HurtSound);
    }

    public void PlayerJumpSound()
    {
        PlaySound(player_JumpSound);
    }

    public void ClickingUISound()
    {
        PlaySound(clickingUISound);
    }

    public void CollectKeyItemSound()
    {
        PlaySound(collectKeyItemSound);
    }

    public void UseStaffFireSound()
    {
        PlaySound(useStaffFireSound);
    }
    public void UseStaffMoveItemSound()
    {
        PlaySound(useStaffMoveItemSound);
    }
    public void FireImpactSound()
    {
        PlaySound(fireImpactSound);
    }

    public void LanternSound()
    {
        PlaySound(lanternSound);
    }

    public void GameWinSound()
    {
        if (!isGameWinSoundPlayOnce)
        {
            isGameWinSoundPlayOnce = true;
            PlaySound(gameWinSound);
        }
    }
    public void GameOverSound()
    {
        if (!isGameOverSoundPlayOnce)
        {
            isGameOverSoundPlayOnce = true;
            PlaySound(gameOverSound);
        }
    }

    public void PlayerFootStepSound(bool isPlay)
    {
        if (isPlay && !isFootStepSoundAlreadyPlaying)
        {
            isFootStepSoundAlreadyPlaying = true;
            player_FootStepSound.Play();
        }
        else if (!isPlay && isFootStepSoundAlreadyPlaying)
        {
            isFootStepSoundAlreadyPlaying = false;
            player_FootStepSound.Stop();
        }
    }

    private void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }
}
