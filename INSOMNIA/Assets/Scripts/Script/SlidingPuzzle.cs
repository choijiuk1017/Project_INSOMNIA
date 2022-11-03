using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidingPuzzle : MonoBehaviour
{
    [SerializeField] private Transform EmptyPiece = null;
    [SerializeField] private Pieces[] pieces;

    Camera MainCamera;

    int EmptyPieceIndex = 8;
    bool isClear = false;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시 
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치에 따른 충돌 감지
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                //Debug.Log(hit.transform.name);

                // 주위 퍼즐이 비어있을 때 이동
                if (Vector2.Distance(EmptyPiece.position, hit.transform.position) < 5)
                {
                    // 클릭한 퍼즐의 스크립트를 가져옴
                    Pieces ThisPiece = hit.transform.GetComponent<Pieces>();

                    // 빈 퍼즐과 클릭 퍼즐 Swap
                    Vector2 LastEmptyPosition = EmptyPiece.position;
                    EmptyPiece.position = ThisPiece.TargetPosition;
                    ThisPiece.TargetPosition = LastEmptyPosition;

                    // 배열 변경
                    int PieceIndex = FindIndex(ThisPiece);
                    pieces[EmptyPieceIndex] = pieces[PieceIndex];
                    pieces[PieceIndex] = null;
                    EmptyPieceIndex = PieceIndex;
                }
            }
        }

        // 퍼즐 맞췄는지 체크
        CorrectPuzzleCheck();
    }

    void CorrectPuzzleCheck()
    {
        int CorrectPieces = 0;

        // 배열을 확인하여 올바른 위치에 있는 퍼즐 개수를 셈
        foreach (var a in pieces)
        {
            if (a != null)
            {
                if (a.inRightPlace)
                {
                    CorrectPieces++;
                }
            }
        }

        // 올바른 위치에 있는 퍼즐의 수가 이동 가능한 퍼즐의 수 성공
        if (CorrectPieces == pieces.Length - 1)     //이동 가능한 퍼즐 수: 비어진 퍼즐을 제외한 퍼즐 배열
        {
            isClear = true;
            SceneManager.LoadScene("CharactorScene");
        }
    }

    // 해당 퍼즐이 위치한 배열 인덱스를 반환
    public int FindIndex(Pieces p)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i] != null)
            {
                if (pieces[i] == p)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public void Shuffle()
    {
        if (EmptyPieceIndex != 8)
        {
            // Swap
            var PieceOn8LastPos = pieces[8].TargetPosition;
            pieces[8].TargetPosition = EmptyPiece.position;
            EmptyPiece.position = PieceOn8LastPos;

            // 배열 변경
            pieces[EmptyPieceIndex] = pieces[8];
            pieces[8] = null;
            EmptyPieceIndex = 8;
        }

        // 퍼즐 섞는 횟수가 짝수일 때까지 셔플
        // 짝수일 때만 해결 가능
        do
        {
            for (int i = 0; i < 8; i++)
            {
                int randomIndex = Random.Range(0, 7);

                // Swap
                var lastPos = pieces[i].TargetPosition;
                pieces[i].TargetPosition = pieces[randomIndex].TargetPosition;
                pieces[randomIndex].TargetPosition = lastPos;

                // 배열 변경
                var tile = pieces[i];
                pieces[i] = pieces[randomIndex];
                pieces[randomIndex] = tile;
            }
        } while (GetInversions() % 2 != 0);  
    }


    // 퍼즐 섞는 횟수
    int GetInversions()
    {
        int inversionsSum = 0;

        for(int i = 0; i < pieces.Length; i++)
        {
            int thisPieceInvertion = 0;

            for(int j = i; j < pieces.Length; j++)
            {
                if(pieces[j] != null)
                {
                    if (pieces[i].number > pieces[j].number)
                    {
                        thisPieceInvertion++;
                    }
                }
            }
            inversionsSum += thisPieceInvertion;
        }
        return inversionsSum;
    }
}
