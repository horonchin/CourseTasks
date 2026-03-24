using VectorTask;

namespace MatrixTask;

class Matrix
{
	private readonly Vector[] _rows;

	public int RowsCount => _rows.Length;

	public int ColumnsCount => _rows[0].Size;

	public Matrix(int rows, int columns)
	{
		if (columns <= 0)
		{
			throw new ArgumentException($"Количество столбцов матрицы должно быть положительным. Столбцы: {columns}");
		}

		if (rows <= 0)
		{
			throw new ArgumentException($"Количество строк матрицы должно быть положительным. Строки: {rows}");
		}

		_rows = new Vector[rows];

		for (int i = 0; i < rows; i++)
		{
			_rows[i] = new Vector(columns);
		}
	}

	public Matrix(Matrix matrix)
	{
		ArgumentNullException.ThrowIfNull(matrix);

		_rows = new Vector[matrix.RowsCount];

		for (int i = 0; i < matrix.RowsCount; i++)
		{
			_rows[i] = new Vector(matrix._rows[i]);
		}
	}

	public Matrix(double[,] array)
	{
		ArgumentNullException.ThrowIfNull(array);

		int rows = array.GetLength(0);
		int columns = array.GetLength(1);

		if (rows == 0 || columns == 0)
		{
			throw new ArgumentException("Матрица не может быть нулевого размера", nameof(array));
		}

		_rows = new Vector[rows];

		for (int i = 0; i < rows; i++)
		{
			Vector row = new Vector(columns);

			for (int j = 0; j < columns; j++)
			{
				row[j] = array[i, j];
			}

			_rows[i] = row;
		}
	}

	public Matrix(Vector[] vectors)
	{
		ArgumentNullException.ThrowIfNull(vectors);

		if (vectors.Length == 0)
		{
			throw new ArgumentException("Массив векторов не может быть пустым", nameof(vectors));
		}

		int maxVectorSize = vectors[0].Size;

		for (int i = 1; i < vectors.Length; i++)
		{
			if (vectors[i].Size > maxVectorSize)
			{
				maxVectorSize = vectors[i].Size;
			}
		}

		_rows = new Vector[vectors.Length];

		for (int i = 0; i < vectors.Length; i++)
		{
			if (vectors[i].Size == maxVectorSize)
			{
				_rows[i] = new Vector(vectors[i]);
			}
			else
			{
				Vector expandedVector = new Vector(maxVectorSize);

				for (int j = 0; j < vectors[i].Size; j++)
				{
					expandedVector[j] = vectors[i][j];
				}

				_rows[i] = expandedVector;
			}
		}
	}

	public Vector this[int rowIndex]
	{
		get
		{
			if (rowIndex < 0 || rowIndex >= RowsCount)
			{
				throw new IndexOutOfRangeException($"Индекс строки {rowIndex} вне границ (0-{RowsCount - 1})");
			}

			return new Vector(_rows[rowIndex]);
		}

		set
		{
			if (rowIndex < 0 || rowIndex >= RowsCount)
			{
				throw new IndexOutOfRangeException($"Индекс строки {rowIndex} вне границ (0-{RowsCount - 1})");
			}

			ArgumentNullException.ThrowIfNull(value);

			if (value.Size != ColumnsCount)
			{
				throw new ArgumentException($"Длина вектора ({value.Size}) не соответствует числу столбцов ({ColumnsCount})");
			}

			_rows[rowIndex] = new Vector(value);
		}
	}

	public double this[int row, int column]
	{
		get
		{
			if (row < 0 || row >= RowsCount)
			{
				throw new IndexOutOfRangeException($"Индекс строки {row} вне границ");
			}

			if (column < 0 || column >= ColumnsCount)
			{
				throw new IndexOutOfRangeException($"Индекс столбца {column} вне границ");
			}

			return _rows[row][column];
		}
		set
		{
			if (row < 0 || row >= RowsCount)
			{
				throw new IndexOutOfRangeException($"Индекс строки {row} вне границ");
			}

			if (column < 0 || column >= ColumnsCount)
			{
				throw new IndexOutOfRangeException($"Индекс столбца {column} вне границ");
			}

			_rows[row][column] = value;
		}
	}

	public Vector GetColumn(int columnIndex)
	{
		if (columnIndex < 0 || columnIndex >= ColumnsCount)
		{
			throw new IndexOutOfRangeException($"Индекс столбца {columnIndex} вне границ (0-{ColumnsCount - 1})");
		}

		Vector columnVector = new Vector(RowsCount);

		for (int i = 0; i < RowsCount; i++)
		{
			columnVector[i] = _rows[i][columnIndex];
		}

		return columnVector;
	}

	public void Transpose()
	{
		if (RowsCount != ColumnsCount)
		{
			throw new InvalidOperationException("In-place транспонирование возможно только для квадратных матриц");
		}

		for (int i = 0; i < RowsCount; i++)
		{
			for (int j = i + 1; j < ColumnsCount; j++)
			{
				double temp = _rows[i][j];
				_rows[i][j] = _rows[j][i];
				_rows[j][i] = temp;
			}
		}
	}

	public double GetDeterminant()
	{
		if (RowsCount != ColumnsCount)
		{
			throw new InvalidOperationException("Определитель существует только для квадратных матриц. Количество строк не равно количеству столбцов.");
		}

		return CalculateDeterminant(_rows);
	}

	private static double CalculateDeterminant(Vector[] rows)
	{
		int n = rows.Length;

		if (n == 1)
		{
			return rows[0][0];
		}

		if (n == 2)
		{
			return rows[0][0] * rows[1][1] - rows[0][1] * rows[1][0];
		}

		double determinant = 0;
		int sign = 1;

		Vector[] submatrix = new Vector[n - 1];

		for (int i = 0; i < n; i++)
		{
			for (int j = 1; j < n; j++)
			{
				Vector rowVector = new Vector(n - 1);
				int columnIndex = 0;

				for (int k = 0; k < n; k++)
				{
					if (k == i)
					{
						continue;
					}

					rowVector[columnIndex] = rows[j][k];
					columnIndex++;
				}

				submatrix[j - 1] = rowVector;
			}

			determinant += sign * rows[0][i] * CalculateDeterminant(submatrix);
			sign = -sign;
		}

		return determinant;
	}

	public override string ToString()
	{
		if (_rows.Length == 0)
		{
			return "{}";
		}

		var result = new StringBuilder();
		result.Append('{');

		for (int i = 0; i < _rows.Length; i++)
		{
			if (i > 0)
			{
				result.Append(", ");
			}

			result.Append(_rows[i].ToString());
		}

		result.Append('}');

		return result.ToString();
	}

	public void MultiplyByScalar(double scalar)
	{
		foreach (Vector row in _rows)
		{
			row.Multiply(scalar);
		}
	}

	public Vector Multiply(Vector vector)
	{
		if (vector.Size != ColumnsCount)
		{
			throw new ArgumentException($"Невозможно умножить матрицу на данный вектор. Размер вектора: {vector.Size}, требуется: {ColumnsCount}", nameof(vector));
		}

		Vector result = new Vector(RowsCount);

		for (int i = 0; i < RowsCount; i++)
		{
			result[i] = Vector.GetDotProduct(_rows[i], vector);
		}

		return result;
	}

	public void Add(Matrix matrix)
	{
		if (RowsCount != matrix.RowsCount || ColumnsCount != matrix.ColumnsCount)
		{
			throw new ArgumentException("Невозможно сложить матрицы. Их размерности отличаются.");
		}

		for (int i = 0; i < RowsCount; i++)
		{
			_rows[i].Add(matrix._rows[i]);
		}
	}

	public void Subtract(Matrix matrix)
	{
		if (RowsCount != matrix.RowsCount || ColumnsCount != matrix.ColumnsCount)
		{
			throw new ArgumentException("Невозможно вычесть матрицы. Их размерности отличаются.");
		}

		for (int i = 0; i < RowsCount; i++)
		{
			_rows[i].Subtract(matrix._rows[i]);
		}
	}

	public static Matrix GetSum(Matrix matrix1, Matrix matrix2)
	{
		if (matrix1.RowsCount != matrix2.RowsCount || matrix1.ColumnsCount != matrix2.ColumnsCount)
		{
			throw new ArgumentException("Невозможно сложить матрицы. Их размерности отличаются.");
		}

		Matrix result = new Matrix(matrix1);
		result.Add(matrix2);

		return result;
	}

	public static Matrix GetDifference(Matrix matrix1, Matrix matrix2)
	{
		if (matrix1.RowsCount != matrix2.RowsCount || matrix1.ColumnsCount != matrix2.ColumnsCount)
		{
			throw new ArgumentException("Невозможно вычесть матрицы. Их размерности отличаются.");
		}

		Matrix result = new Matrix(matrix1);
		result.Subtract(matrix2);

		return result;
	}

	public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
	{
		if (matrix1.ColumnsCount != matrix2.RowsCount)
		{
			throw new ArgumentException("Количество столбцов первой матрицы должно равняться количеству строк второй");
		}

		Matrix result = new Matrix(matrix1.RowsCount, matrix2.ColumnsCount);

		for (int i = 0; i < matrix1.RowsCount; i++)
		{
			for (int j = 0; j < matrix2.ColumnsCount; j++)
			{
				double sum = 0;

				for (int k = 0; k < matrix1.ColumnsCount; k++)
				{
					sum += matrix1[i, k] * matrix2[k, j];
				}

				result[i, j] = sum;
			}
		}

		return result;
	}

	public override int GetHashCode()
	{
		const int prime = 37;
		int hash = 1;

		foreach (Vector row in _rows)
		{
			hash = hash * prime + row.GetHashCode();
		}

		return hash;
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(obj, this))
		{
			return true;
		}

		if (obj is null || GetType() != obj.GetType())
		{
			return false;
		}

		Matrix other = (Matrix)obj;

		if (RowsCount != other.RowsCount || ColumnsCount != other.ColumnsCount)
		{
			return false;
		}

		for (int i = 0; i < RowsCount; i++)
		{
			if (!_rows[i].Equals(other._rows[i]))
			{
				return false;
			}
		}

		return true;
	}
}