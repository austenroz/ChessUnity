using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

    public LayerMask BoardMask;

    public Player player1;
    public Player player2;

    private int currentPlayerTurn = 1;

    public int numberOfMoves = 0;

    private bool isHoldingPiece = false;
    private GameObject pieceHolding;
    private BoardPiece pieceHoldingType;
    private Vector2 pieceHoldingPosition;
    private Vector2 pieceReleasedPosition; //Used for pawn promotion

    public int boardWidth = 8;
    public int boardHeight = 8;
    private BoardSquare[,] board;
    private GameObject[,] boardPieces;

    public GameObject movePieceSoundEffectGO;
    public GameObject capturePieceSoundEffectGO;
    private AudioSource movePieceSoundEffect;
    private AudioSource capturePieceSoundEffect;

    private bool showPawnPromotionMenu;

    private bool gameOver = false;
    private bool showMainMenu = false;
    public int winner = 1;
    public Texture instructionsTexture;

    private bool showControlsMenu = true;

	// Use this for initialization
	void Start () 
    {
        board = new BoardSquare[boardWidth, boardHeight];
        boardPieces = new GameObject[boardWidth, boardHeight];
        movePieceSoundEffect = movePieceSoundEffectGO.GetComponent<AudioSource>();
        capturePieceSoundEffect = capturePieceSoundEffectGO.GetComponent<AudioSource>();
	}
	
    void OnGUI()
    {
        if (showPawnPromotionMenu && !gameOver)
        {
            if (GUI.Button(new Rect(0.0f, 0.0f, 100.0f, 100.0f), "Queen"))
            {
                PromotePawn(BoardPiece.Queen);
            }
            else if (GUI.Button(new Rect(0.0f, 100.0f, 100.0f, 100.0f), "Rook"))
            {
                PromotePawn(BoardPiece.Rook);
            }
            else if (GUI.Button(new Rect(0.0f, 200.0f, 100.0f, 100.0f), "Knight"))
            {
                PromotePawn(BoardPiece.Knight);
            }
            else if (GUI.Button(new Rect(0.0f, 300.0f, 100.0f, 100.0f), "Bishop"))
            {
                PromotePawn(BoardPiece.Bishop);
            }
        }
        if (gameOver)
        {
            string winnerText = "";
            if (winner == 1)
                winnerText = "White wins!";
            else
                winnerText = "Black wins!";

            GUI.Box(new Rect(0.0f, Screen.height-100, Screen.width, 100), winnerText);
        }
        if (showMainMenu)
        {
            if (GUI.Button(new Rect(Screen.width-100.0f, 0.0f, 100.0f, 100.0f), "New Game"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (GUI.Button(new Rect(Screen.width - 100.0f, 120.0f, 100.0f, 100.0f), "Exit to Desktop"))
            {
                Application.Quit();
            }
            if (!gameOver)
                GUI.Box(new Rect(Screen.width - 300.0f, Screen.height - 100.0f, 300.0f, 100.0f), (currentPlayerTurn == 1 ? "White" : "Black") + "'s Turn");
        }
        if (showControlsMenu)
        {
            float sizeOfWindow = Screen.height > Screen.width ? Screen.width : Screen.height;
            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), instructionsTexture);
            if (GUI.Button(new Rect(Screen.width - 300.0f, 0.0f, 300.0f, 100.0f), "Start Game"))
            {
                showControlsMenu = false;
                showMainMenu = true;
                NewGame();
            }
        }
    }

    void PromotePawn(BoardPiece type)
    {
        Destroy(boardPieces[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y]);

        board[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y].piece = type;
        Player pla = currentPlayerTurn == 1 ? player1 : player2;

        if (type == BoardPiece.Rook)
        {
            GameObject g = (GameObject)Instantiate(pla.rookPiece, new Vector3(pieceReleasedPosition.x, 0, pieceReleasedPosition.y), Quaternion.Euler(270, 0, 0));
            g.GetComponent<RookClass>().player = board[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y].player;
            g.GetComponent<RookClass>().piecePosition = new Vector2(pieceReleasedPosition.x, pieceReleasedPosition.y);
            boardPieces[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y] = g;
        }
        else if (type == BoardPiece.Knight)
        {
            GameObject g = (GameObject)Instantiate(pla.knightPiece, new Vector3(pieceReleasedPosition.x, 0, pieceReleasedPosition.y), Quaternion.Euler(270, 0, 0));
            g.GetComponent<KnightClass>().player = board[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y].player;
            g.GetComponent<KnightClass>().piecePosition = new Vector2(pieceReleasedPosition.x, pieceReleasedPosition.y);
            boardPieces[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y] = g;
        }
        else if (type == BoardPiece.Bishop)
        {
            GameObject g = (GameObject)Instantiate(pla.bishopPiece, new Vector3(pieceReleasedPosition.x, 0, pieceReleasedPosition.y), Quaternion.Euler(270, 0, 0));
            g.GetComponent<BishopClass>().player = board[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y].player;
            g.GetComponent<BishopClass>().piecePosition = new Vector2(pieceReleasedPosition.x, pieceReleasedPosition.y);
            boardPieces[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y] = g;
        }
        else if (type == BoardPiece.Queen)
        {
            GameObject g = (GameObject)Instantiate(pla.queenPiece, new Vector3(pieceReleasedPosition.x, 0, pieceReleasedPosition.y), Quaternion.Euler(270, 0, 0));
            g.GetComponent<QueenClass>().player = board[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y].player;
            g.GetComponent<QueenClass>().piecePosition = new Vector2(pieceReleasedPosition.x, pieceReleasedPosition.y);
            boardPieces[(int)pieceReleasedPosition.x, (int)pieceReleasedPosition.y] = g;
        }

        currentPlayerTurn = currentPlayerTurn == 1 ? 2 : 1;
        numberOfMoves++;
        showPawnPromotionMenu = false;

    }

	// Update is called once per frame
	void Update () 
    {
        if (!gameOver)
        {
            if (!showPawnPromotionMenu)
            {
                if (isHoldingPiece)
                {
                    RaycastHit vHit = new RaycastHit();
                    Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(vRay, out vHit, 1000, BoardMask))
                    {
                        pieceHolding.transform.position = new Vector3(vHit.point.x, pieceHolding.transform.position.y, vHit.point.z);
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (isHoldingPiece)
                    {
                        RaycastHit vHit = new RaycastHit();
                        Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(vRay, out vHit, 1000, BoardMask))
                        {
                            Vector2 releasedSquare = new Vector2(Mathf.Round(vHit.point.x), Mathf.Round(vHit.point.z));
                            pieceHolding.transform.position = new Vector3(vHit.point.x, pieceHolding.transform.position.y, vHit.point.z);
                            if (pieceHoldingType == BoardPiece.Pawn)
                            {
                                if (pieceHolding.GetComponent<PawnClass>().isValidSquare(ref board, releasedSquare, pieceHoldingPosition, ref boardPieces))
                                {
                                    playerReleasedPiece(releasedSquare, BoardPiece.Pawn);
                                    pieceHolding.GetComponent<PawnClass>().hasFirstMove = false;
                                    pieceHolding.GetComponent<PawnClass>().turnFirstMoveUsedOn = numberOfMoves - 1;
                                }
                                else
                                    resetPieceHolding();
                            }
                            else if (pieceHoldingType == BoardPiece.Rook)
                            {
                                if (pieceHolding.GetComponent<RookClass>().isValidSquare(board, releasedSquare, pieceHoldingPosition))
                                {
                                    playerReleasedPiece(releasedSquare, BoardPiece.Rook);
                                    pieceHolding.GetComponent<RookClass>().hasFirstMove = false;
                                }
                                else
                                    resetPieceHolding();
                            }
                            else if (pieceHoldingType == BoardPiece.Knight)
                            {
                                if (pieceHolding.GetComponent<KnightClass>().isValidSquare(board, releasedSquare, pieceHoldingPosition))
                                {
                                    playerReleasedPiece(releasedSquare, BoardPiece.Knight);
                                    pieceHolding.GetComponent<KnightClass>().hasFirstMove = false;
                                }
                                else
                                    resetPieceHolding();
                            }
                            else if (pieceHoldingType == BoardPiece.Bishop)
                            {
                                if (pieceHolding.GetComponent<BishopClass>().isValidSquare(board, releasedSquare, pieceHoldingPosition))
                                {
                                    playerReleasedPiece(releasedSquare, BoardPiece.Bishop);
                                    pieceHolding.GetComponent<BishopClass>().hasFirstMove = false;
                                }
                                else
                                    resetPieceHolding();
                            }
                            else if (pieceHoldingType == BoardPiece.Queen)
                            {
                                if (pieceHolding.GetComponent<QueenClass>().isValidSquare(board, releasedSquare, pieceHoldingPosition))
                                {
                                    playerReleasedPiece(releasedSquare, BoardPiece.Queen);
                                    pieceHolding.GetComponent<QueenClass>().hasFirstMove = false;
                                }
                                else
                                    resetPieceHolding();
                            }
                            else if (pieceHoldingType == BoardPiece.King)
                            {
                                if (pieceHolding.GetComponent<KingClass>().isValidSquare(board, releasedSquare, pieceHoldingPosition, boardPieces))
                                {
                                    playerReleasedPiece(releasedSquare, BoardPiece.King);
                                    pieceHolding.GetComponent<KingClass>().hasFirstMove = false;
                                }
                                else
                                    resetPieceHolding();
                            }
                        }
                    }
                }
            }
        }
	}

    public void NewGame()
    {
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                board[x, y].piece = BoardPiece.None;
                board[x, y].player = 0;
                Destroy(boardPieces[x, y]);
            }
        }
        for (int x = 0; x < player1.initialPieceFormation.GetLength(0); x++)
        {
            for (int y = 0; y < player1.initialPieceFormation.GetLength(1); y++)
            {
                board[x, y + 6].piece = player1.initialPieceFormation[x, y].piece;
                board[x, y + 6].player = 1;
            }
        }
        for (int x = 0; x < player2.initialPieceFormation.GetLength(0); x++)
        {
            for (int y = 0; y < player2.initialPieceFormation.GetLength(1); y++)
            {
                board[x, y].piece = player2.initialPieceFormation[x, y].piece;
                board[x, y].player = 2;
            }
        }
        reloadBoard();
    }

    public void playerClickedPiece(int pla, GameObject goPiece, BoardPiece bp, Vector2 pos)
    {
        if (pla == currentPlayerTurn && !isHoldingPiece)
        {
            goPiece.transform.position = new Vector3(goPiece.transform.position.x, goPiece.transform.position.y + .5f, goPiece.transform.position.z);
            pieceHolding = goPiece;
            pieceHoldingPosition = pos;
            pieceHoldingType = bp;
            isHoldingPiece = true;
        }
    }

    public void resetPieceHolding()
    {
        isHoldingPiece = false;
        pieceHolding.transform.position = new Vector3(pieceHoldingPosition.x, pieceHolding.transform.position.y - .5f, pieceHoldingPosition.y);
    }

    public void playerReleasedPiece(Vector2 releasedSquare, BoardPiece bp)
    {
        pieceReleasedPosition.x = releasedSquare.x;
        pieceReleasedPosition.y = releasedSquare.y;
        isHoldingPiece = false;
        Debug.Log(board[(int)releasedSquare.x, (int)releasedSquare.y].piece);
        if (board[(int)releasedSquare.x, (int)releasedSquare.y].piece != BoardPiece.None)
        {
            if (board[(int)releasedSquare.x, (int)releasedSquare.y].piece == BoardPiece.King)
            {
                winner = currentPlayerTurn;
                gameOver = true;
            }
            capturePieceSoundEffect.Play();
            Destroy(boardPieces[(int)releasedSquare.x, (int)releasedSquare.y]);
        }
        else
        {
            movePieceSoundEffect.Play();
        }
        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y] = boardPieces[(int)pieceHoldingPosition.x, (int)pieceHoldingPosition.y];

        board[(int)pieceHoldingPosition.x, (int)pieceHoldingPosition.y].piece = BoardPiece.None;
        board[(int)pieceHoldingPosition.x, (int)pieceHoldingPosition.y].player = 0;
        boardPieces[(int)pieceHoldingPosition.x, (int)pieceHoldingPosition.y] = null;


        board[(int)releasedSquare.x, (int)releasedSquare.y].piece = pieceHoldingType;
        board[(int)releasedSquare.x, (int)releasedSquare.y].player = currentPlayerTurn;
        pieceHolding.transform.position = new Vector3(releasedSquare.x, pieceHolding.transform.position.y - .5f, releasedSquare.y);
        if (bp == BoardPiece.Pawn)
        {
            pieceHolding.GetComponent<PawnClass>().piecePosition = releasedSquare;
            pieceHolding.GetComponent<PawnClass>().hasFirstMove = false;
        }
        else if (bp == BoardPiece.Rook)
        {
            pieceHolding.GetComponent<RookClass>().piecePosition = releasedSquare;
            pieceHolding.GetComponent<RookClass>().hasFirstMove = false;
        }
        else if (bp == BoardPiece.Knight)
        {
            pieceHolding.GetComponent<KnightClass>().piecePosition = releasedSquare;
            pieceHolding.GetComponent<KnightClass>().hasFirstMove = false;
        }
        else if (bp == BoardPiece.Bishop)
        {
            pieceHolding.GetComponent<BishopClass>().piecePosition = releasedSquare;
            pieceHolding.GetComponent<BishopClass>().hasFirstMove = false;
        }
        else if (bp == BoardPiece.Queen)
        {
            pieceHolding.GetComponent<QueenClass>().piecePosition = releasedSquare;
            pieceHolding.GetComponent<QueenClass>().hasFirstMove = false;
        }
        else if (bp == BoardPiece.King)
        {
            pieceHolding.GetComponent<KingClass>().piecePosition = releasedSquare;
            pieceHolding.GetComponent<KingClass>().hasFirstMove = false;
        }

        if (pieceHoldingType == BoardPiece.Pawn &&
            ((currentPlayerTurn == 1 && releasedSquare.y == 0) || (currentPlayerTurn == 2 && releasedSquare.y == 7)))
        {
            Debug.Log("Player Reached end");
            showPawnPromotionMenu = true;
        }
        else
        {
            currentPlayerTurn = currentPlayerTurn == 1 ? 2 : 1;
            numberOfMoves++;
        }
    }

    public void MovePiece(GameObject piece, Vector2 previousPosition, Vector2 newPosition)
    {
        BoardPiece bpMoving = board[(int)previousPosition.x, (int)previousPosition.y].piece;
        int playerMoving = board[(int)previousPosition.x, (int)previousPosition.y].player;

        board[(int)previousPosition.x, (int)previousPosition.y].piece = BoardPiece.None;
        board[(int)previousPosition.x, (int)previousPosition.y].player = 0;

        board[(int)newPosition.x, (int)newPosition.y].piece = bpMoving;
        board[(int)newPosition.x, (int)newPosition.y].player = playerMoving;

        boardPieces[(int)previousPosition.x, (int)previousPosition.y].transform.position = 
            new Vector3(newPosition.x, boardPieces[(int)previousPosition.x, (int)previousPosition.y].transform.position.y, newPosition.y);

        boardPieces[(int)newPosition.x, (int)newPosition.y] = boardPieces[(int)previousPosition.x, (int)previousPosition.y];
        boardPieces[(int)previousPosition.x, (int)previousPosition.y] = null;

        if (bpMoving == BoardPiece.Pawn)
        {
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<PawnClass>().piecePosition = newPosition;
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<PawnClass>().hasFirstMove = false;
        }
        else if (bpMoving == BoardPiece.Rook)
        {
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<RookClass>().piecePosition = newPosition;
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<RookClass>().hasFirstMove = false;
        }
        else if (bpMoving == BoardPiece.Knight)
        {
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<KnightClass>().piecePosition = newPosition;
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<KnightClass>().hasFirstMove = false;
        }
        else if (bpMoving == BoardPiece.Bishop)
        {
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<BishopClass>().piecePosition = newPosition;
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<BishopClass>().hasFirstMove = false;
        }
        else if (bpMoving == BoardPiece.Queen)
        {
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<QueenClass>().piecePosition = newPosition;
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<QueenClass>().hasFirstMove = false;
        }
        else if (bpMoving == BoardPiece.King)
        {
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<KingClass>().piecePosition = newPosition;
            boardPieces[(int)newPosition.x, (int)newPosition.y].GetComponent<KingClass>().hasFirstMove = false;
        }
    }

    public void reloadBoard()
    {
        for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    Player pla = board[x, y].player == 1 ? player1 : player2;
                    if (board[x, y].piece == BoardPiece.Pawn)
                    {
                        GameObject g = (GameObject) Instantiate(pla.pawnPiece, new Vector3(x, 0, y), Quaternion.Euler(270, 0, 0));
                        g.GetComponent<PawnClass>().player = board[x, y].player;
                        g.GetComponent<PawnClass>().piecePosition = new Vector2(x, y);
                        boardPieces[x, y] = g;
                    }
                    else if (board[x, y].piece == BoardPiece.Rook)
                    {
                        GameObject g = (GameObject)Instantiate(pla.rookPiece, new Vector3(x, 0, y), Quaternion.Euler(270, 0, 0));
                        g.GetComponent<RookClass>().player = board[x, y].player;
                        g.GetComponent<RookClass>().piecePosition = new Vector2(x, y);
                        boardPieces[x, y] = g;
                    }
                    else if (board[x, y].piece == BoardPiece.Knight)
                    {
                        GameObject g = (GameObject)Instantiate(pla.knightPiece, new Vector3(x, 0, y), Quaternion.Euler(270, 0, 0));
                        g.GetComponent<KnightClass>().player = board[x, y].player;
                        g.GetComponent<KnightClass>().piecePosition = new Vector2(x, y);
                        boardPieces[x, y] = g;
                    }
                    else if (board[x, y].piece == BoardPiece.Bishop)
                    {
                        GameObject g = (GameObject)Instantiate(pla.bishopPiece, new Vector3(x, 0, y), Quaternion.Euler(270, 0, 0));
                        g.GetComponent<BishopClass>().player = board[x, y].player;
                        g.GetComponent<BishopClass>().piecePosition = new Vector2(x, y);
                        boardPieces[x, y] = g;
                    }
                    else if (board[x, y].piece == BoardPiece.Queen)
                    {
                        GameObject g = (GameObject)Instantiate(pla.queenPiece, new Vector3(x, 0, y), Quaternion.Euler(270, 0, 0));
                        g.GetComponent<QueenClass>().player = board[x, y].player;
                        g.GetComponent<QueenClass>().piecePosition = new Vector2(x, y);
                        boardPieces[x, y] = g;
                    }
                    else if (board[x, y].piece == BoardPiece.King)
                    {
                        GameObject g = (GameObject)Instantiate(pla.kingPiece, new Vector3(x, 0, y), Quaternion.Euler(270, 0, 0));
                        g.GetComponent<KingClass>().player = board[x, y].player;
                        g.GetComponent<KingClass>().piecePosition = new Vector2(x, y);
                        boardPieces[x, y] = g;
                    }
                }
            }
    }

    public enum BoardPiece
    {
        None,
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    public struct BoardSquare
    {
        public int player;
        public BoardPiece piece;
    }
}
