using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundDuke : MonoBehaviour
{
    [SerializeField] List<AudioClip> buble;
    [SerializeField] List<AudioClip> disney;
    [SerializeField] List<AudioClip> pop;
    [SerializeField] List<AudioClip> kings;
    [SerializeField] TextMeshProUGUI showTrackName;
    AudioSource musicPlayer;
    int bubleLength;
    int disneyLength;
    int popLength;
    int kingsLength;
    System.Random rand = new System.Random();


    private void Awake() 
    {
        musicPlayer = GetComponent<AudioSource>();
    }
    void Start()
    {
        bubleLength = buble.Count;
        disneyLength = disney.Count;
        popLength = pop.Count;
        kingsLength = kings.Count;
    }

    private void Update() 
    {
        if (!musicPlayer.isPlaying) PlayNextSong();

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            SkipTrack();
        }
    }

    
    public void PlayNextSong()
    {
        switch (ChooseCatagory())
        {
            case 0:
                int bubleIndex = rand.Next(bubleLength);
                PlaySong(buble[bubleIndex], .70f);
                break;
            case 1:
                int disneyIndex = rand.Next(disneyLength);
                PlaySong(disney[disneyIndex], .60f);
                break;
            case 2:
                int popIndex = rand.Next(popLength);
                PlaySong(pop[popIndex], .60f);
                break;
            case 3:
                int kingsIndex = rand.Next(kingsLength);
                PlaySong(kings[kingsIndex], 1f);
                break;
        }
    }

    private int ChooseCatagory()
    {
        return rand.Next(4);
    }

    private void PlaySong(AudioClip song, float volume)
    {
        if (musicPlayer != null && song != null)
        {
            showTrackName.text = System.Text.RegularExpressions.Regex.Replace(song.name, @"[\d-]", string.Empty); ;
            musicPlayer.clip = song;
            musicPlayer.volume = volume;
            musicPlayer.Play();
        }  
    }

    private void SkipTrack()
    {
        musicPlayer.Stop();
        PlayNextSong();
    }
}
