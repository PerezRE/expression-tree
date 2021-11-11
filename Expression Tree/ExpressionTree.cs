using Automata;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExpressionTree
{
    enum TokenType
    {
        ATOM, AND, OR, LB, RB, SIGN, THEN
    }

    public class ExpressionTree : AbstractAutomaton
    {
        private bool Sign { get; set; }
        private BTNode<Data> Root { get; set; }
        private Stack<BTNode<Data>> LeafNodes { get; set; }
        private Stack<BTNode<Data>> OperatorsNodes { get; set; }

        /// <summary>
        /// Final states
        /// </summary>
        private static readonly int[] F = new int[] { 2 };
        /// <summary>
        /// DFA
        /// </summary>
        private static readonly int[,] automata = new int[3, 7]
            {//ATOM AND OR LP RP +- =>      // States
                { 0, 0, 0, 0, 0, 0, 0 },    // 0
                { 2, 0, 0, 1, 0, 1, 0 },    // 1
                { 0, 1, 1, 0, 2, 0, 1 },    // 2
        };

        /// <summary>
        /// Construye un árbol a partir de una expresión dada, utilizando un automata y lenguaje definido.
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="InvalidOperationException"/>
        public ExpressionTree(string expression) : 
            base(@"(and)|(or)|(-)|(not)|(=>)|(\()|(\))", 1, F, automata)
        {
            Sign = true; // equals positive.
            LeafNodes = new Stack<BTNode<Data>>();
            OperatorsNodes = new Stack<BTNode<Data>>();
            
            string[] tokens = LexerScan(expression);
            
            if (!ProcessSymbols(tokens))
                throw new InvalidOperationException("Expression not accepted");
            
            if (LeafNodes.Count > 1 || OperatorsNodes.Count > 0)
                throw new InvalidOperationException("Parenthesis not balanced in expression");

            LeafNodes.Clear();
            OperatorsNodes.Clear();
        }

        private void Inorden(BTNode<Data> node, Action<BTNode<Data>> fn)
        {
            if (node == null) 
                return;

            Inorden(node.Left, fn);
            fn(node);
            Inorden(node.Right, fn);
        }

        private void Preorden(BTNode<Data> node, Action<BTNode<Data>> fn)
        {
            if (node == null) 
                return;

            fn(node);
            Preorden(node.Left, fn);
            Preorden(node.Right, fn);
        }

        private void Postorden(BTNode<Data> node, Action<BTNode<Data>> fn)
        {
            if (node == null) 
                return;

            Postorden(node.Left, fn);
            Postorden(node.Right, fn);
            fn(node);
        }

        /// <summary>
        /// Gets the following state according the current state and input token.
        /// </summary>
        /// <param name="s">Current state.</param>
        /// <param name="t">Token to process.</param>
        /// <returns>If token is accepted by DFA then it returns the following state, otherwise it will returns a dead state.</returns>
        sealed protected override int DeltaTransition(int s, string t)
        {
            BTNode<Data> node;
            switch (t)
            {
                case "-":
                case "not":
                    Sign = !Sign;
                    return _stateTable[s, (int)TokenType.SIGN];
                case "(":
                    OperatorsNodes.Push(new BTNode<Data>(new Data(Sign)));
                    if (!Sign)
                        Sign = true;
                    return _stateTable[s, (int)TokenType.LB];
                case ")":
                    try
                    {
                        Root = OperatorsNodes.Pop();
                        Root.Right = LeafNodes.Pop();
                        Root.Left = LeafNodes.Pop();
                        LeafNodes.Push(Root);
                    }
                    catch (InvalidOperationException)
                    {
                        throw new InvalidOperationException("Parenthesis not balanced in expression.");
                    }
                    return _stateTable[s, (int)TokenType.RB];
                case "=>":
                    node = OperatorsNodes.Peek();
                    node.Data.Value = t;
                    if (!Sign)
                        Sign = true;
                    return _stateTable[s, (int)TokenType.THEN];
                case "or":
                    node = OperatorsNodes.Peek();
                    node.Data.Value = t;
                    if (!Sign)
                        Sign = true;
                    return _stateTable[s, (int)TokenType.OR];
                case "and":
                    OperatorsNodes.Peek().Data.Value = t;
                    if (!Sign)
                        Sign = true;
                    return _stateTable[s, (int)TokenType.AND];
                default:
                    if (!Regex.IsMatch(t, @"[a-zA-Z][a-zA-Z]*"))
                        return 0;
                    LeafNodes.Push(new BTNode<Data>(new Data(Sign, t)));
                    if (!Sign)
                        Sign = true;
                    return _stateTable[s, (int)TokenType.ATOM];
            }
        }
    }
}