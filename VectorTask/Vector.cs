namespace VectorTask;

class Vector
{
	private double[] _components;

	public int Length => _components.Length;

	public Vector(int vectorLength)
	{
		if (vectorLength <= 0)
		{
			throw new ArgumentException($"Длина вектора должна быть положительной: {vectorLength}", nameof(vectorLength));
		}

		_components = new double[vectorLength];
	}

	public Vector(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		_components = new double[vector.Length];
		Array.Copy(vector._components, _components, vector.Length);
	}

	public Vector(double[] components)
	{
		ArgumentNullException.ThrowIfNull(components);

		_components = new double[components.Length];
		Array.Copy(components, _components, components.Length);
	}

	public Vector(int vectorLength, double[] components)
	{
		ArgumentNullException.ThrowIfNull(components);

		if (vectorLength <= 0)
		{
			throw new ArgumentException($"Длина вектора должна быть положительной: {vectorLength}", nameof(vectorLength));
		}

		_components = new double[vectorLength];

		int copyLength = Math.Min(vectorLength, components.Length);
		Array.Copy(components, 0, _components, 0, copyLength);
	}

	public override string ToString()
	{
		return $"{{{string.Join(", ", _components)}}}";
	}

	public double GetSize()
	{
		double sum = 0;

		for (int i = 0; i < Length; i++)
		{
			sum += _components[i] * _components[i];
		}

		return Math.Sqrt(sum);
	}

	public void Add(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		if (vector.Length > Length)
		{
			Array.Resize(ref _components, vector.Length);
		}

		for (int i = 0; i < vector.Length; i++)
		{
			_components[i] += vector._components[i];
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

	public static Vector GetSubtract(Vector vector1, Vector vector2)
	{
		ArgumentNullException.ThrowIfNull(vector1);
		ArgumentNullException.ThrowIfNull(vector2);

		Vector result = new Vector(vector1);

		result.Subtract(vector2);

		return result;
	}

	public void Subtract(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		if (vector.Length > Length)
		{
			Array.Resize(ref _components, vector.Length);
		}

		for (int i = 0; i < vector.Length; i++)
		{
			_components[i] -= vector._components[i];
		}
	}

	public void Multiply(double n)
	{
		for (int i = 0; i < Length; i++)
		{
			_components[i] *= n;
		}
	}

	public void Negate()
	{
		for (int i = 0; i < Length; i++)
		{
			_components[i] = -_components[i];
		}
	}

	public static double GetDot(Vector vector1, Vector vector2)
	{
		ArgumentNullException.ThrowIfNull(vector1);
		ArgumentNullException.ThrowIfNull(vector2);

		double result = 0;
		int minSize = Math.Min(vector1.Length, vector2.Length);

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

		if (otherVector.Length != Length)
		{
			return false;
		}

		for (int i = 0; i < Length; i++)
		{
			if (_components[i] != otherVector._components[i])
			{
				return false;
			}
		}

		return true;
	}
}