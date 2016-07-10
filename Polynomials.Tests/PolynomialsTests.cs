using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Polynomials;

namespace Polynomials.Tests
{
    [TestFixture]

    #region TestData
    public class DataClass
    {
        public static IEnumerable<TestCaseData> TestCasesForSolve
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

        public static IEnumerable<TestCaseData> TestCasesForPlus
        {
            get
            {
                yield return new TestCaseData(new Polynomial(0, 1, 2, 3), new Polynomial(0, 0, 0, 0, 4, 5)).Returns("1X +2(X^2) +3(X^3) +4(X^4) +5(X^5)");
                yield return new TestCaseData(new Polynomial(0, 1, 2, 3), new Polynomial(0, -1, -2, -3)).Returns("");
                yield return new TestCaseData(new Polynomial(0, 0), new Polynomial(0, -1, -2, -3)).Returns("-1X -2(X^2) -3(X^3)");
                yield return new TestCaseData(new Polynomial(1, 2, 3, 4, 5, 6, 7), new Polynomial(-10, 0, -10, 0, -10)).Returns("-9 +2X -7(X^2) +4(X^3) -5(X^4) +6(X^5) +7(X^6)");

            }
        }

        public static IEnumerable<TestCaseData> TestCasesForMinus
        {
            get
            {
                yield return new TestCaseData(new Polynomial(0, 1, 2, 3), new Polynomial(0, 0, 0, 0, 4, 5)).Returns("1X +2(X^2) +3(X^3) -4(X^4) -5(X^5)");
                yield return new TestCaseData(new Polynomial(0.1, 0, 0, 0, 0.04, -0.05), new Polynomial(0.05, 0, 0, 0, 4, 5)).Returns("0,05 -3,96(X^4) -5,05(X^5)");
                yield return new TestCaseData(new Polynomial(0, 0), new Polynomial(0, 0, 0, 0, 0)).Returns("");
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForMultiplication
        {
            get
            {
                yield return new TestCaseData(new Polynomial(0, 1, 2, 3), new Polynomial(0, 0, 0, 0, 4, 5)).Returns("4(X^5) +13(X^6) +22(X^7) +15(X^8)");
                yield return new TestCaseData(new Polynomial(1, 1, 0, 2), new Polynomial(1, 1, 3)).Returns("1 +2X +4(X^2) +5(X^3) +2(X^4) +6(X^5)");
                yield return new TestCaseData(new Polynomial(0, 0), new Polynomial(0, 0, 0, 0, 0)).Returns("");
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForEquals
        {
            get
            {
                yield return new TestCaseData(new Polynomial(0, 1, 2, 3), new Polynomial(0, 0, 0, 0, 4, 5)).Returns("False");
                yield return new TestCaseData(new Polynomial(0, 1, 2, 3), new Polynomial(0, 1, 2, 3)).Returns("True");
                yield return new TestCaseData(new Polynomial(0, 0), new Polynomial(0, 0, 0, 0, 0)).Returns("False");
                yield return new TestCaseData(new Polynomial(0, 0, 0), new Polynomial(0, 0, 0)).Returns("True");
                yield return new TestCaseData(new Polynomial(1, 2, 3), null).Returns("False");
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForGetHashCode
        {
            get
            {
                yield return new TestCaseData(new Polynomial(0, 1, 2, 3), new Polynomial(0, 0, 0, 0, 4, 5)).Returns("False");
                yield return new TestCaseData(new Polynomial(0, 1, 2, 3), new Polynomial(0, 1, 2, 3)).Returns("True");
                yield return new TestCaseData(new Polynomial(0, 0), new Polynomial(0, 0, 0, 0, 0)).Returns("False");
                yield return new TestCaseData(new Polynomial(0, 0, 0), new Polynomial(0, 0, 0)).Returns("True");
            }
        }
    }
    #endregion

    #region Tests
    public class PolynomialTest
    {
        [Test, TestCaseSource(typeof(DataClass), "TestCasesForSolve")]
        public double SolvePolynomial_PolynomialAndArgument_Result(Polynomial a, double argument)
        {
            return a.Solve(argument);
        }

        [Test, TestCaseSource(typeof(DataClass), "TestCasesForPlus")]
        public string PlusPolynomials_TwoPlynomials_Summ(Polynomial a, Polynomial b)
        {
            return (a + b).ToString();
        }

        [Test, TestCaseSource(typeof(DataClass), "TestCasesForMinus")]
        public string MinusPolynomials_TwoPlynomials_Result(Polynomial a, Polynomial b)
        {
            return (a - b).ToString();
        }

        [Test, TestCaseSource(typeof(DataClass), "TestCasesForMultiplication")]
        public string MultiplicatePolynomials_TwoPlynomials_Result(Polynomial a, Polynomial b)
        {
            return (a * b).ToString();
        }

        [Test, TestCaseSource(typeof(DataClass), "TestCasesForEquals")]
        public string Comparing_TwoPlynomials_ResultToString(Polynomial a, Polynomial b)
        {
            return a.Equals(b).ToString();
        }

        [Test, TestCaseSource(typeof(DataClass), "TestCasesForGetHashCode")]
        public string Comparing_TwoPlynomialsHashes_ResultToString(Polynomial a, Polynomial b)
        {
            return (a.GetHashCode() == b.GetHashCode()).ToString();
        }
    } 
    #endregion
}
