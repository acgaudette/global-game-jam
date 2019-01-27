using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

//Usage: put this on a tenant prefab
//intent: script will determine what sprite/animation will be displayed on the tenant prefab

public class AnimationManager : MonoBehaviour
{
    // reference to Tenant.cs script
    public Tenant _Tenant;
    //reference to SpriteRenderer
    private SpriteRenderer _spriteRenderer;
    //reference to Animator
    private Animator _anim;

    public RuntimeAnimatorController[] _controllers;

    void Awake()
    {
        
    }

    void Start()
    {
        //reference to SpriteRenderer
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //reference to SpriteRenderer
        _anim = GetComponent<Animator>();
        //function that selects which 
        TenantSelected();
    }

    
    void Update()
    {
        
    }

    
    //function that assigns Tenant prefab's Animator 
    void TenantSelected()
    {
        Debug.Log(_Tenant.data.trait.TraitName);
        
        if (_Tenant.data.trait.TraitName == "Alien")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/alien_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[0];
        }
        
        if (_Tenant.data.trait.TraitName == "Barista")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/barista_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[1];
        }
        
        if (_Tenant.data.trait.TraitName == "Dino")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/dino_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[2];
        }
        
        if (_Tenant.data.trait.TraitName == "Fish")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/fish_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[3];
        }
        
        if (_Tenant.data.trait.TraitName == "Hypebeast")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/hypebeast_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[4];
            
        }
        
        if (_Tenant.data.trait.TraitName == "L Train")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/ltrain_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[5];
        }
        
        if (_Tenant.data.trait.TraitName == "Pizza")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/pizza_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[6];
        }
        
        if (_Tenant.data.trait.TraitName == "Snake")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/snake_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[7];
        }
        
        if (_Tenant.data.trait.TraitName == "Streamer")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/twitch_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[8];
        }
        
        if (_Tenant.data.trait.TraitName == "Taxi")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/taxi_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[9];
        }
        
        
        if (_Tenant.data.trait.TraitName == "Teeth")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/teeth_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[10];
        }
        
        if (_Tenant.data.trait.TraitName == "VR")
        {
            //_anim.runtimeAnimatorController = Resources.Load("Assets/Animations/vr_tenant_walk_0") as RuntimeAnimatorController;
            _anim.runtimeAnimatorController = _controllers[11];
        }
    }
}
