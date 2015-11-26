using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PolinomLogic
{
    public class Polinom : ICloneable
    {
        #region fields

        private static double eps = 0.000001;
        private readonly double[] coef = { };
        private int degree;
        #endregion

        #region Ctors
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
            private set
            {
                if (i > degree || i < 0)
                    throw new ArgumentOutOfRangeException(); 
               coef[i]=value;
            }
        }
        public int Degree
        {
            get
            {
                if (coef == null)
                    throw new ArgumentNullException();
                int i = coef.Length - 1;
                while (Math.Abs(coef[i]) < eps && i >= 0)
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
            Polinom m = new Polinom();
            if (m.GetType()==obj.GetType())
                return false;
            
                return this.Equals(m);
        }
        public bool Equals(Polinom p)
        {
            if (ReferenceEquals(p, null))
                return false;
            if (ReferenceEquals(p, this))
                return true;
            if (p.degree != this.degree)
                return false;
            //!!!!!!!!!!!!
            for (int i = 0; i < coef.Length; i++)
            {
                if (Math.Abs(coef[i] - p[i]) > eps)
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return coef.GetHashCode();

        }

        public override string ToString()
        {
            int count = 0;
            string s = "";
            foreach (int i in this.coef)
            {
                s += i + "x^{0}";
            }
            return null;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public Polinom Clone()
        {
            return new Polinom(this.coef);
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

        public static  Polinom Sub(Polinom lhs, Polinom rhs)
        {
            return lhs - rhs;
        }

        public Polinom Mult(Polinom p)
        {
            if (ReferenceEquals(p, null) || ReferenceEquals(this, null))
                throw new ArgumentNullException();
            Polinom pl = new Polinom(new double[degree + p.degree + 1]);
            for (int i = 0; i < degree; i++)
            {
                for (int k = 0; k < p.degree; k++)
                {
                    pl[i + k] += coef[i] * p.coef[k];
                }
            }
            return pl;
        }
    }
}
