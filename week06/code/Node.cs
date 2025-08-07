public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
{
    if (value < Data)
    {
        // Insert to the left
        if (Left is null)
            Left = new Node(value);
        else
            Left.Insert(value);
    }
    else if (value > Data)
    {
        // Insert to the right
        if (Right is null)
            Right = new Node(value);
        else
            Right.Insert(value);
    }
    // If value == Data, do nothing (skip duplicates)
}

    public bool Contains(int value)
{
    if (value == Data)
    {
        return true;
    }
    else if (value < Data)
    {
        return Left != null && Left.Contains(value);
    }
    else // value > Data
    {
        return Right != null && Right.Contains(value);
    }
}


    public int GetHeight()
    {
        int leftHeight = Left?.GetHeight() ?? 0;
        int rightHeight = Right?.GetHeight() ?? 0;
        return 1 + Math.Max(leftHeight, rightHeight);
    }

}