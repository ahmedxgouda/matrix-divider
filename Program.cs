Console.Write("Enter rows: ");
int rows = Convert.ToInt32(Console.ReadLine());
Console.Write("Enter columns: ");
int cols = Convert.ToInt32(Console.ReadLine());

double [,] arr1 = new double [rows, cols];
double [,] arr2 = new double [rows, cols];
double [,] unity = new double [rows, cols];

GettingData(ref arr1, "first");
GettingData(ref arr2, "second");
MakeUnity(ref unity);
Inverse(ref arr2);
// Getting the inverse of the second matrix

void GettingData(ref double [,] arr, string order)
{
    Console.WriteLine($"---The {order} matrix---");
    int rowsLength = arr.GetLength(0);
    int colsLength = arr.GetLength(1);
    for (int i = 0; i < rowsLength; i++)
    {
        for (int j = 0; j < colsLength; j++)
        {
            Console.Write($"Enter element[{i + 1}, {j + 1}]: ");
            arr[i, j] = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine();
    }
}
void MakeUnity(ref double [,] arr)
{
    int rowsLength = arr.GetLength(0);
    int colsLength = arr.GetLength(1);
    for (int i = 0; i < rowsLength; i++)
    {
        for (int j = 0; j < colsLength; j++)
        {
            if (i == j)
            {
                arr[i, j] = 1;
                break;

            }
        }
    }
}
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
    void elimination(int order, int i, double multiply)
    {
        for (int j = 0; j < cols * 2; j++)
        {           
            mixedArr[order, j] -= mixedArr[i, j] *
            (multiply / mixedArr[i, i]);
        }
    }
    // Doing gauss-jordan elimination
    for (int i = 0; i < rows; i++)
    {
        for (int k = i + 1; k < rows; k++)
        {
            double value = mixedArr[k, i];
            elimination(k ,i , value);
        }
        for (int k = 0; k < i; k++)
        {
            double value = mixedArr[k, i];
            elimination(k ,i , value);            
        }
    }  
    // Updating the array
    for (int i = 0; i < rows; i++)
    {
        for (int j = cols; j < cols * 2; j++)
        {
            arr[i, j - cols] = Math.Round(mixedArr[i, j] / mixedArr[i, i], 1);
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