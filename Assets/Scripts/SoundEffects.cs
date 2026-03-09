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
    public float walkInterval=0.5f;     
    public float crouchInterval=0.8f;

    [Header("Volume Settings")]
    [Range(0f,1f)] public float footstep;
    [Range(0f,1f)] public float jump;
    [Range(0f,1f)] public float land;

    private AudioSource _audioSource;
    private CharacterController _controller;
    private FirstPersonController _player;

    private float _stepTimer;       
    private bool _wasGrounded;      

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
        if (!_player.Grounded) return;

        if (_controller.velocity.magnitude < 0.1f) return;

        float interval;
        
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
            _audioSource.PlayOneShot(sandFootstep);     
            _stepTimer=interval;        
        }
    }

    private void HandleJump()
    {
        if(_wasGrounded && !_player.Grounded)
        {
            _audioSource.PlayOneShot(jumpSound,jump);
            _stepTimer=0f;      
        }

        if(!_wasGrounded && _player.Grounded)
        {
            _audioSource.PlayOneShot(landSound,land);
            _stepTimer=0f;
        }

        _wasGrounded=_player.Grounded;      
    }
}
