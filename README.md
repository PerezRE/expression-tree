# Expression Tree
As part of automata theory course, it is a practical example about the construction of an expression tree given a language through an automata which validates whether the word (i.e., expression given) is accepted using a "lexer scan" through a regular expression so that it generates tokens. Being like that it could be easily used as a tiny library of an automaton just removing some classes we are not interested on. It could be applied also for constructing another kind of tree as soon as it requires evaluation while it´s being constructed.

## Features
Generates an epxression tree from a regular expression.

## DFA
A Deterministic Finit Automaton is a tuple which has an alphabet, transition function, states, final states, and initial states. As the name call it, it´s deterministec, i.e., for each symbol in alphabet there is a transition to go from the current state to another.

## NFA
It´s almost identical from DFA, the unique difference between this and DFA it´s that Nondeterministic Finite Automaton (NFA) is not deterministic (that´s why the given name), i.e., for one symbol in alphabet, the current state may change to multiple states. So it would be hard to figure out where would be the next state. This automata is more complex to code, and some cases impossible.
