using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource gameplay;
    [SerializeField] AudioSource zombieFootstep;
    [SerializeField] AudioClip death;
    [SerializeField] bool playGameplayMusic;
    [SerializeField] bool playDeathSound;
    
    float gameplayVolume;

    private void Start() {
        GamePlayMusic();
        gameplayVolume = gameplay.volume;
    }

    public void GamePlayMusic()
    {
        playGameplayMusic = true;
        playDeathSound = false;
        UpdateSounds();
    }

    public void GameDeathMusic()
    {
        playDeathSound = true;
        playGameplayMusic = false;
        UpdateSounds();
    }

    public void ZombieIsComing(float distance)
    {
        if (playGameplayMusic) {
            // convert 0 - 3 to 0 - 1
            if (distance > 3) {
                zombieFootstep.volume = 0;
                gameplay.volume = gameplayVolume;
            }
            else {
                float volume = distance / 3;
                zombieFootstep.volume = 1 - volume;
                gameplay.volume = volume;
            }
        }
    }
    private void UpdateSounds() 
    {
        if (playDeathSound) {
            gameplay.volume = 0;
            zombieFootstep.volume = 0;
            gameplay.Stop();
            zombieFootstep.Stop();
            
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 0.75f);
        }
        else if (playGameplayMusic) {
            gameplay.Play();
        }
    }
}
