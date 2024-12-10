using System.Data;

namespace Trabalho_AEDS_2024
{
    internal class Program
    {
        public static Random rand = new Random();
        public static List<Carta[]> baralhos;
        public static Jogador[] jogadores;
        public static Mesa mesa;
        static void Main(string[] args)
        {
            Console.WriteLine("Digite quantos jogadores o jogo irá ter: ");
            int qtdJogadores = int.Parse(Console.ReadLine());
            DefinirJogadores(qtdJogadores);

            Registrador.Comecar(jogadores);

            bool sair = false;
            while (sair == false)
            {
                for (int i = 0; i < jogadores.Length; i++)
                {
                    jogadores[i].Monte.Clear();
                }
                Console.Clear();
                Console.WriteLine("Uma nova partida está prestes a começar!\n");
                Console.WriteLine("Digite quantos baralhos a partida irá ter: ");
                int qtdBaralhos = int.Parse(Console.ReadLine());
                DefinirBaralhos(qtdBaralhos);

                mesa = new Mesa();

                bool vitoria = false;
                int turnoAtual = 0;
                while (vitoria == false)
                {
                    Jogador jogadorAtual = jogadores[turnoAtual];

                    Registrador.EscreverTurno(jogadorAtual.Nome);
                    bool jogadaEncerrada = false;
                    while (jogadaEncerrada == false)
                    {
                        if (mesa.MonteDeCompras.Count == 0)
                        {
                            vitoria = true;
                            Registrador.Escrever("Não há mais cartas no monte de compra, o jogo acabou!");
                            break;
                        }

                        Carta cartaDaVez = mesa.MonteDeCompras[0];
                        mesa.MonteDeCompras.RemoveAt(0);

                        Registrador.Escrever($"\nO jogador {jogadorAtual.Nome} retirou a carta {cartaDaVez.Nome} do Monte de Compras");

                        Jogador jogadorRoubado = TentarRoubarMontes(jogadorAtual, cartaDaVez);

                        if (jogadorRoubado != null)
                        {
                            Registrador.Escrever($"O jogador {jogadorAtual.Nome} roubou a carta {cartaDaVez.Nome} do jogador {jogadorRoubado.Nome}!");
                            jogadorAtual.Monte.Insert(0, cartaDaVez);
                        }
                        else
                        {
                            Carta areaDeDescarte = TentarPegarAreaDescarte(jogadorAtual, cartaDaVez);
                            if (areaDeDescarte != null)
                            {
                                Registrador.Escrever($"O jogador {jogadorAtual.Nome} pegou a carta {areaDeDescarte.Nome} da Área de Descarte!");
                                jogadorAtual.Monte.Insert(0, cartaDaVez);
                            }
                            else if (jogadorAtual.Monte.Count != 0 && jogadorAtual.Monte[0].Numero == cartaDaVez.Numero)
                            {
                                jogadorAtual.Monte.Insert(0, cartaDaVez);
                            }
                            else
                            {
                                mesa.AreaDeDescarte.Add(cartaDaVez);
                                jogadaEncerrada = true;
                            }
                        }
                    }
                    Console.ReadLine();
                    turnoAtual = (turnoAtual + 1) % qtdJogadores;
                }
                Jogador[] ranking = DefinirRanking(jogadores);

                Registrador.Escrever($"\nO jogador {ranking[0].Nome} ganhou!! ({ranking[0].Posicao}° posição com {ranking[0].QtdCartasUltimaPartida} cartas)");

                Registrador.Escrever("\nRanking da partida: ");
                for (int i = 0; i < ranking.Length; i++)
                {
                    Registrador.Escrever($"{i + 1}° - {ranking[i].Nome} - {ranking[i].Monte.Count} cartas");
                }

                Registrador.EncerrarPartida(ranking);

                while (true)
                {
                    char resposta;
                    do
                    {
                        Console.WriteLine("\nVocê quer visualizar o ranking de algum jogador? (S/N)");
                        resposta = char.Parse(Console.ReadLine().ToLower());
                    } while (resposta != 's' && resposta != 'n');

                    if (resposta == 'n')
                        break;

                    Console.WriteLine("Digite o nome do jogador: ");
                    Jogador busca = EncontrarJogador(Console.ReadLine());

                    if (busca == null)
                        Console.WriteLine("Jogador não encontrado.");
                    else
                    {
                        busca.MostrarHistorico();
                    }
                }

                char resp;
                do
                {
                    Console.WriteLine("Você quer começar uma nova partida? (S/N)");
                    resp = char.Parse(Console.ReadLine().ToLower());
                } while (resp != 's' && resp != 'n');

                if (resp == 'n')
                    sair = true;
                else
                    Registrador.NovaPartida(jogadores);
            }
        }

        public static void DefinirBaralhos(int qtdBaralhos)
        {
            baralhos = new List<Carta[]>();

            for (int i = 0; i < qtdBaralhos; i++)
            {
                baralhos.Add(Baralho.criarBaralho());
                Registrador.Escrever($"Baralho {i + 1} criado com {baralhos[i].Length} cartas");
            }
        }

        public static void DefinirJogadores(int qtdJogadores)
        {
            jogadores = new Jogador[qtdJogadores];

            for (int i = 0; i < qtdJogadores; i++)
            {
                Console.WriteLine($"Digite o nome do {i + 1}° jogador: ");
                string nome = Console.ReadLine();
                jogadores[i] = new Jogador(nome);
            }
        }

        public static Jogador[] DefinirRanking(Jogador[] jogadores)
        {
            Jogador[] ranking = new Jogador[jogadores.Length];

            for (int i = 0; i < jogadores.Length; i++)
            {
                ranking[i] = jogadores[i];
            }

            for (int i = 0; i < ranking.Length; i++)
            {
                int maior = i;
                for (int j = i + 1; j < ranking.Length; j++)
                {
                    if (ranking[j].Monte.Count > ranking[i].Monte.Count)
                        maior = j;
                }
                Jogador temp = ranking[i];
                ranking[i] = ranking[maior];
                ranking[maior] = temp;

                ranking[i].AdicionarRanking(i + 1);
            }

            return ranking;
        }
        public static Jogador EncontrarJogador(string nome)
        {
            for (int i = 0; i < jogadores.Length; i++)
            {
                if (jogadores[i].Nome.ToLower() == nome.ToLower())
                    return jogadores[i];
            }
            return null;
        }
        public static Carta TentarPegarAreaDescarte(Jogador jogadorAtual, Carta cartaDaVez)
        {
            for (int i = 0; i < mesa.AreaDeDescarte.Count; i++)
            {
                Carta cartaAtual = mesa.AreaDeDescarte[i];
                if (cartaAtual.Numero == cartaDaVez.Numero)
                {
                    jogadorAtual.Monte.Insert(0, cartaAtual);
                    mesa.AreaDeDescarte.RemoveAt(i);
                    return jogadorAtual.Monte[0];
                }
            }
            return null;
        }
        public static Jogador TentarRoubarMontes(Jogador jogadorAtual, Carta cartaDaVez)
        {
            List<int> jogadoresParaRoubar = new List<int>();

            //Adicionar jogadores que tem a carta do topo igual ao jogador atual
            for (int i = 0; i < jogadores.Length; i++)
            {
                if (jogadores[i] == jogadorAtual)
                    continue;

                if (jogadores[i].Monte.Count > 0 && jogadores[i].Monte[0].Numero == cartaDaVez.Numero)
                {
                    jogadoresParaRoubar.Add(i);
                }
            }

            if (jogadoresParaRoubar.Count == 0)
                return null;

            Jogador jogadorEscolhido;
            if (jogadoresParaRoubar.Count == 1)
            {
                jogadorEscolhido = jogadores[jogadoresParaRoubar[0]];

                jogadorAtual.Monte.Insert(0, jogadorEscolhido.Monte[0]);
                jogadorEscolhido.Monte.RemoveAt(0);
                return jogadorEscolhido;
            }

            Jogador JogadorMaiorMonte = JogadorComMaiorMonte(jogadoresParaRoubar);
            List<Jogador> JogadoresIguais = JogadoresComMontesDeTamanho(jogadoresParaRoubar, JogadorMaiorMonte.Monte.Count);

            if (JogadoresIguais.Count == 1)
                jogadorEscolhido = JogadorMaiorMonte;
            else
                jogadorEscolhido = JogadoresIguais[rand.Next(0, JogadoresIguais.Count)];

            jogadorAtual.Monte.Insert(0, jogadorEscolhido.Monte[0]);
            jogadorEscolhido.Monte.RemoveAt(0);
            return jogadorEscolhido;
        }

        // Retorna o jogador que tem o maior monte dentre a lista de posições de jogadores
        public static Jogador JogadorComMaiorMonte(List<int> indexJogadores)
        {
            int posMaiorTamanho = 0;
            for (int i = 0; i < indexJogadores.Count; i++)
            {
                List<Carta> MonteMaior = jogadores[indexJogadores[posMaiorTamanho]].Monte;
                List<Carta> MonteI = jogadores[indexJogadores[i]].Monte;

                if (MonteI.Count > MonteMaior.Count)
                    posMaiorTamanho = i;
            }
            return jogadores[indexJogadores[posMaiorTamanho]];
        }

        // Retorna os jogadores que tem um monte com tamanho especifico dentre a lista de posições de jogadores
        public static List<Jogador> JogadoresComMontesDeTamanho(List<int> indexJogadores, int tamanho)
        {
            List<Jogador> resultado = new List<Jogador>();
            for (int i = 0; i < indexJogadores.Count; i++)
            {
                if (jogadores[indexJogadores[i]].Monte.Count == tamanho)
                    resultado.Add(jogadores[indexJogadores[i]]);
            }
            return resultado;
        }
    }
}
