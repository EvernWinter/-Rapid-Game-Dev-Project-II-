using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private Coroutine _playerDeathCoroutine = null;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlayerHurtSound();
            //CheckPointManager.Instance.MovePlayerToCheckPoint();
            if (_playerDeathCoroutine == null)
            {
                _playerDeathCoroutine = StartCoroutine(TeleportPlayerToCheckpoint());
                PlayerAnimator.Instance.TriggerPlayerDeath();
            }

        }
    }

    private IEnumerator TeleportPlayerToCheckpoint()
    {
        yield return new WaitForSeconds(PlayerAnimator.Instance.deathDuration);
        CheckPointManager.Instance.MovePlayerToCheckPoint();
        yield return new WaitForSeconds(0.1f);
        PlayerAnimator.Instance.TriggerPlayerIdle();
        _playerDeathCoroutine = null;
    }
}
