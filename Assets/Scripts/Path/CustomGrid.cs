using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{

    public Transform StartPosition;
    public LayerMask WallMask;
    public Vector2 GridSize;
    public float NodeSize;

    public List<Node> Path;

    private Node[,] _nodeArray;

    private int GridSizeX;
    private int GridSizeY;

    private void Start()
    {
        GridSizeX = Mathf.RoundToInt(GridSize.x / NodeSize);// 노드 하나의 크기로 나누어 배열의 인덱스로 사용
        GridSizeY = Mathf.RoundToInt(GridSize.y / NodeSize);// 노드 하나의 크기로 나누어 배열의 인덱스로 사용
        CreateGrid();
    }

    public void CreateGrid()
    {
        _nodeArray = new Node[GridSizeX, GridSizeY];
        Vector3 _bottomLeft = transform.position - Vector3.right * GridSize.x / 2 - Vector3.forward * GridSize.y / 2;//원점에서 왼쪽 아래로 이동
        _bottomLeft += Vector3.right * (NodeSize/2) + Vector3.forward * (NodeSize/2);//구한 노드의 중심 위치로 이동
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 _point = _bottomLeft + Vector3.right * (x * NodeSize) + Vector3.forward * (y * NodeSize);
                bool _isWall = true;

                if (Physics.CheckSphere(_point, NodeSize, WallMask))//WallMask를 무시하고 충돌 검사
                {
                    _isWall = false;
                }

                _nodeArray[x, y] = new Node(_isWall, _point, x, y);
            }
        }
    }

    public List<Node> GetNeighboringNodes(Node _neighborNode)
    {
        List<Node> _neighborList = new List<Node>();
        int _neighborX;
        int _neighborY;

        //오른쪽 노드
        _neighborX = _neighborNode.X + 1;
        _neighborY = _neighborNode.Y;

        if(InRange(_neighborX, _neighborY))
        {
            _neighborList.Add(_nodeArray[_neighborX, _neighborY]);//가능한 인접 리스트에 추가
        }

        //왼쪽 노드
        _neighborX = _neighborNode.X - 1;
        _neighborY = _neighborNode.Y;

        if (InRange(_neighborX, _neighborY))
        {
            _neighborList.Add(_nodeArray[_neighborX, _neighborY]);//가능한 인접 리스트에 추가
        }

        //위
        _neighborX = _neighborNode.X;
        _neighborY = _neighborNode.Y + 1;

        if (InRange(_neighborX, _neighborY))
        {
            _neighborList.Add(_nodeArray[_neighborX, _neighborY]);//가능한 인접 리스트에 추가
        }

        //아래
        _neighborX = _neighborNode.X;
        _neighborY = _neighborNode.Y - 1;

        if (InRange(_neighborX, _neighborY))
        {
            _neighborList.Add(_nodeArray[_neighborX, _neighborY]);//가능한 인접 리스트에 추가
        }

        return _neighborList;
    }

    private bool InRange(int _x, int _y)
    {
        if (_x >= 0 && _x < GridSizeX)
        {
            if (_y >= 0 && _y < GridSizeY)
            {
                return true;
            }
        }
        return false;
    }

    public Node GetNodeByPosition(Vector3 _position)
    {
        //원점을 (0.5f,0.5f)로 만듬
        float _x = ((_position.x + GridSize.x / 2) / GridSize.x);
        float _y = ((_position.z + GridSize.y / 2) / GridSize.y);

        _x = Mathf.Clamp01(_x);
        _y = Mathf.Clamp01(_y);
       
        return _nodeArray[Mathf.RoundToInt((GridSizeX - 1) * _x), Mathf.RoundToInt((GridSizeY - 1) * _y)];
    }

    private void OnDrawGizmos()
    {
        if (_nodeArray != null)
        {
            foreach (Node n in _nodeArray)
            {
                if (Path != null)
                {
                    if (Path.Contains(n))
                    {
                        Gizmos.DrawCube(n.Position, Vector3.one * NodeSize);
                        Gizmos.color = Color.red;//Set the color of that node
                    }
                }
            }
        }
    }
}
