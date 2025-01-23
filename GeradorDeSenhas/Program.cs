using System;
using System.IO;
using System.Timers;

class Program
{
    static int tempoMaximo = 15;
    static System.Timers.Timer? timer;
    static int tempoRestante = tempoMaximo;

    static void Main()
    {
        using (StreamWriter escrever = new StreamWriter("bkp.TXT"))
        {
            string border = new string('-', 50);
            string texto = "Bem Vindo ao Gerador de Senha";

            int larguraTela = Console.WindowWidth;
            int posicaoCentralizada = (larguraTela - texto.Length) / 2;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(posicaoCentralizada, 4);
            Console.WriteLine(texto);
            Console.ResetColor();

            Console.WriteLine("");
            Console.WriteLine("Me informe a quantidade de dígitos para a sua senha:");
            int quantidade_digitos;
            while (!int.TryParse(Console.ReadLine(), out quantidade_digitos) || quantidade_digitos <= 0)
            {
                Console.WriteLine("Por favor, insira um número válido maior que 0.");
            }

            Console.WriteLine("Na sua senha você deseja ter números? (sim=1, nao=0)");
            int numeros;
            while (!int.TryParse(Console.ReadLine(), out numeros) || (numeros != 0 && numeros != 1))
            {
                Console.WriteLine("Por favor, insira 0 ou 1.");
            }

            Console.WriteLine("Na sua senha você deseja ter letras minusculas? (sim=1, nao=0)");
            int letrasminus;
            while (!int.TryParse(Console.ReadLine(), out letrasminus) || (letrasminus != 0 && letrasminus != 1))
            {
                Console.WriteLine("Por favor, insira 0 ou 1.");
            }

            Console.WriteLine("Na sua senha você deseja ter letras maiusculas? (sim=1, nao=0)");
            int letrasmaius;
            while (!int.TryParse(Console.ReadLine(), out letrasmaius) || (letrasmaius != 0 && letrasmaius != 1))
            {
                Console.WriteLine("Por favor, insira 0 ou 1.");
            }

            Console.WriteLine("Na sua senha você deseja ter caracteres especiais? (sim=1, nao=0)");
            int caracteres;
            while (!int.TryParse(Console.ReadLine(), out caracteres) || (caracteres != 0 && caracteres != 1))
            {
                Console.WriteLine("Por favor, insira 0 ou 1.");
            }

            Console.WriteLine("");
            Console.WriteLine("Tipo de senha gerada:");
            Console.WriteLine($"Quantidade de dígitos: {quantidade_digitos}");
            Console.WriteLine($"Incluir números: {(numeros == 1 ? "Sim" : "Não")}");
            Console.WriteLine($"Incluir letras minusculas: {(letrasminus == 1 ? "Sim" : "Não")}");
            Console.WriteLine($"Incluir letras maiusculas: {(letrasmaius == 1 ? "Sim" : "Nao")}");
            Console.WriteLine($"Incluir caracteres especiais: {(caracteres == 1 ? "Sim" : "Não")}");

            string senha = GerarSenha(quantidade_digitos, numeros, letrasminus, letrasmaius, caracteres);
            ;Console.WriteLine($"Senha gerada: {senha}");
            escrever.WriteLine(border);
            escrever.WriteLine("                   Senha Gerada                   ");
            escrever.WriteLine(border);
            escrever.WriteLine($"Senha: {senha}");
            escrever.WriteLine(border);
            escrever.WriteLine("Esta senha foi gerada por um Gerador de Senha");
            escrever.WriteLine(border);

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += TimerElapsed;
            timer.Start();

            while (tempoRestante > 0)
            {
                Console.Clear();
                Console.WriteLine("");Console.WriteLine("");
                Console.WriteLine("Tipo de senha gerada:");Console.WriteLine("");Console.WriteLine("");
                Console.WriteLine($"Quantidade de dígitos: {quantidade_digitos}");
                Console.WriteLine($"Incluir números: {(numeros == 1 ? "Sim" : "Não")}");
                Console.WriteLine($"Incluir letras minusculas: {(letrasminus == 1 ? "Sim" : "Não")}");
                Console.WriteLine($"Incluir letras maiusculas: {(letrasmaius == 1 ? "Sim" : "Nao")}");
                Console.WriteLine($"Incluir caracteres especiais: {(caracteres == 1 ? "Sim" : "Não")}");
                Console.WriteLine("");Console.WriteLine("");
                Console.WriteLine($"Senha gerada: {senha}");
                Console.WriteLine("Sua senha foi salva no arquivo bkp.txt");
                Console.WriteLine("");Console.WriteLine("");Console.WriteLine("");Console.WriteLine("");
                Console.WriteLine($"Tempo restante para limpar o console: {tempoRestante}");
                System.Threading.Thread.Sleep(500);
            }

            timer.Stop();
            Console.Clear();
        }
    }

    static string GerarSenha(int tamanho, int incluirNumeros, int incluirLetrasminus, int incluirLetrasmaius, int incluirEspeciais)
    {
        const string numeros = "0123456789";
        const string letrasminus = "abcdefghijklmnopqrstuvwxyz";
        const string letrasmaius = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string especiais = "!@#$%^&*()";

        string caracteresDisponiveis = "";
        if (incluirNumeros == 1) caracteresDisponiveis += numeros;
        if (incluirLetrasminus == 1) caracteresDisponiveis += letrasminus;
        if (incluirLetrasmaius == 1) caracteresDisponiveis += letrasmaius;
        if (incluirEspeciais == 1) caracteresDisponiveis += especiais;

        if (string.IsNullOrEmpty(caracteresDisponiveis))
        {
            return "";
        }

        Random random = new Random();
        char[] senha = new char[tamanho];
        for (int i = 0; i < tamanho; i++)
        {
            senha[i] = caracteresDisponiveis[random.Next(caracteresDisponiveis.Length)];
        }
        return new string(senha);
    }

    static void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        tempoRestante--;
    }
}
