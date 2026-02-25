namespace VectorTask;

class Vector
{
	private double[] _components;

	public int Size => _components.Length;

	public Vector(int vectorSize)
	{
		if (vectorSize <= 0)
		{
			throw new ArgumentException($"Длина вектора должна быть положительной: {vectorSize}", nameof(vectorSize));
		}

		_components = new double[vectorSize];
	}

	public Vector(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		_components = new double[vector.Size];
		Array.Copy(vector._components, _components, vector.Size);
	}

	public Vector(double[] components)
	{
		ArgumentNullException.ThrowIfNull(components);

		if (components.Length == 0)
		{
			throw new ArgumentException("Вектор не может быть нулевой размерности", nameof(components));
		}

		_components = new double[components.Length];
		Array.Copy(components, _components, components.Length);
	}

	public Vector(int vectorSize, double[] components)
	{
		ArgumentNullException.ThrowIfNull(components);

		if (vectorSize <= 0)
		{
			throw new ArgumentException($"Длина вектора должна быть положительной: {vectorSize}", nameof(vectorSize));
		}

		_components = new double[vectorSize];

		int copyLength = Math.Min(vectorSize, components.Length);
		Array.Copy(components, 0, _components, 0, copyLength);
	}

	public override string ToString()
	{
		return $"{{{string.Join(", ", _components)}}}";
	}

	public double GetLength()
	{
		double sum = 0;

		foreach (double e in _components)
		{
			sum += e * e;
		}

		return Math.Sqrt(sum);
	}

	public void Add(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		if (vector.Size > Size)
		{
			Array.Resize(ref _components, vector.Size);
		}

		for (int i = 0; i < vector.Size; i++)
		{
			_components[i] += vector._components[i];
		}
	}

	public void Subtract(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		if (vector.Size > Size)
		{
			Array.Resize(ref _components, vector.Size);
		}

		for (int i = 0; i < vector.Size; i++)
		{
			_components[i] -= vector._components[i];
		}
	}

	public static Vector GetSum(Vector vector1, Vector vector2)
	{
		ArgumentNullException.ThrowIfNull(vector1);
		ArgumentNullException.ThrowIfNull(vector2);

		Vector result = new Vector(vector1);

		result.Add(vector2);

		return result;
	}

	public static Vector GetDifference(Vector vector1, Vector vector2)
	{
		ArgumentNullException.ThrowIfNull(vector1);
		ArgumentNullException.ThrowIfNull(vector2);

		Vector result = new Vector(vector1);

		result.Subtract(vector2);

		return result;
	}

	public void Multiply(double scalar)
	{
		for (int i = 0; i < Size; i++)
		{
			_components[i] *= scalar;
		}
	}

	public void Negate()
	{
		for (int i = 0; i < Size; i++)
		{
			_components[i] = -_components[i];
		}
	}

	public static double GetDotProduct(Vector vector1, Vector vector2)
	{
		ArgumentNullException.ThrowIfNull(vector1);
		ArgumentNullException.ThrowIfNull(vector2);

		double result = 0;
		int minSize = Math.Min(vector1.Size, vector2.Size);

		for (int i = 0; i < minSize; i++)
		{
			result += vector1._components[i] * vector2._components[i];
		}

		return result;
	}

	public double this[int index]
	{
		get
		{
			if (index < 0 || index >= _components.Length)
			{
				throw new IndexOutOfRangeException($"Индекс {index} вне границ (0-{_components.Length - 1})");
			}

			return _components[index];
		}

		set
		{
			if (index < 0 || index >= _components.Length)
			{
				throw new IndexOutOfRangeException($"Индекс {index} вне границ (0-{_components.Length - 1})");
			}

			_components[index] = value;
		}
	}

	public override int GetHashCode()
	{
		const int prime = 17;

		int hash = 1;

		foreach (double component in _components)
		{
			hash = hash * prime + component.GetHashCode();
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

		Vector otherVector = (Vector)obj;

		if (otherVector.Size != Size)
		{
			return false;
		}

		for (int i = 0; i < Size; i++)
		{
			if (_components[i] != otherVector._components[i])
			{
				return false;
			}
		}

		return true;
	}
}