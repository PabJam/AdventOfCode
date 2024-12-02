using System;

public static class Utils
{
	/// <summary>
	/// Chinese Remainder Theorem
	/// </summary>
	/// <param name="div">All different divisors</param>
	/// <param name="rem">All different remainders</param>
	/// <returns>smallest possible x so that x % div = rem for each div and rem </returns>
	/// <exception cref="ArgumentException"></exception>
	public static ulong CRT(ulong[] div, ulong[] rem)
	{
		if (div.Length != rem.Length) { throw new ArgumentException("div array and rem array must be of same length"); }
		if (div.Length == 0) { return 0; }
		// TODO check if div values dont share any common divisors (are coprime)
		ulong prod = div[0];

		for (int i = 1; i < div.Length; i++)
		{
			prod *= div[i];
		}

		ulong[] pp = new ulong[div.Length]; // product divided by each div
		for (int i = 0; i < pp.Length; i++)
		{
			pp[i] = prod / div[i];
		}
		// TODO modular multiplicative inverse and final calculation
		return 0;
	}

	/// <summary>
	/// Greatest Common Devisor
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static ulong GCD(ulong a, ulong b)
	{
		while (a > 0)
		{
			ulong _a = a
			a = b % a;
			b = _a;
		}
		return b;
	}
}
