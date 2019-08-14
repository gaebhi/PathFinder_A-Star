using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public int X;
    public int Y;

    public bool IsWall;
    public Vector3 Position;

    public Node ParentNode;

    public int G;//현재 비용
    public int H;//맨해튼 거리 사용

    public int F { get { return G + H; } }

    public Node(bool _isWall, Vector3 _position, int _x, int _y)
    {
        IsWall = _isWall;
        Position = _position;
        X = _x;
        Y = _y;
    }
}
