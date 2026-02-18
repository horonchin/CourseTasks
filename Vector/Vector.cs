using System.Numerics;

namespace VectorTask;

class Vector
{
	public const int prime = 17;

	private double[] _components;

	public Vector(int n)
	{
		if (n <= 0)
		{
			throw new ArgumentException($"Длина массива должна быть положительной: {n}", nameof(n));
		}

		_components = new double[n];
	}

	public Vector(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		_components = new double[vector._components.Length];
		Array.Copy(vector._components, _components, vector._components.Length);
	}

	public Vector(double[] components)
	{
		ArgumentNullException.ThrowIfNull(components);

		if (components.Length == 0)
		{
			throw new ArgumentException("Массив не может быть пустым", nameof(components));
		}

		_components = new double[components.Length];
		Array.Copy(components, _components, components.Length);
	}

	public Vector(int n, double[] components)
	{
		ArgumentNullException.ThrowIfNull(components);

		if (n <= 0)
		{
			throw new ArgumentException($"Длина массива должна быть положительной: {n}", nameof(n));
		}

		if (components.Length == 0)
		{
			throw new ArgumentException("Массив не может быть пустым", nameof(components));
		}

		_components = new double[n];

		int copyLength = Math.Min(n, components.Length);
		Array.Copy(components, 0, _components, 0, copyLength);
	}

	public int GetSize() => _components.Length;

	public override string ToString()
	{
		return "{" + string.Join(", ", _components) + "}";
	}

	public void AddTo(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		int thisSize = this.GetSize();
		int vectorSize = vector.GetSize();

		if (vectorSize > thisSize)
		{
			double[] newComponents = new double[vectorSize];

			Array.Copy(_components, newComponents, thisSize);

			_components = newComponents;
		}

		int maxSize = Math.Max(thisSize, vectorSize);

		for (int i = 0; i < vectorSize; i++)
		{
			_components[i] += vector._components[i];
		}
	}

	public static Vector Sum(Vector vector1, Vector vector2)
	{
		ArgumentNullException.ThrowIfNull(vector1);
		ArgumentNullException.ThrowIfNull(vector2);

		int vector1Size = vector1.GetSize();
		int vector2Size = vector2.GetSize();
		int maxSize = Math.Max(vector1Size, vector2Size);

		double[] resultComponents = new double[maxSize];

		for (int i = 0; i < maxSize; i++)
		{
			double vector1Value = i < vector1Size ? vector1._components[i] : 0;
			double vector2Value = i < vector2Size ? vector2._components[i] : 0;

			resultComponents[i] = vector1Value + vector2Value;
		}

		return new Vector(resultComponents);
	}

	public static Vector Subtract(Vector vector1, Vector vector2)
	{
		ArgumentNullException.ThrowIfNull(vector1);
		ArgumentNullException.ThrowIfNull(vector2);

		int vector1Size = vector1.GetSize();
		int vector2Size = vector2.GetSize();
		int maxSize = Math.Max(vector1Size, vector2Size);

		double[] resultComponents = new double[maxSize];

		for (int i = 0; i < maxSize; i++)
		{
			double vector1Value = i < vector1Size ? vector1._components[i] : 0;
			double vector2Value = i < vector2Size ? vector2._components[i] : 0;

			resultComponents[i] = vector1Value - vector2Value;
		}

		return new Vector(resultComponents);
	}

	public void SubtractTo(Vector vector)
	{
		ArgumentNullException.ThrowIfNull(vector);

		int thisSize = this.GetSize();
		int vectorSize = vector.GetSize();

		if (vectorSize > thisSize)
		{
			double[] newComponents = new double[vectorSize];

			Array.Copy(_components, newComponents, thisSize);

			_components = newComponents;
		}

		int maxSize = Math.Max(thisSize, vectorSize);

		for (int i = 0; i < vectorSize; i++)
		{
			_components[i] -= vector._components[i];
		}
	}

	public void Multiply(double n)
	{
		int thisSize = this.GetSize();

		for (int i = 0; i < thisSize; i++)
		{
			this._components[i] = this._components[i] * n;
		}
	}

	public void Negate()
	{
		int thisSize = this.GetSize();

		for (int i = 0; i < thisSize; i++)
		{
			_components[i] = -_components[i];
		}
	}

	public static double Dot(Vector vector1, Vector vector2)
	{
		ArgumentNullException.ThrowIfNull(vector1);
		ArgumentNullException.ThrowIfNull(vector2);


		double result = 0;
		int minSize = Math.Min(vector1.GetSize(), vector2.GetSize());

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
		int hash = 1;

		for (int i = 0; i < _components.Length; i++)
		{
			hash = hash * prime + _components[i].GetHashCode();
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

		Vector p = (Vector)obj;
		int thisLength = this._components.Length;
		int vectorLength = p._components.Length;

		if (vectorLength == thisLength)
		{
			for (int i = 0; i < thisLength; i++)
			{
				if (this._components[i] != p._components[i])
				{
					return false;
				}

				i++;
			}

			return true;
		}

		return false;
	}
}