BinaryTree binaryTree = new BinaryTree();

binaryTree.CreateTree(30, new List<int>() { 15, 41, 20, 26, 16, 45, 54, 36 });

//binaryTree.CreateRootBST(20);

//binaryTree.InsertNodeBST(15);
//binaryTree.InsertNodeBST(23);
//binaryTree.InsertNodeBST(51);
//binaryTree.InsertNodeBST(41);
//binaryTree.InsertNodeBST(5);
//binaryTree.InsertNodeBST(15);

binaryTree.ShowTree(binaryTree.Root);

Node node = binaryTree.SearchNodeBST(323);

Console.WriteLine($"Search Value: {node?.Data.ToString() ?? "not found"}");
Console.WriteLine();

Console.WriteLine("PrefixOrder:");
binaryTree.PrefixOrder(binaryTree.Root);
Console.WriteLine("\n");

Console.WriteLine("InfixOrder:");
binaryTree.InfixOrder(binaryTree.Root);
Console.WriteLine("\n");

Console.WriteLine("PostfixOrder:");
binaryTree.PostfixOrder(binaryTree.Root);
Console.WriteLine("\n");

Console.WriteLine("Before delete: ");
binaryTree.ShowTree(binaryTree.Root);

binaryTree.DeleteNodeBST(41);
Console.WriteLine();

Console.WriteLine("After delete: ");
binaryTree.ShowTree(binaryTree.Root);

class Node
{
    public Node LeftNode { get; set; }
    public Node RightNode { get; set; }
    public int Data { get; set; }
}

class BinaryTree
{
    public Node Root { get; set; }

    public void ShowTree(Node parent)
    {
        if (parent != null)
        {
            Console.WriteLine($"{parent.Data} layer = {GetLevelOfNode(this.Root, parent.Data)}");
            ShowTree(parent.LeftNode);
            ShowTree(parent.RightNode);
        }
    }

    public void CreateRootBST(int value)
    {
        this.Root = new Node();
        this.Root.Data = value;
    }

    public int GetLevelOfNode(Node node, int data)
    {
        return GetLevelUtil(node, data, 1);
    }

    private int GetLevelUtil(Node node, int data, int level)
    {
        if (node == null)
        {
            return 0;
        }

        if (node.Data == data)
        {
            return level;
        }

        int downlevel
            = GetLevelUtil(node.LeftNode, data, level + 1);
        if (downlevel != 0)
        {
            return downlevel;
        }

        downlevel
            = GetLevelUtil(node.RightNode, data, level + 1);
        return downlevel;
    }

    public void CreateTree(int rootValue, List<int> values)
    {
        this.CreateRootBST(rootValue);

        foreach (var item in values)
        {
            this.InsertNodeBST(item);
        }

        Console.WriteLine($"Tree successfully created with rootValue: {rootValue}");
    }

    public bool InsertNodeBST(int value)
    {
        Node before = null, after = this.Root;

        while (after != null)
        {
            before = after;
            if (value < after.Data)         // Новий елемент з ліва
                after = after.LeftNode;
            else if (value > after.Data)    // Новий елемент з права
                after = after.RightNode;
            else
            {
                // Вже існує такий елемент
                return false;
            }
        }

        Node newNode = new Node();
        newNode.Data = value;

        if (this.Root == null)  // Якщо дерево пусте
            this.Root = newNode;
        else
        {
            if (value < before.Data)
                before.LeftNode = newNode;
            else
                before.RightNode = newNode;
        }

        return true;
    }

    public Node? SearchNodeBST(int value)
    {
        var res = this.Find(value, this.Root);
        if (res == null)
        {
            Console.WriteLine("Node not found :(");
            return null;
        }
        return this.Find(value, this.Root);
    }

    private Node? Find(int value, Node parent)
    {
        if (parent != null)
        {
            if (value == parent.Data)
            {
                return parent;
            }
            if (value < parent.Data)
            {
                return Find(value, parent.LeftNode);
            }
            else
            {
                return Find(value, parent.RightNode);
            }
        }

        return null;
    }

    public void DeleteNodeBST(int value)
    {
        this.Root = Remove(this.Root, value);
    }

    private Node Remove(Node parent, int key)
    {
        if (parent == null) return parent;

        if (key < parent.Data) parent.LeftNode = Remove(parent.LeftNode, key);
        else if (key > parent.Data)
            parent.RightNode = Remove(parent.RightNode, key);
        // Якщо значення таке саме як parent's значення, то цей елемент і має бути видалений
        else
        {
            // вузол лише з одним дочірнім елементом або без нього
            if (parent.LeftNode == null)
                return parent.RightNode;
            else if (parent.RightNode == null)
                return parent.LeftNode;

            // елемент з двома елементами: беремо найменший елемент в правому піддереві
            parent.Data = MinValue(parent.RightNode);

            // І видаляємо цей елемент
            parent.RightNode = Remove(parent.RightNode, parent.Data);
        }

        return parent;
    }

    private int MinValue(Node node)
    {
        int minv = node.Data;

        while (node.LeftNode != null)
        {
            minv = node.LeftNode.Data;
            node = node.LeftNode;
        }

        return minv;
    }

    // Всі які ми зустрічаємо перший раз
    public void PrefixOrder(Node parent)
    {
        if (parent != null)
        {
            Console.Write(parent.Data + " ");
            PrefixOrder(parent.LeftNode);
            PrefixOrder(parent.RightNode);
        }
    }

    // Всі які ми зустрічаємо другий раз раз
    public void InfixOrder(Node parent)
    {
        if (parent != null)
        {
            InfixOrder(parent.LeftNode);
            Console.Write(parent.Data + " ");
            InfixOrder(parent.RightNode);
        }
    }

    // Всі які ми зустрічаємо останній раз
    public void PostfixOrder(Node parent)
    {
        if (parent != null)
        {
            PostfixOrder(parent.LeftNode);
            PostfixOrder(parent.RightNode);
            Console.Write(parent.Data + " ");
        }
    }
}