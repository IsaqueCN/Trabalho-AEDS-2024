using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AEDS_2024
{
    internal class Jogador
    {
        private string nome;
        private int posicao;
        private FilaFlexivel ranking = new FilaFlexivel();
        private List<Carta> monte = new List<Carta>();
        private int qtdCartasUltimaPartida;

        public string Nome => nome;
        public int Posicao
        {
            get => posicao;
            set => posicao = value;
        }
        public FilaFlexivel Ranking
        {
            get => ranking;
            set => ranking = value;
        }
        public List<Carta> Monte
        {
            get => monte;
            set => monte = value;
        }
        public int QtdCartasUltimaPartida
        {
            get => qtdCartasUltimaPartida;
            set => qtdCartasUltimaPartida = value;
        }
        public Jogador(string Nome)
        {
            nome = Nome;
            posicao = 0;
        }

        public void AdicionarRanking(int posicao)
        {
            this.posicao = posicao;
            qtdCartasUltimaPartida = monte.Count;

            if (ranking.Count == 5)
            {
                ranking.Remover();
            }
            ranking.Inserir(posicao);
        }

        public void MostrarHistorico()
        {
            Console.WriteLine($"\nHistórico de posições do Jogador {nome}: (Mais antigo para a mais recente)");
            ranking.Mostrar();
        }
    }
}
