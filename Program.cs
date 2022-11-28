Console.Write("Enter rows: ");
int rows = Convert.ToInt32(Console.ReadLine());
Console.Write("Enter columns: ");
int cols = Convert.ToInt32(Console.ReadLine());

// Initializing arrays
double [,] arr1 = new double [rows, cols];
double [,] arr2 = new double [rows, cols];
double [,] unity = new double [rows, cols];

// Calling functions
GettingData(ref arr1, "first");
GettingData(ref arr2, "second");
MakeUnity(ref unity);
Inverse(ref arr2);

// Reading the matrices from the user
void GettingData(ref double [,] arr, string order)
{
    Console.WriteLine($"---The {order} matrix---");
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Console.Write($"Enter element[{i + 1}, {j + 1}]: ");
            arr[i, j] = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine();
    }
}

// Creating the unity matrix

void MakeUnity(ref double [,] arr)
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

// Getting the inverse of the second matrix to multiply it with the first.
void Inverse(ref double [,] arr)
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
    // Doing gauss-jordan elimination
    void elimination(int type, int i, double multiply)
    {
        for (int j = 0; j < cols * 2; j++)
        {
            try
            {
            // the argument, type is here to define what we want to do,
            // iterates from top to bottom or otherwise
            mixedArr[type, j] -= mixedArr[i, j] *
            (multiply / mixedArr[i, i]);
            }
            catch
            {
                Console.WriteLine("Can't find the inverse of this matrix.");
                Console.WriteLine("Please, enter valid data.");
                return;
            }
        }
    }
    
    for (int i = 0; i < rows; i++)
    {
        // For every single row after the row which iterates
        for (int k = i + 1; k < rows; k++)
        {
            // assigining it to a variable because its value changes after
            // first iteration and we want the original value not the modified
            double value = mixedArr[k, i];
            elimination(k ,i , value);
        }
        // This for rows before the iterating row
        for (int k = 0; k < i; k++)
        {
            // same as above
            double value = mixedArr[k, i];
            elimination(k ,i , value);            
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
            arr[i, j - cols] = Math.Round(mixedArr[i, j] / mixedArr[i, i], 3);
        }
    }
}
for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < cols; j++)
    {
        Console.Write($"{arr2[i, j]}\t");
    }
    Console.WriteLine();
}