using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] backgroundMusicClips;
    private int _clipIndex;
    private AudioSource _audioSource;

    private void Awake() 
    {
        DontDestroyOnLoad(this);
        _audioSource = GetComponent<AudioSource>();
        _clipIndex = 0;
    }

    private void Start()
    {
        PlayNextTrack();
        StartCoroutine(WaitAndPlayTracks());
    }

    private IEnumerator WaitAndPlayTracks()
    {
        for(int i=0; i<backgroundMusicClips.Length; i++)
        {
            yield return new WaitForSeconds(_audioSource.clip.length);
            PlayNextTrack();
        }
        
    }

    private void PlayNextTrack()
    {
        _audioSource.clip = backgroundMusicClips[_clipIndex];
        _audioSource.Play();

        if((_clipIndex + 1) < backgroundMusicClips.Length)
            _clipIndex++;
        else
            _audioSource.loop = true;
    }
}
