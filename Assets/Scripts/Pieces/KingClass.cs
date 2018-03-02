using UnityEngine;
using System.Collections;

public class KingClass : MonoBehaviour {

    public int player;
    public Vector2 piecePosition;
    private BoardManager bm;
    public bool hasFirstMove = true;

    void OnMouseDown()
    {
        bm.playerClickedPiece(player, this.gameObject, BoardManager.BoardPiece.King, piecePosition);
    }

    public bool isValidSquare(BoardManager.BoardSquare[,] board, Vector2 releasedSquare, Vector2 piecePosition, GameObject[,] boardPieces)
    {
        if (releasedSquare.x > board.GetLength(0) || releasedSquare.x < 0 || releasedSquare.y > board.GetLength(1) || releasedSquare.y < 0)
            return false;
        else if (releasedSquare == piecePosition)
            return false;
        else if (Mathf.Abs(piecePosition.x - releasedSquare.x) == 2 && Mathf.Abs(piecePosition.y - releasedSquare.y) == 0)
        {
            return CheckForCastle(board, boardPieces, piecePosition, releasedSquare);
        }
        //Normal move
        else if (Mathf.Abs(piecePosition.x - releasedSquare.x) <= 1 && Mathf.Abs(piecePosition.y - releasedSquare.y) <= 1)
            return board[(int)releasedSquare.x, (int)releasedSquare.y].player != player;
        else
            return false;
    }

    public bool CheckForCastle(BoardManager.BoardSquare[,] board, GameObject[,] boardPieces, Vector2 piecePosition, Vector2 releasedSquare)
    {
        if (hasFirstMove)
        {
            if (board[(int)(releasedSquare.x > piecePosition.x ? board.GetLength(0)-1 : 0),(int)releasedSquare.y].piece == BoardManager.BoardPiece.Rook)
            {
                if (boardPieces[(int)(releasedSquare.x > piecePosition.x ? board.GetLength(0)-1 : 0),(int)releasedSquare.y].GetComponent<RookClass>().hasFirstMove)
                {
                    if (CheckIfLineIsClear(board, new Vector2(releasedSquare.x > piecePosition.x ? 1 : -1, 0), piecePosition, 
                        new Vector2((int)(releasedSquare.x > piecePosition.x ? board.GetLength(0)-1 : 0), (int)releasedSquare.y)))
                    {
                        bm.MovePiece(boardPieces[(int)(releasedSquare.x > piecePosition.x ? board.GetLength(0)-1 : 0), (int)releasedSquare.y],
                                    new Vector2((int)(releasedSquare.x > piecePosition.x ? board.GetLength(0)-1 : 0), (int)releasedSquare.y),
                                    new Vector2((int)(releasedSquare.x > piecePosition.x ? releasedSquare.x-1 : releasedSquare.x+1), (int)releasedSquare.y));
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool CheckIfLineIsClear(BoardManager.BoardSquare[,] board, Vector2 direction, Vector2 position, Vector2 rookPosition)
    {
        if (position == rookPosition)
            return true;
        else if (board[(int)position.x, (int)position.y].piece != BoardManager.BoardPiece.None && position != this.piecePosition)
        {
            return false;
        }
        else if (position.x + direction.x < 0 || position.x + direction.x + 1 > board.GetLength(0))
        {
            return false;
        }
        else if (position.y + direction.y < 0 || position.y + direction.y + 1 > board.GetLength(1))
        {
            return false;
        }
        else
            return CheckIfLineIsClear(board, direction, position + direction, rookPosition);
    }
    // Use this for initialization
    void Start()
    {
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
    }
}
