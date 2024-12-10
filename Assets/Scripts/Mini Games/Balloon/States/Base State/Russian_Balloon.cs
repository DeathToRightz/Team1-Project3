using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Russian_Balloon : MonoBehaviour
{
    //This is the script that hold all of the relevant information need for the Mini Game

    [SerializeField] public GameObject currentLever;
    //Array of the levers, may turn into a List
    [SerializeField] public GameObject[] _arrayOfLevers;

    //The tag that is used to define which is the bad lever
    [SerializeField] public string explosiveTagName;

    //The tag that is used to define the safe levers
    [SerializeField] public string safeTagName;

    //Get the reference to the states in the BaseState, all states inherit from the Base State meaning it can be switched between each other
    //causing the state transitions
    public BalloonMiniGamBaseState _currentState;

    //Variable of a Choose Lever State that is called here
    public BalloonStateChooseLever _stateChooseLever = new BalloonStateChooseLever();

    //Variable of a Randomize Bad Lever State that is called here
    public BalloonStateRandomizeBadLever _stateRandomizeBadLever = new BalloonStateRandomizeBadLever();


    public BalloonStateCheckingLever _stateCheckingLever = new BalloonStateCheckingLever();

    public BalloonStateResetLevers _stateResetLevers = new BalloonStateResetLevers();

    public BalloonStateWin _stateWin = new BalloonStateWin();
    LevelOneInput levelOneInput;
    public void SelectLever()
    {
        _currentState.OnLeverSelected(this);
    }
    //Variable of a Randomize Good Lever State that is called here
    //public BallonStateRandomizeGoodLevers _stateRandomizeGoodLever = new BallonStateRandomizeGoodLevers(); // Jancy Added this

    [SerializeField] public GameObject chosenLever = null;
    private void Awake()
    {
        //Current state for game starts as the Randomize Good Lever State
        //_currentState = _stateRandomizeGoodLever; // Jancy Added this

        RandomizePlayerPositions();

        // Enqueue the first two players
        for (int i = 0; i < Mathf.Min(2, players.Count); i++)
        {
            playerQueue.Enqueue(players[i]);
        }

        remainingPlayers = players.Count;

        //Current state for game starts as the Randomize Bad Lever State
        _currentState = _stateRandomizeBadLever;

        //With the current state being switched up top, we call the Start State function is initially made in the Base State
        //TO SEE WHAT THIS DOES move to the BalloonStateRandomizeBadLever script
        _currentState.OnStartState(this); // this refers to this script meaning where ever this is called we have info from here that may be needed else where


        levelOneInput = FindAnyObjectByType<LevelOneInput>();
        //currentPlayerOnStage = playerQueue.Dequeue();
        //nextPlayer = playerQueue.Peek();
        //StartCoroutine(ProcessQueue());
    }

    public void OnTransitionState(BalloonMiniGamBaseState stateToTransitionTo) ///When calling this funciton in other states it will ask for the specific state to change to
    {
        //Changes the current state to the chosen one in the parameter,
        //it then starts the Start state funciton for the specific new state, and debugs the name of the new state

        //_currentState.OnTransitionState(this);

        _currentState = stateToTransitionTo;

        _currentState.OnStartState(this);
        
        //Debug.Log("Transitioned to: " + stateToTransitionTo.ToString());
    }
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        _currentState.OnUpdateCurrentState(this);
    }

    [SerializeField] public int remainingPlayers;
   // private bool gameInProgress = true;
    [SerializeField] public Queue<GameObject> playerQueue = new Queue<GameObject>();
    private float smoothMoveSpeed = 5f;
    private float smoothMoveThreshold = 0.1f;
    [SerializeField] public Transform[] stagePositions;
    [SerializeField] public Transform[] exitPathPositions;
    [SerializeField] public Transform[] linePositions;
    [SerializeField] public List<GameObject> players;
    [SerializeField] public GameObject currentPlayerOnStage;
    [SerializeField] public GameObject nextPlayer;

    [SerializeField] public Animator _balloonAnimator;

    [SerializeField] public AnimationClip _inflateClip;
    
    //private GameObject winningPlayer;
    //private bool hasWinnerDeclared = false;

    private void RandomizePlayerPositions()
    {
        Shuffle(players);

        for (int i = 0; i < players.Count && i < linePositions.Length; i++)
        {
            players[i].transform.position = linePositions[i].position;
            players[i].transform.LookAt(new Vector3(0, players[i].transform.position.y, 0));
        }
    }

    private void Shuffle(List<GameObject> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public IEnumerator SmoothMovePlayer(GameObject player, Transform targetPosition)
    {
        float journeyLength = Vector3.Distance(player.transform.position, targetPosition.position);
        float startTime = Time.time;

        while (Vector3.Distance(player.transform.position, targetPosition.position) > smoothMoveThreshold)
        {
            float distanceCovered = (Time.time - startTime) * smoothMoveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition.position, smoothMoveSpeed * Time.deltaTime);
            yield return null;
        }

        player.transform.position = targetPosition.position;
    }

    public void UpdateExitPositions()
    {
        for (int i = 1; i < players.Count && i < linePositions.Length; i++)
        {
            GameObject player = players[i];
            Transform targetPosition = linePositions[i - 1];
            StartCoroutine(SmoothMovePlayer(player, targetPosition));
        }

        if (players.Count >= 1 && players.Count <= linePositions.Length)
        {
            GameObject lastPlayer = players[players.Count - 1];
            Transform lastExitPosition = linePositions[linePositions.Length - 1];

            for (int i = 0; i <= _arrayOfLevers.Length - 1; i++)   //This is the thing causing the array of levers to be the
                                                                   //player. What is the purpose of this?
            {
                //_arrayOfLevers[i] = players[0]; (This was Changed because it was giving an error)
            }

            StartCoroutine(SmoothMovePlayer(lastPlayer, lastExitPosition));
        }
    }

    public IEnumerator MovePlayerOntoStage(GameObject player)
    {
        // Define the target position on the stage (in this case, `stageStartPosition`)
        Vector3 targetPosition = stagePositions[0].position;
        levelOneInput.isInStageArea = true;
        // While the player hasn't yet reached the target position
        while (Vector3.Distance(player.transform.position, targetPosition) > 0.1f)
        {
            // Move the player smoothly towards the target
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                targetPosition,
                smoothMoveSpeed * Time.deltaTime
            );

            // Wait for the next frame before continuing the loop
            yield return null;
        }

        // Set the player's position to the exact target position to complete the movement
        player.transform.position = targetPosition;
    }

}


