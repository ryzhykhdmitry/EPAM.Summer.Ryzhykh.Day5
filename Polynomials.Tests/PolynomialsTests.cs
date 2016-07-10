using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Polynomials;

namespace Polynomials.Tests
{
    [TestFixture]

    public class DataClass
    {
        public static IEnumerable TestCasesForSolve
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 1, 2, 3), 10).Returns(3211);
                yield return new TestCaseData(new Polynomial(200, 1, -2, 3), 10).Returns(3010);
                yield return new TestCaseData(new Polynomial(-1, -2, -3, -4), 1).Returns(-10);
                yield return new TestCaseData(new Polynomial(0, 0, 0, 0, 0, 0), 10).Returns(0);
                yield return new TestCaseData(new Polynomial(0, long.MaxValue, long.MaxValue), 1).Returns(2 * Convert.ToDecimal(long.MaxValue));
                yield return new TestCaseData(new Polynomial(0, double.MaxValue, double.MaxValue, double.MaxValue), 10).Returns(double.PositiveInfinity);
                yield return new TestCaseData(new Polynomial(0, double.MinValue, double.MinValue, double.MinValue), 10).Returns(double.NegativeInfinity);
                yield return new TestCaseData(new Polynomial(0.1, 0, 0, 0.3), 10).Returns(300.1);
            }
        }

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(255, 853, 5, 9, 863);
                yield return new TestCaseData(106, 106, 2, 5, 106);
                yield return new TestCaseData(0, 7, 2, 2, 4);
                yield return new TestCaseData(-7, 2, 1, 1, -5);

            }
        }
    }

    public class PolynomialTest
    {
        [Test, TestCaseSource(typeof(DataClass), "TestCasesForSolve")]
        public double SolvePolynomial_PolynomialAndArgument_Result(Polynomial a, double argument)
        {
            return a.Solve(argument);
        }
    }
}
