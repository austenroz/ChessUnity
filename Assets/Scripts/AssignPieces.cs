using UnityEngine;
using System.Collections;

public class AssignPieces : MonoBehaviour {

    public Player player1;
    public Player player2;

	// Use this for initialization
	void Start () 
    {
        player1.initialPieceFormation[0, 1].piece = BoardManager.BoardPiece.Rook;
        player1.initialPieceFormation[1, 1].piece = BoardManager.BoardPiece.Knight;
        player1.initialPieceFormation[2, 1].piece = BoardManager.BoardPiece.Bishop;
        player1.initialPieceFormation[3, 1].piece = BoardManager.BoardPiece.King;
        player1.initialPieceFormation[4, 1].piece = BoardManager.BoardPiece.Queen;
        player1.initialPieceFormation[5, 1].piece = BoardManager.BoardPiece.Bishop;
        player1.initialPieceFormation[6, 1].piece = BoardManager.BoardPiece.Knight;
        player1.initialPieceFormation[7, 1].piece = BoardManager.BoardPiece.Rook;
        player1.initialPieceFormation[0, 0].piece = BoardManager.BoardPiece.Pawn;
        player1.initialPieceFormation[1, 0].piece = BoardManager.BoardPiece.Pawn;
        player1.initialPieceFormation[2, 0].piece = BoardManager.BoardPiece.Pawn;
        player1.initialPieceFormation[3, 0].piece = BoardManager.BoardPiece.Pawn;
        player1.initialPieceFormation[4, 0].piece = BoardManager.BoardPiece.Pawn;
        player1.initialPieceFormation[5, 0].piece = BoardManager.BoardPiece.Pawn;
        player1.initialPieceFormation[6, 0].piece = BoardManager.BoardPiece.Pawn;
        player1.initialPieceFormation[7, 0].piece = BoardManager.BoardPiece.Pawn;

        player2.initialPieceFormation[0, 0].piece = BoardManager.BoardPiece.Rook;
        player2.initialPieceFormation[1, 0].piece = BoardManager.BoardPiece.Knight;
        player2.initialPieceFormation[2, 0].piece = BoardManager.BoardPiece.Bishop;
        player2.initialPieceFormation[3, 0].piece = BoardManager.BoardPiece.King;
        player2.initialPieceFormation[4, 0].piece = BoardManager.BoardPiece.Queen;
        player2.initialPieceFormation[5, 0].piece = BoardManager.BoardPiece.Bishop;
        player2.initialPieceFormation[6, 0].piece = BoardManager.BoardPiece.Knight;
        player2.initialPieceFormation[7, 0].piece = BoardManager.BoardPiece.Rook;
        player2.initialPieceFormation[0, 1].piece = BoardManager.BoardPiece.Pawn;
        player2.initialPieceFormation[1, 1].piece = BoardManager.BoardPiece.Pawn;
        player2.initialPieceFormation[2, 1].piece = BoardManager.BoardPiece.Pawn;
        player2.initialPieceFormation[3, 1].piece = BoardManager.BoardPiece.Pawn;
        player2.initialPieceFormation[4, 1].piece = BoardManager.BoardPiece.Pawn;
        player2.initialPieceFormation[5, 1].piece = BoardManager.BoardPiece.Pawn;
        player2.initialPieceFormation[6, 1].piece = BoardManager.BoardPiece.Pawn;
        player2.initialPieceFormation[7, 1].piece = BoardManager.BoardPiece.Pawn;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
}
