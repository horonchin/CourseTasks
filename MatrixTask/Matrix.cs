using System.Text;
using VectorTask;

namespace MatrixTask;

public class Matrix
{
	private Vector[] _rows;

	public int RowsCount => _rows.Length;

	public int ColumnsCount => _rows[0].Size;

	public Matrix(int rowsCount, int columnsCount)
	{
		if (columnsCount <= 0)
		{
			throw new ArgumentException($"Количество столбцов матрицы должно быть положительным. Количество столбцов: {columnsCount}", nameof(columnsCount));
		}

		if (rowsCount <= 0)
		{
			throw new ArgumentException($"Количество строк матрицы должно быть положительным. Количество строк: {rowsCount}", nameof(rowsCount));
		}

		_rows = new Vector[rowsCount];

		for (int i = 0; i < rowsCount; i++)
		{
			_rows[i] = new Vector(columnsCount);
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

		int rowsCount = array.GetLength(0);
		int columnsCount = array.GetLength(1);

		if (rowsCount == 0 || columnsCount == 0)
		{
			throw new ArgumentException("Матрица не может быть нулевого размера", nameof(array));
		}

		_rows = new Vector[rowsCount];

		for (int i = 0; i < rowsCount; i++)
		{
			Vector row = new Vector(columnsCount);

			for (int j = 0; j < columnsCount; j++)
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
			throw new ArgumentException($"Массив векторов не может быть пустым. Длина: {vectors.Length}", nameof(vectors));
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
				throw new ArgumentException($"Размерность вектора ({value.Size}) не соответствует числу столбцов ({ColumnsCount})", nameof(value));
			}

			_rows[rowIndex] = new Vector(value);
		}
	}

	public double this[int rowIndex, int columnIndex]
	{
		get
		{
			if (rowIndex < 0 || rowIndex >= RowsCount)
			{
				throw new IndexOutOfRangeException($"Индекс строки {rowIndex} вне границ (0-{RowsCount - 1})");
			}

			if (columnIndex < 0 || columnIndex >= ColumnsCount)
			{
				throw new IndexOutOfRangeException($"Индекс столбца {columnIndex} вне границ (0-{ColumnsCount - 1})");
			}

			return _rows[rowIndex][columnIndex];
		}
		set
		{
			if (rowIndex < 0 || rowIndex >= RowsCount)
			{
				throw new IndexOutOfRangeException($"Индекс строки {rowIndex} вне границ (0-{RowsCount - 1})");
			}

			if (columnIndex < 0 || columnIndex >= ColumnsCount)
			{
				throw new IndexOutOfRangeException($"Индекс столбца {columnIndex} вне границ (0-{ColumnsCount - 1})");
			}

			_rows[rowIndex][columnIndex] = value;
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
		Vector[] transposedRows = new Vector[ColumnsCount];

		for (int i = 0; i < ColumnsCount; i++)
		{
			transposedRows[i] = GetColumn(i);
		}

		_rows = transposedRows;
	}

	public double GetDeterminant()
	{
		if (RowsCount != ColumnsCount)
		{
			throw new InvalidOperationException($"Определитель существует только для квадратных матриц. Количество строк: {RowsCount}, количество столбцов: {ColumnsCount}");
		}

		return CalculateDeterminant(_rows);
	}

	private static double CalculateDeterminant(Vector[] rows)
	{
		int size = rows.Length;

		if (size == 1)
		{
			return rows[0][0];
		}

		if (size == 2)
		{
			return rows[0][0] * rows[1][1] - rows[0][1] * rows[1][0];
		}

		double determinant = 0;
		int sign = 1;

		Vector[] submatrix = new Vector[size - 1];

		for (int i = 0; i < size - 1; i++)
		{
			submatrix[i] = new Vector(size - 1);
		}

		for (int i = 0; i < size; i++)
		{
			for (int j = 1; j < size; j++)
			{
				Vector row = submatrix[j - 1];
				int columnIndex = 0;

				for (int k = 0; k < size; k++)
				{
					if (k == i)
					{
						continue;
					}

					row[columnIndex] = rows[j][k];
					columnIndex++;
				}
			}

			determinant += sign * rows[0][i] * CalculateDeterminant(submatrix);
			sign = -sign;
		}

		return determinant;
	}

	public override string ToString()
	{
		StringBuilder result = new StringBuilder();
		result.Append('{').Append(_rows[0]);

		for (int i = 1; i < _rows.Length; i++)
		{
			result.Append(", ").Append(_rows[i]);
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

	public Vector GetMultiply(Vector vector)
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

	private static void ValidateSameSize(Matrix matrix1, Matrix matrix2)
	{
		if (matrix1.RowsCount != matrix2.RowsCount || matrix1.ColumnsCount != matrix2.ColumnsCount)
		{
			throw new ArgumentException($"Нельзя выполнить операцию: матрицы разных размеров {matrix1.RowsCount}x{matrix1.ColumnsCount} и {matrix2.RowsCount}x{matrix2.ColumnsCount}", nameof(matrix2));
		}
	}

	public void Add(Matrix matrix)
	{
		ValidateSameSize(this, matrix);

		for (int i = 0; i < RowsCount; i++)
		{
			_rows[i].Add(matrix._rows[i]);
		}
	}

	public void Subtract(Matrix matrix)
	{
		ValidateSameSize(this, matrix);

		for (int i = 0; i < RowsCount; i++)
		{
			_rows[i].Subtract(matrix._rows[i]);
		}
	}

	public static Matrix GetSum(Matrix matrix1, Matrix matrix2)
	{
		ValidateSameSize(matrix1, matrix2);

		Matrix result = new Matrix(matrix1);
		result.Add(matrix2);

		return result;
	}

	public static Matrix GetDifference(Matrix matrix1, Matrix matrix2)
	{
		ValidateSameSize(matrix1, matrix2);

		Matrix result = new Matrix(matrix1);
		result.Subtract(matrix2);

		return result;
	}

	public static Matrix GetMultiply(Matrix matrix1, Matrix matrix2)
	{
		if (matrix1.ColumnsCount != matrix2.RowsCount)
		{
			throw new ArgumentException($"Невозможно умножить матрицы: первая {matrix1.RowsCount}x{matrix1.ColumnsCount}, вторая {matrix2.RowsCount}x{matrix2.ColumnsCount}. Число столбцов первой должно равняться числу строк второй", nameof(matrix2));
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