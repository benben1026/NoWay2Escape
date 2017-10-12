
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour {

    public Slider Volume;
    public AudioSource myMusic;

    public void VolumeControl()
    {
        myMusic.volume = Volume.value;
    }
}
