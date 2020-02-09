using System;
using NUnit.Framework;
using Turnable.Pathfinding;
using System.Linq;
using System.Collections.Generic;
using Turnable.Utilities;

namespace Tests.Pathfinding
{
    [TestFixture]
    public class NodeListTests
    {
        private List<Node> nodes;
        private NodeList nodeList;
        private Rectangle levelBounds;

        [SetUp]
        public void SetUp()
        {
            levelBounds = new Rectangle(new Position(0, 0), new Position(15, 15));
            nodes = new List<Node>();
            nodes.Add(new Node(levelBounds, new Position(1, 1)));
            nodes.Add(new Node(levelBounds, new Position(2, 2)));
            nodes.Add(new Node(levelBounds, new Position(3, 2)));
            nodeList = new NodeList();
        }

        [Test]
        public void Add_GivenANode_AddsTheNode()
        {
            nodeList.Add(nodes[0]);

            Assert.That(nodeList.Count, Is.EqualTo(1));
            Assert.That(nodeList.First<Node>, Is.SameAs(nodes.First<Node>()));
        }

        [Test]
        public void Insert_ThrowsException_InOrderToPreventOutOfOrderInsertion()
        {
            Assert.That(() => nodeList.Insert(0, nodes.First<Node>()), Throws.TypeOf<NotImplementedException>());
        }

        //[Test]
        //public void Add_AddsNodesSortedByEstimatedMovementCost()
        //{
        //    _nodes[0].EstimatedMovementCost = 10;
        //    _nodes[1].EstimatedMovementCost = 5;
        //    _nodes[2].EstimatedMovementCost = 3;

        //    // NOTE: AddRange is not overriden in NodeList, so each node has to be added one by one in order to maintain the sorted order
        //    foreach (Node node in _nodes)
        //    {
        //        _nodeList.Add(node);
        //    }

        //    Assert.That(_nodeList[0], Is.EqualTo(_nodes[2]));
        //    Assert.That(_nodeList[1], Is.EqualTo(_nodes[1]));
        //    Assert.That(_nodeList[2], Is.EqualTo(_nodes[0]));
        //}

        //[Test]
        //public void Add_WhenAddingNodesWithSamePathScore_Succeeds()
        //{
        //    _nodes[0].ActualMovementCost = 5;
        //    _nodes[0].EstimatedMovementCost = 5;
        //    _nodes[1].ActualMovementCost = 5;
        //    _nodes[1].EstimatedMovementCost = 5;
        //    _nodes[2].ActualMovementCost = 5;
        //    _nodes[2].EstimatedMovementCost = 5;

        //    _nodeList.AddRange(_nodes);

        //    foreach (Node node in _nodes)
        //    {
        //        Assert.That(_nodeList.Contains(node), Is.True);
        //    }
        //}
    }
}