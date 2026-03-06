using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]

public class SoundEffects : MonoBehaviour
{
    [Header("Footstep Sounds")]
    public AudioClip sandFootstep;
    public AudioClip jumpSound;
    public AudioClip landSound;

    [Header("Footstep settings")]
    public float walkInterval=0.5f;     //how often footsteps play
    public float crouchInterval=0.8f;

    [Header("Volume Settings")]
    [Range(0f,1f)] public float footstep;
    [Range(0f,1f)] public float jump;
    [Range(0f,1f)] public float land;


    private AudioSource _audioSource;
    private CharacterController _controller;
    private FirstPersonController _player;

    private float _stepTimer;       //controls footstep timing
    private bool _wasGrounded;      //stores last frame's grounded state, allows to the detect transition from ground-air

    void Start()
    {
        _audioSource=GetComponent<AudioSource>();
        _controller=GetComponent<CharacterController>();
        _player=GetComponent<FirstPersonController>();

        _audioSource.playOnAwake=false;
        _audioSource.loop=false;
        _audioSource.spatialBlend=1f;

        _wasGrounded=_player.Grounded;
    }

    void Update()
    {
        HandleFootSteps();
        HandleJump();
    }

    private void HandleFootSteps()
    {
        //if the player in air-no footstep
        if (!_player.Grounded)
        {
            return;
        }

        //must be moving
        if (_controller.velocity.magnitude < 0.1f)
        {
            return;
        }

        float interval;
        //chooses how fast footsteps play based on movement state
        if (_player.IsCrouching)
        {
            interval=crouchInterval;
        }
        else
        {
            interval=walkInterval;
        }

        _stepTimer-=Time.deltaTime;
        if (_stepTimer <= 0f)
        {
            _audioSource.PlayOneShot(sandFootstep);     //plays sound
            _stepTimer=interval;        //resets timer
        }
    }

    private void HandleJump()
    {
        //if last frame on ground-this in air
        if(_wasGrounded && !_player.Grounded)
        {
            _audioSource.PlayOneShot(jumpSound,jump);
            _stepTimer=0f;      //prevents footstep overlap after jump or land
        }

        //landing
        if(!_wasGrounded && _player.Grounded)
        {
            _audioSource.PlayOneShot(landSound,land);
            _stepTimer=0f;
        }

        _wasGrounded=_player.Grounded;      //stores current state for next frame
    }
}
