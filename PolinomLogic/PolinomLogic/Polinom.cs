using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PolinomLogic
{
    public class Polinom:ICloneable
    {
        private readonly double[] coef = {};
        private int degree;

        public Polinom( params  double[] list)
        {
            if(list==null)
                throw new ArgumentNullException();

            coef = new double[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                coef[i] = list[i];
            }

        }

        public double this[int i]
        {
            get
            {
                if(i>degree)
                    throw new ArgumentOutOfRangeException();
                return coef[i];
            }
            set
            {
               Degree();
            }
        }
        public void Degree()
        {
            if (coef == null)
                throw new ArgumentNullException();
            int i = coef.Length - 1;
            while (coef[i] == 0 && i >= 0)
            {
                i--;
            }
            degree = i;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj,null))
                return false;
            Polinom m = obj as Polinom;
            if (m as Polinom == null)
                return false;
            return this.Equals(m);
        }
        public  bool Equals(Polinom obj)
        {
            if (obj == null)
                return false;
            if (obj.degree != this.degree)
                return false;
            if (obj.coef == this.coef)
            return true;
            return true;
            
        }

        public override int GetHashCode()
        {
            if (coef != null)
                return (int) (coef[0] + degree + coef[degree - 1]);
            else return 0;

        }

        //public override string ToString()
        //{
        //    int count = 0;
        //    string s="";
        //    foreach (int i in this.coef)
        //    {
        //        s+= i + "x^{0}";
        //    }
        //    return null;
        //}
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static Polinom operator +(Polinom obj1, Polinom obj2)
        {
            if (obj1 == null || obj2 == null)
                throw new ArgumentNullException();
            Polinom newm;
            if (obj1.degree > obj2.degree)
            {
               newm = (Polinom)obj1.Clone();
            }
            else
            {newm = (Polinom)obj2.Clone();}

            for(int i=0; i<Math.Min(obj1.degree,obj2.degree);i++)
            {
                if (newm.Equals(obj1))
                {
                    newm.coef[i] += obj2.coef[i];
                }
                else
                {
                    newm.coef[i] += obj1.coef[i];
                }
            }
            return newm;
        }

        public  Polinom Add(Polinom obj)
        {
            return this+obj;
        }
        public static Polinom operator *(Polinom obj, double a)
        {
            if (obj==null)
                throw new ArgumentNullException();
            double[] masc = new double[obj.degree];
            for (int i = 0; i < obj.degree; i++)
            {
                masc[i] += obj.coef[i]*a;
            }
            return new Polinom(masc);
        }

        public static Polinom operator -(Polinom obj1, Polinom obj2)
        {
            if (obj1 == null || obj2 == null)
                throw new ArgumentNullException();
            double a = -1.0;
            Polinom newm = (Polinom)obj1.Clone();
            newm.Add(obj2*a);
            return newm;
        }

        public Polinom Sub(Polinom obj)
        {
            return this - obj;
        }
    }
}
