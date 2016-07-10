using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Polynomial
{
    /// <summary>
    /// Allows to work with polynomials. 
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Coefficients of the polynomial.
        /// </summary>
        public double[] Coefficients { get; }

        /// <summary>
        /// Initializes a new instance of the polynomial class.
        /// </summary>
        /// <param name="Coefficients">Coefficients of polynomial.</param>
        public Polynomial(params double[] Coefficients)
        {
            if (Coefficients.Length > 0)
            {
                this.Coefficients = new double[Coefficients.Length];
                Coefficients.CopyTo(this.Coefficients, 0);
            }
            else
                throw new ArgumentException();
        }

        /// <summary>
        /// Allows to solve the polynomial according to the argument.
        /// </summary>
        /// <param name="argument">Argument.</param>
        /// <returns>Solution.</returns>
        public double Solve(double argument)
        {
            double result = 0;
            for (int i = 0; i < Coefficients.Length; i++)
            {
                result += Coefficients[i] * Math.Pow(argument, i);
            }
            return result;
        }

        /// <summary>
        /// Overloads "+" operator to work woth polynomials.
        /// </summary>
        /// <param name="a">First summand.</param>
        /// <param name="b">Second summand.</param>
        /// <returns>Sum of the two polynomials.</returns>
        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            double[] longerPolynomial = null, shorterPolynomial = null;
            if (a.Coefficients.Length < b.Coefficients.Length)
            {
                longerPolynomial = b.Coefficients;
                shorterPolynomial = a.Coefficients;
            }
            else
            {
                longerPolynomial = a.Coefficients;
                shorterPolynomial = b.Coefficients;
            }
            double[] resultCoefficients = new double[longerPolynomial.Length];
            longerPolynomial.CopyTo(resultCoefficients, 0);
            for (int i = 0; i < shorterPolynomial.Length; i++)
                checked
                {
                    resultCoefficients[i] = longerPolynomial[i] + shorterPolynomial[i];
                }
            return new Polynomial(resultCoefficients);
        }

        /// <summary>
        /// Overloads "-" operator to work woth polynomials.
        /// </summary>
        /// <param name="a">Minuend.</param>
        /// <param name="b">Subtrahend.</param>
        /// <returns>Diffirence of the two polynomials.</returns>
        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            double[] longerPolynomial = null, shorterPolynomial = null;
            if (a.Coefficients.Length < b.Coefficients.Length)
            {
                longerPolynomial = b.Coefficients;
                shorterPolynomial = a.Coefficients;
            }
            else
            {
                longerPolynomial = a.Coefficients;
                shorterPolynomial = b.Coefficients;
            }
            double[] resultCoefficients = new double[longerPolynomial.Length];
            longerPolynomial.CopyTo(resultCoefficients, 0);
            for (int i = 0; i < shorterPolynomial.Length; i++)                
                {
                    resultCoefficients[i] = longerPolynomial[i] - shorterPolynomial[i];
                }
            if (longerPolynomial != a.Coefficients)
                for (int i = 0; i < resultCoefficients.Length; i++)
                    checked
                    {
                        resultCoefficients[i] = -resultCoefficients[i];
                    }
            return new Polynomial(resultCoefficients);
        }

        /// <summary>
        /// Overloads "*" operator to work woth polynomials.
        /// </summary>
        /// <param name="a">Factor.</param>
        /// <param name="b">Factor.</param>
        /// <returns>Product of the two polynomials.</returns>
        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            double[] resultCoefficients = new double[a.Coefficients.Length + b.Coefficients.Length - 1];
            for (int i = 0; i < a.Coefficients.Length; i++)
            {
                for (int j = 0; j < b.Coefficients.Length; j++)
                    resultCoefficients[i + j] += a.Coefficients[i] * b.Coefficients[j]; 
            }
            return new Polynomial(resultCoefficients);
        }

        /// <summary>
        /// Allows to check equals.
        /// </summary>
        /// <param name="obj">Object to check.</param>
        /// <returns>Result.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            else if (this.Coefficients.Length != (obj as Polynomial).Coefficients.Length)
                return false;
            else
                return this.Coefficients.SequenceEqual((obj as Polynomial).Coefficients);
        }

        /// <summary>
        /// Allows to show polynomial as string.
        /// </summary>
        /// <returns>Polynomial as string.</returns>
        public override string ToString()
        {
            string result = "";
            if (this.Coefficients[0] != 0)
                result += this.Coefficients[0].ToString() + " ";
            if (this.Coefficients[1] != 0)
            {
                if (this.Coefficients[1] > 0 && result != "")
                    result += "+";
                result += this.Coefficients[1].ToString() + "X ";
            }
            for (int i = 2; i < this.Coefficients.Length; i++)
            {
                if (this.Coefficients[i] != 0)
                {
                    if (this.Coefficients[i] > 0 && result != "")
                        result += "+";
                    result += this.Coefficients[i].ToString() + "(X^" + i.ToString() + ")";
                    if (i != this.Coefficients.Length - 1)
                        result += " ";
                }
            }
            return result;
        }

        /// <summary>
        /// Allows to get hash code.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return (int)(this.Coefficients.Min() * 100 - this.Coefficients.Sum() * 10 + this.Coefficients.Max() - this.Coefficients.Length * 1000);
        }
    }
}
