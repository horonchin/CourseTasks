namespace RangeTask;

class Range
{
	public double From { get; set; }

	public double To { get; set; }

	public Range(double from, double to)
	{
		From = from;
		To = to;
	}

	public double GetLength()
	{
		return To - From;
	}

	public bool IsInside(double number)
	{
		return number >= From && number <= To;
	}

	public Range GetIntersection(Range range)
	{
		double intersectionFrom = Math.Max(this.From, range.From);
		double intersectionTo = Math.Min(this.To, range.To);

		if (intersectionFrom < intersectionTo)
		{
			return new Range(intersectionFrom, intersectionTo);
		}

		return null;
	}

	public Range[] GetUnion(Range range)
	{
		bool canMerge = Math.Max(this.From, range.From) <= Math.Min(this.To, range.To);

		if (canMerge)
		{
			double unionFrom = Math.Min(this.From, range.From);
			double unionTo = Math.Max(this.To, range.To);

			return new Range[] { new Range(unionFrom, unionTo) };
		}
		else
		{
			Range[] result = new Range[2];

			if (this.From < range.From)
			{
				result[0] = new Range(this.From, this.To);
				result[1] = new Range(range.From, range.To);
			}
			else
			{
				result[0] = new Range(range.From, range.To);
				result[1] = new Range(this.From, this.To);
			}

			return result;
		}
	}

	public Range[] GetDifference(Range range)
	{
		double intersectFrom = Math.Max(this.From, range.From);
		double intersectTo = Math.Min(this.To, range.To);

		if (intersectFrom >= intersectTo)
		{
			return new[] { new Range(this.From, this.To) };
		}

		bool hasLeft = this.From < intersectFrom;
		bool hasRight = this.To > intersectTo;

		if (hasLeft && hasRight)
		{
			Range leftPart = new Range(this.From, range.From);
			Range rightPart = new Range(range.To, this.To);

			return new Range[] { leftPart, rightPart };
		}
		else if (hasLeft)
		{
			return new[] { new Range(this.From, intersectFrom) };
		}
		else if (hasRight)
		{
			return new[] { new Range(intersectTo, this.To) };
		}
		else
		{
			return Array.Empty<Range>();
		}
	}
}