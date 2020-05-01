using System;

namespace DesafioSelecaoMercadorGalaxia
{
    class Program
    {
        static void Main(string[] args)
        { 
            try
            {
                if (args.Length > 1)
                {
                    Console.WriteLine("Erro: Programa aceita no máximo um argumento.");
                    System.Console.ReadKey();
                    return;
                }

                String file;
                if(args.Length == 1)
                {
                    file = args[0];
                }
                else
                {
                    file = "InputFile.txt";
                }

                FileTranslator fileTranslator = new FileTranslator(file);
                String result = fileTranslator.Translate();
                Console.WriteLine(result.Trim());
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

        }
    }
}
