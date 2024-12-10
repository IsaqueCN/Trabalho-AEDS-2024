using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AEDS_2024
{
    internal class Carta
    {
        public static string[] naipes = { "Espadas", "Copas", "Paus", "Ouros" };
        private int numero;
        private string naipe;

        public int Numero
        {
            get => numero;
            set => numero = value;
        }

        public string Naipe
        {
            get => naipe;
            set => naipe = value;
        }
        public string Nome => $"{naipe}-{numero}";

        public Carta(int numero, string naipe)
        {
            this.numero = numero;
            this.naipe = naipe;
        }
    }
}
