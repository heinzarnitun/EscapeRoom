using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI gameMessage;
    [SerializeField] private TextMeshProUGUI gameMessage2;
    [SerializeField] private TextMeshProUGUI gameMessage3;
    [SerializeField] private TextMeshProUGUI gameMessage4;

    [Header("Board Buttons")]
    [SerializeField] private Button[] buttons = new Button[9];

  [Header("Game Over Effects")]
public GameObject finalFireObject;
public AudioSource finalBgm;


    [Header("Audio Sources")]
    public AudioSource playerWinSound;
    public AudioSource playerLoseSound;
    public AudioSource finalWinSound;
    public AudioSource finalLoseSound;

    [Header("Timer")]
    public CountdownTimer countdownTimer;

    [Header("Panel Control")]
    public CloseTicTacToePanel panelCloser;

    [Header("Post-Game Message UI")]
    public TextMeshProUGUI finalGameOverMessage; // final message Text UI

    private TextMeshProUGUI[] buttonTexts = new TextMeshProUGUI[9];
    private char[] board = new char[9];

    private int playerScore = 0;
    private int computerScore = 0;
    private int gameCount = 0;

    private bool playerTurn = true;
    private bool gameOver = false;
    private bool finalGameOver = false;

    private bool playerWonFinal = false;

    private Color defaultColor = Color.black;
    private Color winColor = Color.green;

    private void Start()
    {
        if (finalFireObject != null)
{
    finalFireObject.SetActive(false);
}

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null)
            {
                Debug.LogError($"Button {i} is not assigned!");
                continue;
            }
            int index = i;
            buttonTexts[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttons[i].onClick.AddListener(() => OnPlayerClick(index));
        }

        if (finalGameOverMessage != null)
            finalGameOverMessage.gameObject.SetActive(false);

        StartNewRound();
    }

    private void Update()
    {
        if (!finalGameOver && countdownTimer != null && countdownTimer.timeRemaining <= 0)
        {
            finalGameOver = true;
            DisableAllButtons();
            if (finalLoseSound != null) finalLoseSound.Play();
            playerWonFinal = false;
            Invoke(nameof(ClosePanelAndShowFinalMessage), 3f);
        }

        if (Input.GetKeyDown(KeyCode.L))
    {
        finalGameOver = true;
        playerWonFinal = false;
        if (finalLoseSound != null) finalLoseSound.Play();
        DisableAllButtons();
        Invoke(nameof(ClosePanelAndShowFinalMessage), 3f);
    }
    if (Input.GetKeyDown(KeyCode.P))
{
     // Simulate final winning condition immediately:

    playerScore = 3;  // Set player's score to final winning score
    playerWonFinal = true;

    UpdateScore();

    if (finalWinSound != null) finalWinSound.Play();

    DisableAllButtons();
    ClosePanelAndShowFinalMessage();

}


    }

    private void StartNewRound()
    {
        gameCount++;
        gameOver = false;
        playerTurn = true;
        gameMessage3.text = "Playing...";
        gameMessage.text = "Turn - Player";
        gameMessage4.text = $"Game Count: {gameCount}";

        for (int i = 0; i < 9; i++)
        {
            board[i] = '\0';
            buttonTexts[i].text = "";
            buttonTexts[i].color = defaultColor;
            buttons[i].interactable = true;
        }
    }

    private void OnPlayerClick(int index)
    {
        if (gameOver || !playerTurn || board[index] != '\0') return;

        MakeMove(index, 'X');
        if (CheckWin('X'))
        {
            PlayerWinsRound();
            return;
        }
        else if (CheckDraw())
        {
            DrawRound();
            return;
        }

        playerTurn = false;
        gameMessage.text = "Turn - Master";
        Invoke(nameof(ComputerMove), 1f);
    }

    private void ComputerMove()
    {
        if (gameOver || finalGameOver) return;

        int move = FindBestMove();
        MakeMove(move, 'O');

        if (CheckWin('O'))
        {
            ComputerWinsRound();
            return;
        }
        else if (CheckDraw())
        {
            DrawRound();
            return;
        }

        playerTurn = true;
        gameMessage.text = "Turn - Player";
    }

    private int FindBestMove()
    {
        int[,] wins = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < wins.GetLength(0); i++)
        {
            int a = wins[i,0], b = wins[i,1], c = wins[i,2];
            if (board[a] == 'O' && board[b] == 'O' && board[c] == '\0') return c;
            if (board[a] == 'O' && board[b] == '\0' && board[c] == 'O') return b;
            if (board[a] == '\0' && board[b] == 'O' && board[c] == 'O') return a;
        }

        for (int i = 0; i < wins.GetLength(0); i++)
        {
            int a = wins[i,0], b = wins[i,1], c = wins[i,2];
            if (board[a] == 'X' && board[b] == 'X' && board[c] == '\0') return c;
            if (board[a] == 'X' && board[b] == '\0' && board[c] == 'X') return b;
            if (board[a] == '\0' && board[b] == 'X' && board[c] == 'X') return a;
        }

        List<int> available = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == '\0') available.Add(i);
        }
        if (available.Count == 0) return 0;
        return available[Random.Range(0, available.Count)];
    }

    private void MakeMove(int index, char playerChar)
    {
        board[index] = playerChar;
        buttonTexts[index].text = playerChar.ToString();
        buttons[index].interactable = false;
    }

    private bool CheckWin(char playerChar)
    {
        int[,] wins = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < wins.GetLength(0); i++)
        {
            int a = wins[i, 0];
            int b = wins[i, 1];
            int c = wins[i, 2];

            if (board[a] == playerChar && board[b] == playerChar && board[c] == playerChar)
            {
                HighlightWin(a, b, c);
                return true;
            }
        }
        return false;
    }

    private bool CheckDraw()
    {
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == '\0') return false;
        }
        return true;
    }

    private void HighlightWin(int a, int b, int c)
    {
        buttonTexts[a].color = winColor;
        buttonTexts[b].color = winColor;
        buttonTexts[c].color = winColor;
    }

    private void PlayerWinsRound()
    {
        playerScore++;
        gameMessage3.text = "Player won this round!";
        if (playerWinSound != null) playerWinSound.Play();
        UpdateScore();
        EndRoundCheck();
    }

    private void ComputerWinsRound()
    {
        computerScore++;
        gameMessage3.text = "Master won this round! -2 mins penalty.";
        if (playerLoseSound != null) playerLoseSound.Play();

        if (countdownTimer != null)
            countdownTimer.DeductTime(120f);

        UpdateScore();
        EndRoundCheck();
    }

    private void DrawRound()
    {
        gameMessage3.text = "Round Draw!";
        EndRoundCheck();
    }

    private void UpdateScore()
    {
        gameMessage2.text = $"Player: {playerScore} - Master: {computerScore}";
    }

    private void EndRoundCheck()
    {
        gameOver = true;

        if (playerScore >= 3)
        {
            finalGameOver = true;
            playerWonFinal = true;
            if (finalWinSound != null) finalWinSound.Play();
            DisableAllButtons();
            Invoke(nameof(ClosePanelAndShowFinalMessage), 3f);
            return;
        }

        if (computerScore >= 3)
        {
            finalGameOver = true;
            playerWonFinal = false;
            if (finalLoseSound != null) finalLoseSound.Play();
            DisableAllButtons();
            Invoke(nameof(ClosePanelAndShowFinalMessage), 3f);
            return;
        }

        Invoke(nameof(StartNewRound), 3f);
    }

    private void DisableAllButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

   private void ClosePanelAndShowFinalMessage()
{
    if (panelCloser != null)
    {
        panelCloser.ClosePanel();
    }

    if (playerWonFinal)
    {
        // Player won: make sure fire is off
        if (finalFireObject != null)
            finalFireObject.SetActive(false);
        finalBgm.Stop();
        playerWinSound.Play();


    }
    else
    {
        // Player lost: enable fire effect
        if (finalFireObject != null)
            finalFireObject.SetActive(true);
        if (finalBgm != null)
    {
        finalBgm.Play();
    }
    
    }

    

    if (finalGameOverMessage != null)
    {
        finalGameOverMessage.gameObject.SetActive(true);
        finalGameOverMessage.text = playerWonFinal ? "Good job! You escaped finally!" : "Game Over";
        Invoke(nameof(ExitAfterFinalMessage), 5f);
    }
}


private void ExitAfterFinalMessage()
{
    // Stop audio
    if (finalBgm != null)
        finalBgm.Stop();

    // Hide fire FX
    if (finalFireObject != null)
        finalFireObject.SetActive(false);

    // Hide final message
    if (finalGameOverMessage != null)
        finalGameOverMessage.gameObject.SetActive(false);

    // Quit play mode in editor or quit app in build
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
}




}
