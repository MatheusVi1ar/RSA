using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            bool erro = false;

            int p = 0;
            int q = 0;
            int n = 0;
            int fi_n = 0;
            int e = 0;
            int d = 0;

            string descricao = "";
            string criptografado = "";
            string descriptografado = "";

            do
            {
               // p = 0;
               // q = 0;
               // n = 0;
               // fi_n = 0;
               // e = 0;
               // d = 0;

                try
                {
                    //Definir p e q
                    Random randNum = new Random();
                    int valor_minimo = 2;
                    int valor_maximo = 100;

                    do
                    {
                        p = randNum.Next(valor_minimo, valor_maximo);
                        q = randNum.Next(valor_minimo, valor_maximo);                       
                    } while (p == q || !VerificaPrimo(p) || !VerificaPrimo(q));

                    //Etapa 1: n = p.q 

                    n = p * q;

                    //Etapa 2: Φ(n) = (p-1).(q-1)
                    fi_n = (p - 1) * (q - 1);

                    //Etapa 3: 1 < e < Φ(N)
                    //OBS: Verificar se são primos entre si, quando dois números apresentam 
                    //um único divisor comum entre eles!
                    e = fi_n-1;

                    while (e < fi_n)
                    {                    
                        if(mmc(e, fi_n) == 1)
                        {
                            break;
                        }                        
                        e++;
                    };

                    //Etapa 4: e.d mod Φ(N) =1
                    d = 1;
                    do
                    {
                        d++;
                    } while ((e * d) % fi_n != 1);

                    

                    //Descrição: No alfabeto de A – Z a letra “C” equivale a 3ª letra, podemos atribuir o valor 3 em decimal.Deste modo, podemos criptografar a letra C usando o algoritmo RSA.                        
                    descricao = "The information security is of significant importance to ensure the privacy of communications";

                    //Criptografar: Chave Pública(e, N)
                    criptografado = Criptografar(descricao, e, n);

                    //Decriptografar: Chave Privada(d, N)
                    descriptografado = Descriptografar(criptografado, d, n);

                    string x = "1";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (erro);

            Console.WriteLine("Definir p e q");
            Console.WriteLine(String.Format("P: {0}", p));
            Console.WriteLine(String.Format("Q: {0}", q));
            Console.WriteLine("\n");
            Console.WriteLine("Etapa 1: n = p.q ");
            Console.WriteLine(String.Format("n: {0}", n));
            Console.WriteLine("\n");
            Console.WriteLine("Etapa 2: Φ(n) = (p-1).(q-1)");
            Console.WriteLine(String.Format("Φ(n): {0}", fi_n));
            Console.WriteLine("\n");
            Console.WriteLine("Etapa 3: 1 < e < Φ(N)");
            Console.WriteLine(String.Format("e: {0}", e));
            Console.WriteLine("\n");
            Console.WriteLine("Etapa 4: e.d mod Φ(N)=1");
            Console.WriteLine(String.Format("d: {0}", d));
            Console.WriteLine("\n");
            Console.WriteLine(String.Format("descrição: {0}", descricao));
            Console.WriteLine("\n");
            Console.WriteLine(String.Format("criptografado: {0}", criptografado));
            Console.WriteLine("\n");
            Console.WriteLine(String.Format("descriptografado: {0}", descriptografado));
            Console.WriteLine("\n");

            Console.ReadLine();
        }

        static int mmc(int a, int h)
        {
            int temp;
            while (true)
            {
                temp = a % h;
                if (temp == 0)
                    return h;
                a = h;
                h = temp;
            }
        }

        public static bool VerificaPrimo(int valor)
        {
            try
            {
                for (int i = 2; i < Math.Ceiling(Convert.ToDouble(valor / 2)); i++)
                {
                    int resto = valor % i;
                    if (resto == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao Verificar nº primo");
                Console.WriteLine(e);
            }
            return false;
        }

        public static string Criptografar(string descricao, int e, int n)
        {
            string criptografado = "";

            foreach (char c in descricao)
            {
                BigInteger aux = BigInteger.Pow(c, e);
                BigInteger unicode = aux % n;
                char character = (char)(unicode);
                criptografado += character.ToString();
            }            
            return criptografado;
        }

        public static string Descriptografar(string criptografado, int d, int n)
        {
            string descriptografado = "";

            foreach (char c in criptografado)
            {
                BigInteger aux = BigInteger.Pow(c, d);
                BigInteger unicode = aux % n;
                char character = (char)(unicode);
                descriptografado += character.ToString();
            }
            return descriptografado;
        }
    }
}
