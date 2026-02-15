using System.Diagnostics.Tracing;
using System.Formats.Tar;
using VectorTask;

namespace Academits.Courses;

class Program
{
	public static void Main()
	{
		//проверка конструкторов
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

		v1.AddTo(v2);
		Console.WriteLine($"Сложение v1 и v2 равно: {v1}");

		v1.SubtractTo(v2);
		Console.WriteLine($"Вычитание v1 и v2 равно {v1}");

		Vector v5 = v1.Sum(v2);
		Console.WriteLine($"Сложение векторов {v1} и {v2} равно вектору v5: {v5}");

		v1.Negate();
		Console.WriteLine($"Развёрнутый v1 равен: {v1}");

		Vector v6 = v3.Sum(v2);
		Console.WriteLine($"Cумма векторов {v3} и {v2} равно вектору v6: {v6}");

		v6.Multiply(5);
		Console.WriteLine($"Умножение вектора v6 на скаляр 5 равно: {v6}");

		Vector v7 = v4.Subtract(v6);
		Console.WriteLine($"Вычитание вектора {v4} и {v6} равно: {v7}");

	}
}