using UnityEngine;
using System.Collections;

public class RookClass : MonoBehaviour {


    public int player;
    public Vector2 piecePosition;
    private BoardManager bm;
    public bool hasFirstMove = true;

    void OnMouseDown()
    {
        bm.playerClickedPiece(player, this.gameObject, BoardManager.BoardPiece.Rook, piecePosition);
    }

    public bool isValidSquare(BoardManager.BoardSquare[,] board, Vector2 releasedSquare, Vector2 piecePosition)
    {
        return (CheckDiagonal(board, new Vector2(0, 1), piecePosition, releasedSquare) ||
                CheckDiagonal(board, new Vector2(0, -1), piecePosition, releasedSquare) ||
                CheckDiagonal(board, new Vector2(1, 0), piecePosition, releasedSquare) ||
                CheckDiagonal(board, new Vector2(-1, 0), piecePosition, releasedSquare));
    }

    private bool CheckDiagonal(BoardManager.BoardSquare[,] board, Vector2 direction, Vector2 position, Vector2 releasedSquare)
    {
        if (releasedSquare == this.piecePosition)
            return false;
        else if (position == releasedSquare && board[(int)position.x, (int)position.y].player != player)
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
            return CheckDiagonal(board, direction, position + direction, releasedSquare);
    }

    // Use this for initialization
    void Start()
    {
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
    }
}
