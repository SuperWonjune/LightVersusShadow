using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSizeDefiner : MonoBehaviour {

    private static int screenWidth = Screen.width;
    private static int screenHeight = Screen.height;


    // 이동 input 경계선
    public static int blackMoveXBoundary = screenWidth / 10;
    public static int whiteMoveXBoundary = screenWidth - (screenWidth / 10);
    public static int blackMoveYBoundary = screenHeight / 2;
    public static int whiteMoveYBoundary = screenHeight / 2;

    // 발사 input 경계선
    public static int blackShootXBoundary = (screenWidth / 2) - (screenWidth / 25);
    public static int whiteShootXBoundary = (screenWidth / 2) + (screenWidth / 25);




    void Start () {
        print(blackMoveXBoundary);
        print(whiteMoveXBoundary);
        print(blackMoveYBoundary);
        print(blackShootXBoundary);
        print(whiteShootXBoundary);
    }

    private void Update()
    {
        
    }
}
