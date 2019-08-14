using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Agent Agent;
    private CustomGrid _grid;

    private void Awake()
    {
        _grid = GetComponent<CustomGrid>();
    }

    private void Update()
    {
        _grid.CreateGrid();//고정된 장애물일 경우 필요없는 구문
        FindPath(Agent.transform.position, Agent.TargetPosition);
        if(_grid.Path.Count > 0)
            Agent.NextNode = _grid.Path[0];
    }

    void FindPath(Vector3 _startPosition, Vector3 _targetPosition)
    {
        Agent.CurrentNode = _grid.GetNodeByPosition(_startPosition);//위치 기준으로 가장 가까운 노드 가져옴

        Node _startNode = Agent.CurrentNode;
        Node _targetNode = _grid.GetNodeByPosition(_targetPosition);

        List<Node> _openList = new List<Node>();
        HashSet<Node> _closeList = new HashSet<Node>();

        _openList.Add(_startNode);

        while (_openList.Count > 0)
        {
            Node _currentNode = _openList[0];

            for (int i = 1; i < _openList.Count; i++)
            {
                if (_openList[i].F < _currentNode.F || 
                    _openList[i].F == _currentNode.F && _openList[i].H < _currentNode.H)//비용이 작은 경우 _openList에 추가
                {
                    _currentNode = _openList[i];
                }
            }
            _openList.Remove(_currentNode);
            _closeList.Add(_currentNode);

            if (_currentNode == _targetNode)
            {
                GetPath(_startNode, _targetNode);
            }

            foreach (Node NeighborNode in _grid.GetNeighboringNodes(_currentNode))
            {
                if (!NeighborNode.IsWall || _closeList.Contains(NeighborNode))//벽이거나 닫힌 목록이면 넘어감
                {
                    continue;
                }
                int _cost = _currentNode.G + GetManhattenDistance(_currentNode, NeighborNode);

                if (_cost < NeighborNode.G || !_openList.Contains(NeighborNode))//열린목록이 아니거나(방문한적이 없거나) 코스트가 낮으면
                {
                    NeighborNode.G = _cost;
                    NeighborNode.H = GetManhattenDistance(NeighborNode, _targetNode);
                    NeighborNode.ParentNode = _currentNode;

                    if (!_openList.Contains(NeighborNode))//방문한적 없으면
                    {
                        _openList.Add(NeighborNode);
                    }
                }
            }
        }
    }

    private void GetPath(Node _startNode, Node _endNode)
    {
        List<Node> _path = new List<Node>();
        Node _currentNode = _endNode;

        while (_currentNode != _startNode)//뒤에서 부터 넣어준다.
        {
            _path.Add(_currentNode);
            _currentNode = _currentNode.ParentNode;//앞으로 한칸 이동
        }

        _path.Reverse();
        _grid.Path = _path;
    }

    int GetManhattenDistance(Node _nodeA, Node _nodeB)
    {
        return Mathf.Abs(_nodeA.X - _nodeB.X) + Mathf.Abs(_nodeA.Y - _nodeB.Y);//맨해튼 거리 = |x1-x2| + |y1-y2|;
    }
}
