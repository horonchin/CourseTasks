using VectorTask;

namespace MatrixTask;

class Program
{
	public static void Main()
	{
		Matrix matrix1 = new Matrix(3, 4);
		Console.WriteLine(matrix1);

		Matrix matrix2 = new Matrix(matrix1);
		Console.WriteLine(matrix2);

		double[,] array = new double[3, 5];
		Matrix matrix3 = new Matrix(array);
		Console.WriteLine(matrix3);

		Vector vector1 = new Vector(1);
		Vector vector2 = new Vector(2);
		Vector vector3 = new Vector(3);
		Vector[] vectors = [vector1, vector2, vector3];
		Matrix matrix4 = new Matrix(vectors);
		Console.WriteLine(matrix4);

		Console.WriteLine($"Первая строка матрицы matrix4 равна: {matrix4[0]}");

		double[] array2 = { 1, 2, 3 };
		Vector vector4 = new Vector(array2);
		matrix4[1] = vector4;
		Console.WriteLine($"Вторая строка матрицы matrix4 была изменена и теперь равна: {matrix4[1]}");

		Console.WriteLine($"Первый столбец матрицы matrix4 равен: {matrix4.GetColumn(0)}");

		matrix4.Transpose();
		Console.WriteLine($"Транспонированная матрица от matrix4 равна: {matrix4}");

		double[] array3 = { 0, 1, 4 };
		Vector vector5 = new Vector(array3);
		matrix4[2] = vector5;

		double[] array4 = { 0, 0, 1 };
		Vector vector6 = new Vector(array4);
		matrix4[0] = vector6;
		Console.WriteLine($"Определитель матрицы {matrix4} равен: {matrix4.GetDeterminant()}");

		Console.WriteLine($"Умножение матрицы {matrix4} на вектор {vector4} равно: {matrix4.Multiply(vector4)}");

		Matrix matrix5 = new Matrix(matrix4);

		matrix4.MultiplyByScalar(5);
		Console.WriteLine($"Умножение матрицы matrix4 на 5 равно: {matrix4}");

		matrix4.Add(matrix5);
		Console.WriteLine($"Сумма matrix4 и matrix5 равна: {matrix4}");

		matrix4.Subtract(matrix5);
		Console.WriteLine($"Разность matrix4 и matrix5 равна: {matrix4}");

		Console.WriteLine($"Сумма matrix4 и matrix5 равна: {Matrix.GetSum(matrix4, matrix5)}");

		Console.WriteLine($"Разность matrix4 и matrix5 равна: {Matrix.GetDifference(matrix4, matrix5)}");

		Console.WriteLine($"Произведение matrix4 и matrix5 равно: {Matrix.Multiply(matrix4, matrix5)}");
	}
}