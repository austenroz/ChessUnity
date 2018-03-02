using UnityEngine;
using System.Collections;

public class PawnClass : MonoBehaviour {

    public int player;
    public Vector2 piecePosition;
    private BoardManager bm;
    public bool hasFirstMove = true;
    public int turnFirstMoveUsedOn = 0;
    public bool movedDoubleForward = false;

    void OnMouseDown()
    {
        bm.playerClickedPiece(player, this.gameObject, BoardManager.BoardPiece.Pawn, piecePosition);
    }

    public bool isValidSquare(ref BoardManager.BoardSquare[,] board, Vector2 releasedSquare, Vector2 piecePosition, ref GameObject[,] boardPieces)
    {
        if (player == 1)
        {
            //Default: Move forward one space
            if (releasedSquare.y == piecePosition.y - 1 && releasedSquare.x == piecePosition.x &&
                board[(int)releasedSquare.x, (int)releasedSquare.y].piece == BoardManager.BoardPiece.None)
                return true;
            //First Move: Move forward two spaces
            else if (releasedSquare.y == piecePosition.y - 2 && hasFirstMove && releasedSquare.x == piecePosition.x &&
                     board[(int)releasedSquare.x, (int)releasedSquare.y].piece == BoardManager.BoardPiece.None)
            {
                movedDoubleForward = true;
                return true;
            }
            //Attacking: Move diagonally one space
            else if ((releasedSquare.y == piecePosition.y - 1) &&
                ((releasedSquare.x == piecePosition.x - 1 && board[(int)releasedSquare.x, (int)releasedSquare.y].piece != BoardManager.BoardPiece.None &&
                releasedSquare.x == piecePosition.x - 1 && board[(int)releasedSquare.x, (int)releasedSquare.y].player != 1) ||
                (releasedSquare.x == piecePosition.x + 1 && board[(int)releasedSquare.x, (int)releasedSquare.y].piece != BoardManager.BoardPiece.None &&
                releasedSquare.x == piecePosition.x + 1 && board[(int)releasedSquare.x, (int)releasedSquare.y].player != 1)))
                return true;
            //En Passant
            else if (releasedSquare.y == piecePosition.y - 1)
            {
                if (releasedSquare.x == piecePosition.x - 1 && board[(int)releasedSquare.x, (int)releasedSquare.y + 1].piece == BoardManager.BoardPiece.Pawn &&
                    board[(int)releasedSquare.x, (int)releasedSquare.y + 1].player != 1)
                {
                    if (boardPieces[(int)releasedSquare.x, (int)releasedSquare.y + 1].GetComponent<PawnClass>().turnFirstMoveUsedOn == bm.numberOfMoves - 1 &&
                        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y + 1].GetComponent<PawnClass>().movedDoubleForward)
                    {
                        Destroy(boardPieces[(int)releasedSquare.x, (int)releasedSquare.y + 1]);
                        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y + 1] = null;
                        board[(int)releasedSquare.x, (int)releasedSquare.y + 1].player = 0;
                        board[(int)releasedSquare.x, (int)releasedSquare.y + 1].piece = BoardManager.BoardPiece.None;
                        return true;
                    }
                }
                else if (releasedSquare.x == piecePosition.x + 1 && board[(int)releasedSquare.x, (int)releasedSquare.y + 1].piece == BoardManager.BoardPiece.Pawn &&
                board[(int)releasedSquare.x, (int)releasedSquare.y + 1].player != 1)
                {
                    if (boardPieces[(int)releasedSquare.x, (int)releasedSquare.y + 1].GetComponent<PawnClass>().turnFirstMoveUsedOn == bm.numberOfMoves - 1 &&
                        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y + 1].GetComponent<PawnClass>().movedDoubleForward)
                    {
                        Destroy(boardPieces[(int)releasedSquare.x, (int)releasedSquare.y + 1]);
                        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y + 1] = null;
                        board[(int)releasedSquare.x, (int)releasedSquare.y + 1].player = 0;
                        board[(int)releasedSquare.x, (int)releasedSquare.y + 1].piece = BoardManager.BoardPiece.None;
                        return true;
                    }
                }
            }
        }
        if (player == 2)
        {
            //Default: Move forward one space
            if (releasedSquare.y == piecePosition.y + 1 && releasedSquare.x == piecePosition.x && 
                board[(int)releasedSquare.x, (int)releasedSquare.y].piece == BoardManager.BoardPiece.None)
                return true;
            //First Move: Move forward two spaces
            else if (releasedSquare.y == piecePosition.y + 2 && hasFirstMove && releasedSquare.x == piecePosition.x && 
                     board[(int)releasedSquare.x, (int)releasedSquare.y].piece == BoardManager.BoardPiece.None)
            {
                movedDoubleForward = true;
                return true;
            }
            //Attacking: Move diagonally one space
            else if ((releasedSquare.y == piecePosition.y + 1) &&
                ((releasedSquare.x == piecePosition.x - 1 && board[(int)releasedSquare.x, (int)releasedSquare.y].piece != BoardManager.BoardPiece.None &&
                releasedSquare.x == piecePosition.x - 1 && board[(int)releasedSquare.x, (int)releasedSquare.y].player != 2) ||
                (releasedSquare.x == piecePosition.x + 1 && board[(int)releasedSquare.x, (int)releasedSquare.y].piece != BoardManager.BoardPiece.None &&
                releasedSquare.x == piecePosition.x + 1 && board[(int)releasedSquare.x, (int)releasedSquare.y].player != 2)))
                return true;
            //En Passant
            else if (releasedSquare.y == piecePosition.y + 1)
            {
                if (releasedSquare.x == piecePosition.x - 1 && board[(int)releasedSquare.x, (int)releasedSquare.y - 1].piece == BoardManager.BoardPiece.Pawn &&
                    board[(int)releasedSquare.x, (int)releasedSquare.y - 1].player != 2)
                {
                    if (boardPieces[(int)releasedSquare.x, (int)releasedSquare.y - 1].GetComponent<PawnClass>().turnFirstMoveUsedOn == bm.numberOfMoves - 1 &&
                        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y - 1].GetComponent<PawnClass>().movedDoubleForward)
                    {
                        Destroy(boardPieces[(int)releasedSquare.x, (int)releasedSquare.y - 1]);
                        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y - 1] = null;
                        board[(int)releasedSquare.x, (int)releasedSquare.y - 1].player = 0;
                        board[(int)releasedSquare.x, (int)releasedSquare.y - 1].piece = BoardManager.BoardPiece.None;
                        return true;
                    }
                }
                else if (releasedSquare.x == piecePosition.x + 1 && board[(int)releasedSquare.x, (int)releasedSquare.y - 1].piece == BoardManager.BoardPiece.Pawn &&
                board[(int)releasedSquare.x, (int)releasedSquare.y - 1].player != 2)
                {
                    if (boardPieces[(int)releasedSquare.x, (int)releasedSquare.y - 1].GetComponent<PawnClass>().turnFirstMoveUsedOn == bm.numberOfMoves - 1 &&
                        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y - 1].GetComponent<PawnClass>().movedDoubleForward)
                    {
                        Destroy(boardPieces[(int)releasedSquare.x, (int)releasedSquare.y - 1]);
                        boardPieces[(int)releasedSquare.x, (int)releasedSquare.y - 1] = null;
                        board[(int)releasedSquare.x, (int)releasedSquare.y - 1].player = 0;
                        board[(int)releasedSquare.x, (int)releasedSquare.y - 1].piece = BoardManager.BoardPiece.None;
                        return true;
                    }
                }
            }
        }
        return false;
    }
	// Use this for initialization
	void Start () 
    {
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
	}
}
