namespace MatrixDivider
{
    class Program
    {
        public static int rows;
        public static int cols;
        static void Main()
        {
            Console.WriteLine("Welcome to matrix-divider.\nWe will divide two matrices you choose.");
            while (true)
            {
                try
                {
                    Console.Write("Enter rows: ");
                    rows = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter columns: ");
                    cols = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch 
                {
                    Console.WriteLine("Wrong input.");
                }
            }
            if (rows != cols)
            {
                Console.WriteLine("Collumns and rows must be equal.");
                return;
            }
            // Initializing arrays
            double [,] arr1 = new double [rows, cols];
            double [,] arr2 = new double [rows, cols];
            double [,] identity = new double [rows, cols];
            double [,] output;

            // Calling functions
            GettingData(ref arr1, "first");
            GettingData(ref arr2, "second");
            MakeIdentity(ref identity);
            Inverse(ref arr2, identity);
            Multiply(ref arr1, ref arr2, out output);
            PrintingData(ref output);
        }
        // Reading the matrices from the user
        public static void GettingData(ref double [,] arr, string order)
        {
            Console.WriteLine($"---The {order} matrix---");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write($"Enter element[{i + 1}, {j + 1}]: ");
                            arr[i, j] = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Wrong input.");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        // Creating the unity matrix
        public static void MakeIdentity(ref double [,] arr)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == j)
                    {
                        arr[i, j] = 1;
                        break;

                    }
                }
            }
        }

        // Exit the program if the second matrix does not have an inverse
        public static void Exit()
        {
            Console.WriteLine("Couldn't complete the division. Second matrix does not have an inverse.");
            System.Environment.Exit(1);
        }

        // If there is a zero in the first diagonal element
        public static void Swap(ref double [,] arr)
        {
            if (arr[0, 0] == 0d)
            {
                double tmp;
                int arrRows = arr.GetLength(0);

                for (int i = 0; i < arrRows; i++)
                {
                    for (int k = i + 1; k < arrRows; k++)
                    {
                        for (int j = 0, arrCols = arr.GetLength(1); j < arrCols; j++)
                        {
                            tmp = arr[i, j];
                            arr[i, j] = arr[k, j];
                            arr[k, j] = tmp;
                        }

                        if (arr[0, 0] != 0d)
                        {
                            goto Finish;
                        }
                    }
                }
                if (arr[0, 0] == 0d)
                {
                    Exit();
                }
            }
            Finish:
                return;
        }
        // Doing gauss-jordan elimination
        public static void elimination(int type, int i, double multiply, ref double [,] arr)
        {
            
            for (int j = 0; j < cols * 2; j++)
            {
            // the argument, type is here to define what we want to do,
            // iterates from top to bottom or otherwise
            arr[type, j] -= arr[i, j] *
            (multiply / arr[i, i]);

            }
        }

        // Getting the inverse of the second matrix to multiply it with the first.
        public static void Inverse(ref double [,] arr, double [,] unity)
        {
            // Preparing the array for gauss-jordan elemination method
            // Appending unity elements
            double [,] mixedArr = new double [rows, cols * 2];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    mixedArr[i, j] = arr[i, j];
                }
                for (int k = cols; k < cols * 2; k++)
                {
                    mixedArr[i, k] = unity[i, k - cols];
                }
            }

            Swap(ref mixedArr);
            for (int i = 0; i < rows; i++)
            {
                // For every single row after the row which iterates
                for (int k = i + 1; k < rows; k++)
                {
                    // assigining it to a variable because its value changes after
                    // first iteration and we want the original value not the modified
                    double value = mixedArr[k, i];
                    elimination(k ,i , value, ref mixedArr);
                }
                // This for rows before the iterating row
                for (int k = 0; k < i; k++)
                {
                    // same as above
                    double value = mixedArr[k, i];
                    elimination(k ,i , value, ref mixedArr);            
                }
            }  
            // Updating the array
            for (int i = 0; i < rows; i++)
            {
                for (int j = cols; j < cols * 2; j++)
                {
                    // mixedArr[i, i] is any element in the main diagonal
                    // We divide by it, to convert the diagonal elements to ones.
                    // So, we get the unity matrix in the left side of the mixedArr
                    // therefore, we get the inverse of the matrix successfully.
                    arr[i, j - cols] = mixedArr[i, j] / mixedArr[i, i];
                    if (arr[i, j - cols] == double.NaN)
                    {
                        Exit();
                    }
                }
            }
        }

        // multiplying the first array by the inversed array
        public static void Multiply(ref double [,] matrix1, ref double [,] matrix2, out double [,] result)
        {
            // Math rules
            int resultRows = matrix1.GetLength(0);
            int resultCols = matrix2.GetLength(1);
            result = new double [resultRows, resultCols];
            double sum;

            for (int k = 0; k < resultCols; k++)
            {
                
                for (int i = 0; i < resultRows; i++)
                {
                    sum = 0;
                    for (int j = 0; j < resultCols; j++)
                    {
                        sum += matrix1[i, j] * matrix2[j, k];
                    }
                    result[i, k] = sum;
                }
            }
        }

        // Now the final step
        // Printing the result
        public static void PrintingData(ref double [,] result)
        {
            Console.WriteLine("---The result matrix---");
            for (int i = 0, resultRows = result.GetLength(0); i < resultRows; i++)
            {
                for (int j = 0, resultCols = result.GetLength(1); j < resultCols; j++)
                {
                    // For allignment
                    string messeage = String.Format("{0,-8}\t", Math.Round(result[i, j], 3));
                    Console.Write(messeage);
                }
                Console.WriteLine();
            }
        }
    }
}