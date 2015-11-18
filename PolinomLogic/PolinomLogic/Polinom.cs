using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PolinomLogic
{
    public class Polinom
    {
        private readonly int[] coef;
        private readonly int degree;

        private Polinom( params  int[] list )
        {
            coef = list;
            degree = list.Length;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Polinom m = obj as Polinom;
            if (m as Polinom == null)
                return false;
            return m.coef == this.coef && m.degree==this.degree;
        }
        public  bool Equals(Polinom obj)
        {
            if (obj == null)
                return false;
           
            return obj.coef == this.coef && obj.degree==this.degree;
        }

        public override int GetHashCode()
        {
            return this.coef[0]+this.degree;
        }

        //public override string ToString()
        //{
        //    int count = 0;
        //    foreach (int i in this.coef)
        //    {
        //        return i + "x^{0}";
        //    }
        //    return null;
        //}

    }
}
