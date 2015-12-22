using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GeradorDeRegistros
{
    class Program
    {
        static void Main(string[] args)
        {

            int qtRegistros = 100000;
            int qtUsuarios = 100;
            List<string> registros = new List<string>();

            int[] regPusr = new int[qtUsuarios]; // armazena quantidade de registros pro usuário
            Random rnd = new Random();
            int _qtUsuarios = qtUsuarios;

            for (int i = 0; i < qtUsuarios; i++)
            {
                int qt = 0;

                if(i < (qtUsuarios-1))
                    qt = rnd.Next(1, (qtRegistros / _qtUsuarios));
                else
                    qt = qtRegistros;

                regPusr[i] = qt;
                qtRegistros = qtRegistros - qt;
                _qtUsuarios--;
            }

            // Exemplo de entrada:
            // 177.126.180.83 - - [15/Aug/2013:13:54:38 -0300] "GET /meme.jpg HTTP/1.1" 200 2148 "-" "userid=5352b590-05ac-11e3-9923-c3e7d8408f3a"
            // 177.126.180.83 - - [{0} -0300] "GET /lolcats.jpg HTTP/1.1" 200 2148 "-" "{1}"
            // 177.126.180.83 - - [{0} -0300] \"GET /lolcats.jpg HTTP/1.1\" 200 2148 \"-\" \"{1}\"
            // {0} = Data; {1} = UserId

            for (int k = 0; k < regPusr.Count(); k++)
            {
                Console.WriteLine("Usuário " + k + " :" + regPusr[k]);
            }
            Console.ReadLine();

            for (int j = 0; j < regPusr.Count(); j++)
            {
                Guid usrId = Guid.NewGuid();
                
                for (int k = 0; k < regPusr[j]; k++)
                {
                    DateTime data = DateTime.UtcNow.AddDays(rnd.Next(1, 365) * -1);
                    data = data.AddDays(rnd.NextDouble());
                    registros.Add(string.Format(
                        "{0}|177.126.180.83 - - [{1} -0300] \"GET /lolcats.jpg HTTP/1.1\" 200 2148 \"-\" \"{2}\""
                        , data.ToString("yyyyMMddHHmmss")
                        , data.ToString("dd/MMM/yyyy:HH:mm:ss")
                        , usrId.ToString()));
                }
            }

            registros.Sort();

            StreamWriter file = new StreamWriter(@"C:\Vix\httpBalanced.txt");
            file.Flush();

            foreach (string reg in registros)
            {
                file.WriteLine(reg.Split('|')[1]);
            }
        }
    }
}
