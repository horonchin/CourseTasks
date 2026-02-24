using VectorTask;

class Program
{
	public static void Main()
	{
		Vector vector1 = new Vector(5);
		Console.WriteLine(vector1);

		vector1[0] = 1.4;
		vector1[1] = 2.5;
		vector1[2] = 3.3;
		vector1[3] = 5.5;
		vector1[4] = 7;
		Console.WriteLine(vector1);

		Vector vector2 = new Vector(vector1);
		Console.WriteLine(vector2);

		double[] array1 = { 1.1, 2.2, 3.3, 4.4 };
		Vector vector3 = new Vector(array1);
		Console.WriteLine(vector3);
		
		Vector vector4 = new Vector(5, array1);
		Console.WriteLine(vector4);

		Console.WriteLine($"Длина вектора {vector1} равна: {vector1.Length}");

		vector1.Add(vector2);
		Console.WriteLine($"Сложение vector1 и vector2 равно: {vector1}");

		vector1.Subtract(vector2);
		Console.WriteLine($"Вычитание vector1 и vector2 равно {vector1}");

		Vector vector5 = Vector.GetSum(vector2, vector1);
		Console.WriteLine($"Сумма векторов {vector1} и {vector2} равно вектору vector5: {vector5}");

		vector1.Negate();
		Console.WriteLine($"Развёрнутый vector1 равен: {vector1}");

		Vector vector6 = Vector.GetSum(vector2, vector3);
		Console.WriteLine($"Cумма векторов {vector3} и {vector2} равна вектору vector6: {vector6}");

		vector6.Multiply(5);
		Console.WriteLine($"Умножение вектора vector6 на скаляр 5 равно: {vector6}");

		Vector vector7 = Vector.GetSubtract(vector6,vector4);
		Console.WriteLine($"Разность вектора {vector4} и {vector6} равна: {vector7}");

		vector7[0] = 5;
		double firstvector7Component = vector7[0];
		Console.WriteLine($"Первый элемент вектора vector7 был изменён и стал равен: {firstvector7Component}");

		Console.WriteLine($"Скалярное произведение вектора vector7 на vector4 равно: {Vector.GetDot(vector4, vector7)}");

		Console.WriteLine($"Длина вектора {vector7} равна {vector7.GetSize()}");
	}
}