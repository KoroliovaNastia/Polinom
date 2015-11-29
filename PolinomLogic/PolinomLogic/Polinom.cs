using System;
using System.Configuration.Internal;
using System.Diagnostics;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PolinomLogic
{
    public class Polinom : ICloneable
    {
        #region fields

        private static double eps = 0.00001;
        private readonly double[] coef = { };
        private int degree;
        #endregion

        #region Ctors

        // static Polinom()
        //{
        //    try
        //    {
        //        eps = Convert.ToDouble(ConfigurationManager.AppSettings["eps"]);
        //    }
        //    catch (ConfigurationErrorsException)
        //    {
        //        eps = 0.000001;
        //    }
        //}

        public Polinom(params  double[] list)
        {
            if (list == null)
                throw new ArgumentNullException();

            coef = new double[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                coef[i] = list[i];
            }

        }
        #endregion

        public double this[int i]
        {
            get
            {
                if (i > degree || i < 0)
                    throw new ArgumentOutOfRangeException();

                return coef[i];
            }
            set
            {
                if (i > degree || i < 0)
                    throw new ArgumentOutOfRangeException();

                coef[i] = value;
                //if (Math.Abs(value) < eps && i == degree)
                //{
                //    FindDegree();
                //}
            }
        }

        //public int FindDegree()
        //{
        //    if (coef == null)
        //        throw new ArgumentNullException();
        //    int i = coef.Length - 1;
        //    while (coef[i] < eps && i > -1)
        //    {
        //        i--;
        //    }
        //    degree = i;
        //    return degree;
        //}

        public int Degree
        {
            get
            {
                if (coef == null)
                    throw new ArgumentNullException();
                int i = coef.Length - 1;
                while (Math.Abs(coef[i]) < 0.000001 && i >= 0)
                {
                    i--;
                }
                degree = i;
                return degree;

            }

            set { degree = value; }

        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            Polinom p = new Polinom();

            if (p.GetType() != obj.GetType())
                return false;
            p = (Polinom)obj;
            return Equals(p);
        }
        public bool Equals(Polinom p)
        {
            if (ReferenceEquals(p, null))
                return false;
            if (ReferenceEquals(p, this))
                return true;
            if (p.degree != degree)
                return false;
            //!!!!!!!!!!!!
            for (int i = 0; i < degree; i++)
            {
                if (Math.Abs(coef[i] - p[i]) > eps)

                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return coef.GetHashCode();

        }

        public override string ToString()
        {
            string str = "";
            str += coef[0];
            //double[] mass = new double[coef.Length];
            for (int i = 1; i < coef.Length; i++)
            {
                if (coef[i] > eps)
                    str += coef[i] + "x^"+i;

            }


            return str;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public Polinom Clone()
        {
            return new Polinom(coef);
        }

        public static Polinom operator +(Polinom p1, Polinom p2)
        {
            if (p1 == null || p2 == null)
                throw new ArgumentNullException();

            Polinom newm = p1.degree > p2.degree ? p1.Clone() : p2.Clone();

            for (int i = 0; i < Math.Min(p1.degree, p2.degree); i++)
            {
                if (newm.Equals(p1))
                {
                    newm.coef[i] += p2.coef[i];
                }
                else
                {
                    newm.coef[i] += p1.coef[i];
                }
            }
            return newm;
        }

        public static Polinom Add(Polinom lhs, Polinom rhs)
        {
            return lhs + rhs;
        }
        public static Polinom operator *(Polinom p, double a)
        {
            if (p == null)
                throw new ArgumentNullException();

            Polinom polinom = p.Clone();

            for (int i = 0; i < p.degree; i++)
            {
                polinom[i] = a * polinom[i];
            }
            return polinom;
        }

        public static Polinom operator -(Polinom p1, Polinom p2)
        {
            if (p1 == null || p2 == null)
                throw new ArgumentNullException();

            return p1 + p2 * (-1);
        }

        public static Polinom Sub(Polinom lhs, Polinom rhs)
        {
            return lhs - rhs;
        }

        public static Polinom Mult(Polinom lhs, Polinom rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                throw new ArgumentNullException();
            Polinom p2 = new Polinom(new double[lhs.degree + rhs.degree + 1]);
            for (int i = 0; i < lhs.degree; i++)
            {
                Polinom p1 = rhs * lhs[i];
                for (int k = 0; k < rhs.degree; k++)
                {
                    p2[i + k] += p1[i];
                }
            }
            return p2;
        }
        public static bool operator ==(Polinom lhs, Polinom rhs)
        {
            if (ReferenceEquals(rhs, lhs))
            {
                return true;
            }
            if (ReferenceEquals(lhs, null))
            {
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Polinom lhs, Polinom rhs)
        {
            return !(lhs == rhs);
        }
    }
}
