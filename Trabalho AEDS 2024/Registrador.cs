using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AEDS_2024
{
    internal class Registrador
    {
        private static int contadorPartidas = 1;
        private static int contadorTurno = 1;
        private static string diretorio = "Relatorio.txt";
        private static string relatorioGeral = "----- RELATORIO GERAL -----\n";
        private static string andamentoDoJogo = "----- ANDAMENTO DO JOGO -----\n\n";

        private static StreamWriter writerAdicionar
        {
            get { return new StreamWriter(diretorio, true, Encoding.UTF8); }
        }
        private static StreamWriter writerSubstituir
        {
            get { return new StreamWriter(diretorio, false, Encoding.UTF8); }
        }

        public static int ContadorTurno
        {
            get => contadorTurno;
            set => contadorTurno = value;
        }
        public static void Comecar(Jogador[] jogadores)
        {
            StreamWriter Writer = writerSubstituir;
            string texto = $"O jogo Rouba Montes está começando com {jogadores.Length} jogadores: ";
            for (int i = 0; i < jogadores.Length; i++)
            {
                texto += $"{jogadores[i].Nome} ";
            }
            texto += $"\n\n======== UMA NOVA PARTIDA ESTÁ INICIANDO ========\n";

            andamentoDoJogo += $"{texto}\n";
            Writer.WriteLine(andamentoDoJogo);
            Writer.Close();
        }

        public static void Escrever(string texto)
        {
            StreamWriter Writer = writerAdicionar;
            Writer.WriteLine(texto);
            Console.WriteLine(texto);
            andamentoDoJogo += $"{texto}\n";

            Writer.Close();
        }
        public static void EscreverTurno(string nomeJogador)
        {
            StreamWriter Writer = writerAdicionar;
            string texto = $"\n======== TURNO {contadorTurno} ========\n\nO jogador {nomeJogador} está jogando.";
            Writer.WriteLine(texto);
            Console.WriteLine(texto);
            andamentoDoJogo += $"{texto}\n";
            Writer.Close();
            AtualizarRelatorio();
            contadorTurno++;
        }

        public static void AtualizarRelatorio()
        {
            StreamWriter Writer = writerSubstituir;
            if (relatorioGeral == "----- RELATORIO GERAL -----\n")
                Writer.WriteLine($"{andamentoDoJogo}");
            else
                Writer.WriteLine($"{relatorioGeral}{andamentoDoJogo}");

            Writer.Close();
        }

        public static void EncerrarPartida(Jogador[] ranking)
        {
            relatorioGeral += $"\nA partida {contadorPartidas} durou {contadorTurno - 1} turnos e o jogador {ranking[0].Nome} ganhou!\nRanking da partida {contadorPartidas}:\n";
            for (int i = 0; i < ranking.Length; i++)
            {
                relatorioGeral += ($"{i + 1}° - {ranking[i].Nome} - {ranking[i].Monte.Count} cartas\n");
            }
            relatorioGeral += "\n";
            AtualizarRelatorio();
            contadorPartidas++;
        }
        public static void NovaPartida(Jogador[] jogadores)
        {
            string texto =  $"\n======== UMA NOVA PARTIDA ESTÁ INICIANDO ========\n\nJogadores da partida: ";
            for (int i = 0; i < jogadores.Length; i++)
            {
                texto += $"{jogadores[i].Nome} ";
            }
            texto += "\n";

            andamentoDoJogo += $"{texto}\n";
            AtualizarRelatorio();
            contadorTurno = 1;
        }
    }
}
