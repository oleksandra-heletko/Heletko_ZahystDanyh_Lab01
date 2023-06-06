using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heletko_Zahyst_Danyh_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string a_test = "1001101101";
            string b_test = "11100011";
            string m_test = "10011";

            string a_exp = "1001111101111";
            string b_exp = "11101011101";
            string m_exp = "100011011";

            int a_t = Convert.ToInt32(a_test, 2);
            int b_t = Convert.ToInt32(b_test, 2);
            int m_t = Convert.ToInt32(m_test, 2);
            int ab_t = PolyProd(a_t, b_t, b_test.Length);
            int abm_t = PolyMod(ab_t, m_t, a_test.Length + b_test.Length, m_test.Length);

            Console.WriteLine("Test polynomials:");
            Console.WriteLine("A: {0}", a_test);
            PolyPrint(a_t);
            Console.WriteLine("B: {0}", b_test);
            PolyPrint(b_t);
            Console.WriteLine("M: {0}", m_test);
            PolyPrint(m_t);
            Console.WriteLine("\nA * B: {0}", Convert.ToString(ab_t, 2));
            PolyPrint(ab_t);
            Console.WriteLine("\nA * B mod M: {0}", Convert.ToString(abm_t, 2));
            PolyPrint(abm_t);

            Console.WriteLine("\n");

            int a_e = Convert.ToInt32(a_exp, 2);
            int b_e = Convert.ToInt32(b_exp, 2);
            int m_e = Convert.ToInt32(m_exp, 2);
            int ab_e = PolyProd(a_e, b_e, b_exp.Length);
            int abm_e = PolyMod(ab_e, m_e, a_exp.Length + b_exp.Length, m_exp.Length);

            Console.WriteLine("Polynomials:");
            Console.WriteLine("A: {0}", a_exp);
            PolyPrint(a_e);
            Console.WriteLine("B: {0}", b_exp);
            PolyPrint(b_e);
            Console.WriteLine("M: {0}", m_exp);
            PolyPrint(m_e);
            Console.WriteLine("\nA * B: {0}", Convert.ToString(ab_e, 2));
            PolyPrint(ab_e);
            Console.WriteLine("\nA * B mod M: {0}", Convert.ToString(abm_e, 2));
            PolyPrint(abm_e);

            Console.ReadKey();
        }

        static int PolyProd(int poly_a, int poly_b, int size) //Polynomial production
        {
            int poly_res = 0;
            for(int i = 0; i < size; i++)
            {
                if((poly_b & (1 << i)) > 0)
                {
                    poly_res ^= poly_a << i;
                }
            }
            return poly_res;
        }
        
        static int PolyMod(int poly_ab, int poly_m, int size_ab, int size_m) //Polynomial modulus
        {
            int poly_res = poly_ab;
            for (int i = size_ab-size_m-1; i > 0; i--)
            {
                while (GetMSBPos(poly_res) < i + size_m-1)
                {
                    i--;
                }
                poly_res ^= poly_m << i;
            }
            return poly_res;
        } 

        static int GetMSBPos(int poly)
        {
            int p = 0;
            for (int i = 0; i < sizeof(Int32) * 8; i++)
            {
                if ((poly & (1 << i)) > 0)
                {
                    p = i;
                }
            }
            return p;
        }

        static void PolyPrint(int poly)
        {
            int s = GetMSBPos(poly);
            for (int i = 0; i < s+1; i++)
            {
                if ((poly & (1 << i)) > 0)
                {
                    if (i > 0) Console.Write(" + 1*x^{0}", i); else Console.Write("1");
                }
                else
                {
                    if (i > 0) Console.Write(" + 0*x^{0}", i); else Console.Write("0");
                }
            }
            Console.WriteLine();
        }
    }
}
