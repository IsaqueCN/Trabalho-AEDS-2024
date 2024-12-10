using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AEDS_2024
{
    internal class Mesa
    {
        List<Carta> monteDeCompras;
        List<Carta> areaDeDescarte = new List<Carta>();

        public List<Carta> MonteDeCompras
        {
            get => monteDeCompras;
            set => monteDeCompras = value;
        }
        public List<Carta> AreaDeDescarte
        {
            get => areaDeDescarte;
            set => areaDeDescarte = value;
        }
        public Mesa()
        {
            monteDeCompras = new List<Carta>(Program.baralhos.Count*52);

            for (int i = 0; i < Program.baralhos.Count; i++)
            {
                Baralho.AleatorizarBaralho(Program.baralhos[i]);
                for (int j = 0; j < Program.baralhos[i].Length; j++)
                {
                    monteDeCompras.Add(Program.baralhos[i][j]);
                }
            }

            Registrador.Escrever($"Monte de Compras criado com {monteDeCompras.Count} cartas");
        }

        public void MostrarMonteDeCompras()
        {
            for (int i = 0; i < monteDeCompras.Count; i++)
            {
                Console.WriteLine($"{monteDeCompras[i].Naipe}-{monteDeCompras[i].Numero}");
            }
        }
    }
}
