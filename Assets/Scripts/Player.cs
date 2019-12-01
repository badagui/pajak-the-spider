using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    Cinemachine.CinemachineVirtualCamera vcamPlayer;

    [SerializeField]
    private SaveData saveData;

    [SerializeField]
    private GameObject pauseScreen;

    [SerializeField]
    private GameObject deathScreen;

    private Collider2D myCollider2D;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private SimpleAudioEvent landOnGroundAudioEvent;

    [SerializeField]
    private SimpleAudioEvent deathAudioEvent;

    private ContactFilter2D groundContactFilter;

    RaycastHit2D[] raycastHitResults = new RaycastHit2D[1];

    private bool isOnGround;
    public bool IsOnGround => isOnGround;

    private JumpPathPlanner jumpPathPlanner;

    private StateMachine stateMachine;

    private List<BaseState> playerStates;

    private PlayerAudio playerAudio;

    public bool isPlayerDisabled;

    public static Action OnPlayerRess = delegate { };

    [SerializeField]
    private GameObject itemHolder;

    private SpriteRenderer itemHolderSpriteRenderer;

    private Animator itemHolderAnimator;

    public bool IsHoldingItem => itemHolderSpriteRenderer.enabled;

    public bool InputDisabled;

    private void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
        jumpPathPlanner = GetComponent<JumpPathPlanner>();
        stateMachine = GetComponent<StateMachine>();
        playerAudio = GetComponent<PlayerAudio>();

        groundContactFilter = new ContactFilter2D();
        groundContactFilter.SetLayerMask(groundLayer);
        groundContactFilter.useLayerMask = true;

        // set start position
        if(saveData.LastCheckpointPosition == Vector3.zero)
        {
            saveData.LastCheckpointPosition = transform.position;
        }

        itemHolderSpriteRenderer = itemHolder.GetComponent<SpriteRenderer>();
        itemHolderAnimator = itemHolder.GetComponent<Animator>();
    }

    private void Start()
    {
        playerStates = new List<BaseState>() {  new PlayerGroundState(this),
                                                new PlayerFlyingState(this),
                                                new PlayerHookedState(this),
                                                new PlayerShootState(this) };
        stateMachine.SetAvailableStates(playerStates);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseMenu();
        }

        isOnGround = myCollider2D.Cast(Vector2.down, groundContactFilter, raycastHitResults, 0.2f) > 0;

        if (!pauseScreen.activeSelf && !isPlayerDisabled)
        {
            Type nextState = stateMachine.CurrentState.Tick();
            bool isTransition = nextState != stateMachine.CurrentState.GetType();
            if (isTransition && nextState == typeof(PlayerGroundState))
            {
                playerAudio.PlaySimpleAudio(landOnGroundAudioEvent);
            }

            stateMachine.SetCurrentState(nextState);
        }
    }

    public void TogglePauseMenu()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        Cursor.visible = pauseScreen.activeSelf;
        Cursor.lockState = pauseScreen.activeSelf ? CursorLockMode.Confined : CursorLockMode.Locked;
        Time.timeScale = pauseScreen.activeSelf ? 0f : 1f;
    }

    public void Death()
    {
        Time.timeScale = 1f;
        GetComponentInChildren<Animator>().SetTrigger("death");
        this.enabled = false;
        //mainCameraFollowTransform.isFollowing = false;
        vcamPlayer.Follow = null;
        deathScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        playerAudio.PlaySimpleAudio(deathAudioEvent);
        HideItem();

    }

    public void RessAtCheckpoint()
    {
        OnPlayerRess(); // static broadcast

        if (pauseScreen.activeSelf)
        {
            TogglePauseMenu();
        }

        transform.position = saveData.LastCheckpointPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //mainCameraFollowTransform.isFollowing = true;
        vcamPlayer.Follow = this.transform;
        this.enabled = true;
        GetComponentInChildren<Animator>().ResetTrigger("death");
        GetComponentInChildren<Animator>().SetTrigger("ress");
        stateMachine.SetCurrentState(typeof(PlayerGroundState));
    }

    public bool SetCheckpoint(Vector3 position)
    {
        bool newCheckpoint = (position != saveData.LastCheckpointPosition);
        if (newCheckpoint)
        {
            saveData.LastCheckpointPosition = position;
        }
        return newCheckpoint;

    }

    public void SetShootState()
    {
        stateMachine.SetCurrentState(typeof(PlayerShootState));
    }

    public void HoldItem(Sprite sprite)
    {
        itemHolderSpriteRenderer.sprite = sprite;
        itemHolderSpriteRenderer.enabled = true;
    }

    public void HideItem()
    {
        itemHolderSpriteRenderer.enabled = false;
    }

    public void UseItem()
    {
        itemHolderAnimator.SetTrigger("activate");
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;

        Cursor.visible = pauseScreen.activeSelf;
        Cursor.lockState = pauseScreen.activeSelf ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        print("levelLoad");
        saveData.LastCheckpointPosition = transform.position;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
}
