using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject pawnPiece;
    public GameObject rookPiece;
    public GameObject knightPiece;
    public GameObject bishopPiece;
    public GameObject queenPiece;
    public GameObject kingPiece;

    
    public BoardManager.BoardSquare[,] initialPieceFormation = new BoardManager.BoardSquare[8,2];

	// Use this for initialization
	void Start () 
    {
        for (int x = 0; x < initialPieceFormation.GetLength(0); x++)
        {
            for (int y = 0; y < initialPieceFormation.GetLength(1); y++)
            {
                initialPieceFormation[x, y].player = 1;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
