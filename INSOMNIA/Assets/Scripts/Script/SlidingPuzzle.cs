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
        // ���콺 ���� ��ư Ŭ�� �� 
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ�� ���� �浹 ����
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                //Debug.Log(hit.transform.name);

                // ���� ������ ������� �� �̵�
                if (Vector2.Distance(EmptyPiece.position, hit.transform.position) < 5)
                {
                    // Ŭ���� ������ ��ũ��Ʈ�� ������
                    Pieces ThisPiece = hit.transform.GetComponent<Pieces>();

                    // �� ����� Ŭ�� ���� Swap
                    Vector2 LastEmptyPosition = EmptyPiece.position;
                    EmptyPiece.position = ThisPiece.TargetPosition;
                    ThisPiece.TargetPosition = LastEmptyPosition;

                    // �迭 ����
                    int PieceIndex = FindIndex(ThisPiece);
                    pieces[EmptyPieceIndex] = pieces[PieceIndex];
                    pieces[PieceIndex] = null;
                    EmptyPieceIndex = PieceIndex;
                }
            }
        }

        // ���� ������� üũ
        CorrectPuzzleCheck();
    }

    void CorrectPuzzleCheck()
    {
        int CorrectPieces = 0;

        // �迭�� Ȯ���Ͽ� �ùٸ� ��ġ�� �ִ� ���� ������ ��
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

        // �ùٸ� ��ġ�� �ִ� ������ ���� �̵� ������ ������ �� ����
        if (CorrectPieces == pieces.Length - 1)     //�̵� ������ ���� ��: ����� ������ ������ ���� �迭
        {
            isClear = true;
            SceneManager.LoadScene("CharactorScene");
        }
    }

    // �ش� ������ ��ġ�� �迭 �ε����� ��ȯ
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

            // �迭 ����
            pieces[EmptyPieceIndex] = pieces[8];
            pieces[8] = null;
            EmptyPieceIndex = 8;
        }

        // ���� ���� Ƚ���� ¦���� ������ ����
        // ¦���� ���� �ذ� ����
        do
        {
            for (int i = 0; i < 8; i++)
            {
                int randomIndex = Random.Range(0, 7);

                // Swap
                var lastPos = pieces[i].TargetPosition;
                pieces[i].TargetPosition = pieces[randomIndex].TargetPosition;
                pieces[randomIndex].TargetPosition = lastPos;

                // �迭 ����
                var tile = pieces[i];
                pieces[i] = pieces[randomIndex];
                pieces[randomIndex] = tile;
            }
        } while (GetInversions() % 2 != 0);  
    }


    // ���� ���� Ƚ��
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
