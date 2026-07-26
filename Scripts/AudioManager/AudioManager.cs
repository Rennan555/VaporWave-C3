using Godot;
using System;

public partial class AudioManager : Node
{
    [Export] private AudioStreamPlayer SFXAudioCatWalk;
	[Export] private AudioStreamPlayer SFXAudioPlayerWalk;
	[Export] private AudioStreamPlayer SFXAudioPlayerDash;
	[Export] private AudioStreamPlayer SFXAudioPlayerJump;
	[Export] private AudioStreamPlayer SFXAudioPlayerDamage;
	[Export] private AudioStreamPlayer SFXAudioPlayerDie;
	[Export] private AudioStreamPlayer SFXAudioPopUp;
	[Export] private AudioStreamPlayer SFXAudioMouseClick;
    public static AudioManager Instance;

    public override void _Ready()
    {
        Instance = this;
    }

    public void PlayCatWalk(){SFXAudioCatWalk.Play();}
    public void PlayPlayerWalk(){SFXAudioPlayerWalk.Play();}
    public bool IsPlayerWalkPlaying(){return SFXAudioPlayerWalk.Playing;}
    public void StopPlayerWalk(){SFXAudioPlayerWalk.Stop();}
    public void PlayPlayerDash(){SFXAudioPlayerDash.Play();}
    public void PlayPlayerJump(){SFXAudioPlayerJump.Play();}
    public void PlayPlayerDamage(){SFXAudioPlayerDamage.Play();}
    public void PlayPlayerDie(){SFXAudioPlayerDie.Play();}
    public void PlayPopUp(){SFXAudioPopUp.Play();}
    public void PlayMouseClick(){SFXAudioMouseClick.Play();}


}
