using System.Linq;
using System.Text.RegularExpressions;

namespace Automata
{
    public abstract class AbstractAutomaton
    {
        /// <summary>
        /// Estado inicial.
        /// </summary>
        protected readonly int _s;
        /// <summary>
        /// Conjunto de estados finales o de aceptación.
        /// </summary>
        protected readonly int[] _f;
        /// <summary>
        /// Estados (Q) del automata.
        /// </summary>
        protected readonly int[,] _stateTable;
        /// <summary>
        /// Expresión regular equivalente al automata, i.e., está conformado por el alfabeto ∑.
        /// </summary>
        private readonly string _regex;

        /// <summary>
        /// Construye un automata finito determinista.
        /// </summary>
        /// <param name="F">Conjunto de estados finales.</param>
        /// <param name="stateTable">Tabla de estados o transición.</param>
        public AbstractAutomaton(string regex, int s, int[] F, int[,] stateTable)
        {
            _s = s;
            _f = F;
            _regex = regex;
            _stateTable = stateTable;
        }

        /// <summary>
        /// Función de transición de estados, donde lee el estado actual y un símbolo leído del alfabeto ∑.
        /// </summary>
        /// <param name="s">Estado actual del automata.</param>
        /// <param name="c">Token o símbolo a evaluar.</param>
        /// <returns>Devuelve el estado siguiente Q(K,∑).</returns>
        protected abstract int DeltaTransition(int s, string c);

        /// <summary>
        /// Escaner léxico. Parser que permite generar tokens a partir de la expresión regular dada, tal que los tokens forman parte del alfabeto ∑.
        /// </summary>
        /// <param name="expression">Expresión a tokenizar</param>
        /// <returns> Devuelve un arreglo con los lexemas leídos.</returns>
        protected string[] LexerScan(string expression)
        {
            return Regex.Split(expression, _regex).Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToArray();
        }

        /// <summary>
        /// Algoritmo del automata.
        /// Evalua una cadena de símbolos e indica si pertenece al lenguaje de este automata.
        /// </summary>
        /// <param name="tokens">Símbolos o tokens que forman parte del alfabeto ∑.</param>
        /// <returns>Devuelve verdadero si el estado es de aceptación.</returns>
        protected virtual bool ProcessSymbols(string[] tokens)
        {
            // Estado inicial.
            int s = _s;

            // Recorrer la cadena de entrada.
            for(int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];
                
                // Obtener el siguiente estado para Q(K,∑)
                s = DeltaTransition(s, token);
                
                // Si el estado actual no es un estado muerto, i.e., Q(K,∑) != 0
                if (s == 0)
                    break;
            }

            // Si S está en F devolver el estado.
            return _f.Contains(s);
        }
    }
}