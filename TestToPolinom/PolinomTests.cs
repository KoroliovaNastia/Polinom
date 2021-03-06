﻿using System;
using PolinomLogic;
using System.Collections.Generic;
using NUnit.Framework;


namespace TestToPolinom
{
    [TestFixture]
    public class PolinomTests
    {
        [Test, TestCaseSource(typeof(PolinomFactoryClass), "CreateTestCases")]
        public Polinom PolinomCreateTest(double[] coef)
        {
            Polinom obj = new Polinom(coef);

            return obj;
        }

        [Test, TestCaseSource(typeof(PolinomFactoryClass), "SumTestCases")]
        public Polinom PolinomSumTest(double[] coef1, double[] coef2)
        {
            Polinom p1 = new Polinom(coef1);
            Polinom p2 = new Polinom(coef2);
            return Polinom.Add(p1, p2);
        }

        [Test, TestCaseSource(typeof(PolinomFactoryClass), "DiffTestCases")]
        public Polinom PolinomDiffTest(double[] coef1, double[] coef2)
        {
            Polinom p1 = new Polinom(coef1);
            Polinom p2 = new Polinom(coef2);
            return Polinom.Sub(p1, p2);
        }
        [Test, TestCaseSource(typeof(PolinomFactoryClass), "MultTestCases")]
        public Polinom PolinomMultTest(double[] coef1, double[] coef2)
        {
            Polinom p1 = new Polinom(coef1);
            Polinom p2 = new Polinom(coef2);
            return Polinom.Mult(p1, p2);
        }
    }

    public class PolinomFactoryClass
    {

        public static IEnumerable<TestCaseData> CreateTestCases
        {
            get
            {
                yield return new TestCaseData(new[] { 1, 0, 3, 0.5 }).Returns(new Polinom(1, 0, 3, 0.5));
                yield return new TestCaseData(null).Throws(typeof(ArgumentNullException));
            }
        }
        public static IEnumerable<TestCaseData> SumTestCases
        {
            get
            {
                yield return new TestCaseData(new[] { 1, 0, 3, 0.5 }, new[] { 0, 2.5, 1, 2 }).Returns(new Polinom(1, 2.5, 4, 2.5));
                yield return new TestCaseData(new[] { 1.0, 0 }, new[] { 0, 2.5, 1, 2 }).Returns(new Polinom(1, 2.5, 1, 2));
                yield return new TestCaseData(new[] { 1.0, 2 }, new[] { 5, 6555.8 }).Returns(new Polinom(6, 6557.8));
                yield return new TestCaseData(null, new[] { 0, 1.0 }).Throws(typeof(ArgumentNullException));
            }
        }
        public static IEnumerable<TestCaseData> DiffTestCases
        {
            get
            {
                yield return new TestCaseData(new[] { 1, 0, 3, 0.5 }, new[] { 0, 2.5, 1, 2 }).Returns(new Polinom(1, -2.5, 2, -1.5));
                yield return new TestCaseData(new[] { 1.0, 0 }, new[] { 0, 2.5, 1, 2 }).Returns(new Polinom(1, -2.5, -1, -2));
                yield return new TestCaseData(new[] { 1.0, 2 }, new[] { 5, 6555.8 }).Returns(new Polinom(-4, -6553.8));
                yield return new TestCaseData(null, new[] { 0, 1.0 }).Throws(typeof(ArgumentNullException));
            }
        }
        public static IEnumerable<TestCaseData> MultTestCases
        {
            get
            {
                yield return new TestCaseData(new[] { 1, 0, 3, 0.5 }, new[] { 0, 2.5, 1, 2 }).Returns(new Polinom(0, 2.5, 1, 9.5, 4.25, 6.5, 1));
                //yield return new TestCaseData(new[] { 1.0, 0 }, new[] { 0, 2.5, 1, 2 }).Returns(new Polinom(1, -2.5, -1, -2));
                //yield return new TestCaseData(new[] { 1.0, 2 }, new[] { 5, 6555.8 }).Returns(new Polinom(-4, -6553.8));
                yield return new TestCaseData(null, new[] { 0, 1.0 }).Throws(typeof(ArgumentNullException));
            }
        }
    }
}
