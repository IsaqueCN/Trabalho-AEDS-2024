using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AEDS_2024
{
    internal class Baralho
    {
        public static Carta[] criarBaralho()
        {
            Carta[] monte = new Carta[52];
            for (int i = 0; i < Carta.naipes.Length; i++)
            {
                string naipeAtual = Carta.naipes[i];
                for (int j = 1; j < 14; j++)
                {
                    monte[(13 * i - 1) + j] = new Carta(j, naipeAtual);
                }
            }
            return monte;
        }

        public static void AleatorizarBaralho(Carta[] baralho)
        {
            List<int> PosicoesNaoAcessadas = new List<int>(baralho.Length);

            for (int i = 0; i < baralho.Length; i++)
            {
                PosicoesNaoAcessadas.Add(i);
            }

            while (PosicoesNaoAcessadas.Count > 0)
            {
                int aleatorio1 = Program.rand.Next(0, PosicoesNaoAcessadas.Count);
                int aleatorio2 = Program.rand.Next(0, PosicoesNaoAcessadas.Count);

                int posicaoAleatoria1 = PosicoesNaoAcessadas[aleatorio1];
                int posicaoAleatoria2 = PosicoesNaoAcessadas[aleatorio2];

                Carta temp = baralho[posicaoAleatoria1];
                baralho[posicaoAleatoria1] = baralho[posicaoAleatoria2];
                baralho[posicaoAleatoria2] = temp;

                PosicoesNaoAcessadas.Remove(posicaoAleatoria1);
                PosicoesNaoAcessadas.Remove(posicaoAleatoria2);
            }
        }
    }
}
