using VectorTask;

namespace MatrixTask;


class Matrix
{
	private readonly Vector[] _vectors;

	public int Rows => _vectors.Length;

	public int Columns => _vectors[0].Size;


	public Matrix(int rows, int columns)
	{
		if (rows <= 0 || columns <= 0)
		{
			throw new ArgumentException("Размеры матрицы должны быть положительны.");
		}

		_vectors = new Vector[rows];

		for (int i = 0; i < columns; i++)
		{
			_vectors[i] = new Vector(columns);
		}
	}

	public Matrix(Matrix matrix)
	{
		ArgumentNullException.ThrowIfNull(matrix);

		if (matrix._vectors == null)
		{
			throw new InvalidOperationException("Исходная матрица повреждена.");
		}

		_vectors = new Vector[matrix.Rows];

		for (int i = 0; i < matrix.Rows; i++)
		{
			_vectors[i] = matrix._vectors[i];
		}
	}

	public Matrix(double[,] array)
	{
		ArgumentNullException.ThrowIfNull(array);

		int rows = array.GetLength(0);
		int columns = array.GetLength(1);

		_vectors = new Vector[rows];

		for (int i = 0; i < rows; i++)
		{
			double[] row = new double[columns];

			for (int j = 0; j < columns; j++)
			{
				row[j] = array[i, j];
			}

			_vectors[i] = new Vector(row);
		}
	}

	public Matrix(Vector[] vectors)
	{
		ArgumentNullException.ThrowIfNull(vectors);

		if (vectors.Length == 0)
		{
			throw new ArgumentException("Массив векторов не может быть пустым", nameof(vectors));
		}

		_vectors = new Vector[vectors.Length];

		for (int i = 0; i < vectors.Length; i++)
		{
			_vectors[i] = new Vector(vectors[i]);
		}
	}

	public Vector this[int rowIndex]
	{
		get
		{
			if (rowIndex <= 0 || rowIndex >= Rows)
			{
				throw new IndexOutOfRangeException($"Индекс строки {rowIndex} вне границ (0-{Rows - 1})");
			}

			return new Vector(_vectors[rowIndex]);
		}

		set
		{
			if (rowIndex <= 0 || rowIndex >= Rows)
			{
				throw new IndexOutOfRangeException($"Индекс строки {rowIndex} вне границ (0-{Rows - 1})");
			}

			ArgumentNullException.ThrowIfNull(value);

			if (value.Size != Columns)
			{
				throw new ArgumentException($"Длина вектора ({value.Size}) не соответствует числу столбцов ({Columns})");
			}

			_vectors[rowIndex] = new Vector(value);
		}
	}

	public Vector GetColumn(int columnIndex)
	{
		if (columnIndex < 0 || columnIndex >= Columns)
		{
			throw new IndexOutOfRangeException($"Индекс столбца {columnIndex} вне границ (0-{Columns - 1})");
		}

		double[] columnData = new double[Rows];

		for (int i = 0; i < Rows; i++)
		{
			columnData[i] = _vectors[i][columnIndex];
		}

		return new Vector(columnData);
	}

	public Matrix Transpose()
	{
		int rows = Rows;
		int columns = Columns;

		double[,] transposed = new double[rows, columns];

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				transposed[j, i] = _vectors[i][j];
			}
		}

		return new Matrix(transposed);
	}

	public double Determinant()
	{
		if (Rows != Columns)
			throw new InvalidOperationException("Определитель существует только для квадратных матриц");

		return CalculateDeterminant(_vectors);
	}

	private static double CalculateDeterminant(Vector[] matrix)
	{
		int n = matrix.Length;

		if (n == 1)
			return matrix[0][0];

		if (n == 2)
			return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];

		double det = 0;
		int sign = 1;

		for (int j = 0; j < n; j++)
		{
			Vector[] minor = new Vector[n - 1];

			for (int i = 1; i < n; i++)
			{
				double[] rowData = new double[n - 1];
				int colIndex = 0;

				for (int k = 0; k < n; k++)
				{
					if (k == j)
					{
						continue;
					}

					rowData[colIndex++] = matrix[i][k];
				}

				minor[i - 1] = new Vector(rowData);
			}

			det += sign * matrix[0][j] * CalculateDeterminant(minor);
			sign = -sign;
		}

		return det;
	}

	public override string ToString()
	{
		if (_vectors == null || _vectors.Length == 0)
		{
			return "{}";
		}

		string[] rows = new string[_vectors.Length];

		for (int i = 0; i < _vectors.Length; i++)
		{
			rows[i] = _vectors[i].ToString();
		}

		return $"{{{string.Join(", ", rows)}}}";
	}

	public void MultiplyByScalar(double scalar)
	{
		for (int i = 0; i < Rows; i++)
		{
			_vectors[i].Multiply(scalar);
		}
	}

	public Vector GetMultiplyByVector(Vector vector)
	{
		if (vector.Size != Columns)
		{
			throw new ArgumentException("Невозможно умножить матрицу на данный вектор.");
		}
		else
		{
			Vector result = new Vector(Rows);

			for (int i = 0; i < Rows; i++)
			{
				double sum = 0;

				for (int j = 0; j < Columns; j++)
				{
					sum += _vectors[i][j] * vector[j];
				}

				result[i] = sum;
			}

			return result;
		}
	}

	public void Add(Matrix matrix)
	{
		if (Rows != matrix.Rows || Columns != matrix.Columns)
		{
			throw new ArgumentException("Невозможно сложить матрицы. Их размерности отличаются.");
		}
		else
		{
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					_vectors[i][j] += matrix._vectors[i][j];
				}
			}
		}
	}

	public void Substract(Matrix matrix)
	{
		if (Rows != matrix.Rows || Columns != matrix.Columns)
		{
			throw new ArgumentException("Невозможно вычесть матрицы. Их размерности отличаются.");
		}
		else
		{
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					_vectors[i][j] -= matrix._vectors[i][j];
				}
			}
		}
	}

	public static Matrix GetSum(Matrix matrix1, Matrix matrix2)
	{
		Matrix result = new Matrix(matrix1);

		result.Add(matrix2);

		return result;
	}

	public static Matrix GetDifference(Matrix matrix1, Matrix matrix2)
	{
		Matrix result = new Matrix(matrix1);

		result.Substract(matrix2);

		return result;
	}

	public static Matrix GetMultiplyByMatrix(Matrix matrix1, Matrix matrix2)
	{
		if (matrix1.Columns != matrix2.Rows)
			throw new ArgumentException("Количество столбцов первой матрицы должно равняться количеству строк второй");

		Matrix result = new Matrix(matrix1.Rows, matrix2.Columns);

		for (int i = 0; i < matrix1.Rows; i++)
		{
			for (int j = 0; j < matrix2.Columns; j++)
			{
				double sum = 0;

				for (int k = 0; k < matrix1.Columns; k++)
				{
					sum += matrix1._vectors[i][k] * matrix2._vectors[k][j];
				}

				result[i][j] = sum;
			}
		}

		return result;
	}
}