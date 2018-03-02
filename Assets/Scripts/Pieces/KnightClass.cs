using UnityEngine;
using System.Collections;

public class KnightClass : MonoBehaviour {

    public int player;
    public Vector2 piecePosition;
    private BoardManager bm;
    public bool hasFirstMove = true;

    void OnMouseDown()
    {
        bm.playerClickedPiece(player, this.gameObject, BoardManager.BoardPiece.Knight, piecePosition);
    }

    public bool isValidSquare(BoardManager.BoardSquare[,] board, Vector2 releasedSquare, Vector2 piecePosition)
    {
        if (releasedSquare.x > board.GetLength(0) || releasedSquare.x < 0 || releasedSquare.y > board.GetLength(1) || releasedSquare.y < 0)
            return false;
        else if (releasedSquare.x != piecePosition.x && releasedSquare.y != piecePosition.y &&
                 Mathf.Abs(piecePosition.x - releasedSquare.x) + Mathf.Abs(piecePosition.y - releasedSquare.y) == 3.0f)
            return board[(int)releasedSquare.x, (int)releasedSquare.y].player != player;
        else
            return false;
    }
    // Use this for initialization
    void Start()
    {
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
    }
}
