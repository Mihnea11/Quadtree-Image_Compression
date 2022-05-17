namespace Quadtree_Image_Compression
{
    internal class QuadTreeNode
    {
        private double nodeError;
        private Color nodeColor;
        private Point leftCorner;
        private Point rightCorner;
        private bool isLeaf;
        private int nodeDepth;
        private QuadTreeNode[] children;

        public static QuadTreeNode nullNode = new QuadTreeNode();

        public QuadTreeNode()
        {
            nodeError = -1;
            nodeColor = Color.Black;
            nodeDepth = 0;

            leftCorner = new Point(0, 0);
            rightCorner = new Point(0, 0);

            isLeaf = true;
            children = new QuadTreeNode[4];
        }

        public double NodeError
        {
            get { return nodeError; }
            set { nodeError = value; }
        }

        public Color NodeColor
        {
            get { return nodeColor; }
            set { nodeColor = value; }
        }

        public Point LeftCorner
        {
            get { return leftCorner; }
            set { leftCorner = value; }
        }
        public Point RightCorner
        {
            get { return rightCorner; }
            set { rightCorner = value; }
        }

        public bool IsLeaf
        {
            get { return isLeaf; }
            set { isLeaf = value; }
        }

        public int NodeDepth
        {
            get { return nodeDepth; }
            set { nodeDepth = value; }
        }

        public QuadTreeNode GetChildrenIndex(int index)
        {
            return children[index];
        }

        public void SetCorners(Point leftCorner, Point rightCorner)
        {
            this.leftCorner = leftCorner;
            this.rightCorner = rightCorner;
        }

        public void SplitNode()
        {
            isLeaf = false;

            Point middle = new Point((leftCorner.X + (rightCorner.X - leftCorner.X) / 2), (leftCorner.Y + (rightCorner.Y - leftCorner.Y) / 2));

            Point firstLeft = leftCorner;
            Point firstRight = new Point(middle.X, middle.Y);

            Point secondLeft = new Point(middle.X, leftCorner.Y);
            Point secondRight = new Point(rightCorner.X, middle.Y);

            Point thirdLeft = new Point(leftCorner.X, middle.Y);
            Point thirdRight = new Point(middle.X, rightCorner.Y);

            Point fourthLeft = new Point(middle.X, middle.Y);
            Point fourthRight = rightCorner;

            children[0] = new QuadTreeNode();
            children[1] = new QuadTreeNode();
            children[2] = new QuadTreeNode();
            children[3] = new QuadTreeNode();

            children[0].NodeDepth = nodeDepth + 1;
            children[1].NodeDepth = nodeDepth + 1;
            children[2].NodeDepth = nodeDepth + 1;
            children[3].NodeDepth = nodeDepth + 1;

            children[0].SetCorners(firstLeft, firstRight);
            children[1].SetCorners(secondLeft, secondRight);
            children[2].SetCorners(thirdLeft, thirdRight);
            children[3].SetCorners(fourthLeft, fourthRight);
        }
    }
}