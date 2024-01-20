class Program
{
    // Print a number of n empty spaces
    static void Pad(int n)
    {
        for (int i = 0; i < n; i++)
            Console.Write(" ");
    }
    static void DrawHeap(string[] A)
    {
        // The height of the heap is log2(n)
        int height = (int)Math.Log2(A.Length) + 1;

        int index = 0;
        int row = 0;
        while (index < A.Length)
        {
            // == Print the node values ==
            // The first number from the left needs to be padded with 2 to the power of height of the row - 1 characters
            int padLeft = (int)Math.Pow(2, height - row) - 1;
            int limit = (int)Math.Pow(2, row);
            for (int i = 0; i < limit && index < A.Length; i++)
            {
                if (i == 0)
                    Pad(padLeft - A[index].ToString().Length + 1);  // a completely different padding for the very first node value
                else
                    Pad(2 * padLeft + 1 - A[index].ToString().Length + 1);  // same padding for the remaining node values
                Console.Write("{0}", A[index]);
                index++;
            }
            Console.WriteLine();
            // == End printing the node values ==
            // == Print the edges ==
            if (row != height - 1)  // use special caret symbols only for the last row
            {
                // First print the left white space
                int nextPadLeft = (int)Math.Pow(2, height - row - 1) - 1;
                Pad((padLeft + nextPadLeft) / 2);
                //Console.WriteLine("index={0} index_last={1} Math.Pow(2, row)={2}", index, index-Math.Pow(2, row), Math.Pow(2, row));
                for (int k = 0; k < (int)Math.Pow(2, row) && index + k < A.Length; k++)
                {
                    if (k >= 1)
                    {
                        // Next compute if needed the white space in the "no-man's-land" patches between "exterior" edges
                        Pad((padLeft + 2 * padLeft + 1) / 2);
                    }
                    Console.Write("/");
                    // Now print the white space in-between the 2 edges coming down from the node above
                    Pad(nextPadLeft);
                    Console.Write("\\");
                }
            }
            Console.WriteLine();
            // == End printing the edges ==
            row++;
        }
        Console.WriteLine();
    }

    public static void Main()
    {
        string[] A = { "16", "4", "10", "14", "7", "9", "3", "2", "8", "1" };  // yep, it's the CLRS Max-Heapify sample array
        DrawHeap(A);
    }
}

