using VectorTask;

class Program
{
	public static void Main()
	{
		Vector v1 = new Vector(5);
		Console.WriteLine(v1);

		v1[0] = 1.4;
		v1[1] = 2.5;
		v1[2] = 3.3;
		v1[3] = 5.5;
		v1[4] = 7;
		Console.WriteLine(v1);

		Vector v2 = new Vector(v1);
		Console.WriteLine(v2);

		double[] arr1 = { 1.1, 2.2, 3.3, 4.4 };
		Vector v3 = new Vector(arr1);
		Console.WriteLine(v3);

		Vector v4 = new Vector(5, arr1);
		Console.WriteLine(v4);

		Console.WriteLine($"Длина вектора {v1} равна: {v1.GetSize()}");

		v1.AddTo(v2);
		Console.WriteLine($"Сложение v1 и v2 равно: {v1}");

		v1.SubtractTo(v2);
		Console.WriteLine($"Вычитание v1 и v2 равно {v1}");

		Vector v5 = Vector.Sum(v2, v1);
		Console.WriteLine($"Сложение векторов {v1} и {v2} равно вектору v5: {v5}");

		v1.Negate();
		Console.WriteLine($"Развёрнутый v1 равен: {v1}");

		Vector v6 = Vector.Sum(v2, v3);
		Console.WriteLine($"Cумма векторов {v3} и {v2} равно вектору v6: {v6}");

		v6.Multiply(5);
		Console.WriteLine($"Умножение вектора v6 на скаляр 5 равно: {v6}");

		Vector v7 = Vector.Subtract(v6,v4);
		Console.WriteLine($"Вычитание вектора {v4} и {v6} равно: {v7}");

		v7[0] = 5;
		double firstV7Component = v7[0];
		Console.WriteLine($"Первый элемент вектора v7 был изменён и стал равен: {v7[0]}");

		double dot = Vector.Dot(v4, v7);
		Console.WriteLine($"Скалярное произведение вектора v7 на v4 равно: {dot}");
	}
}