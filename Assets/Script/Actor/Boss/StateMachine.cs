using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    private List<Transition> _anyTransitions = new List<Transition>();

    private static List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }
        _currentState?.Tick();
    }

    public void SetState(IState _state)
    {
        Debug.Log(_state);
        if (_state == _currentState)
        {
            return;
        }

        _currentState?.OnExit();
        _currentState = _state;

        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
        {
            _currentTransitions = EmptyTransitions;
        }

        _currentState.OnEnter();
    }

    public void AddTransition(IState _from, IState _to, Func<bool> _predicate)
    {
        if (_transitions.TryGetValue(_from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[_from.GetType()] = transitions;
        }

        transitions.Add(new Transition(_to, _predicate));
    }

    public void AddAnyTransition(IState _state, Func<bool> _predicate)
    {
        _anyTransitions.Add(new Transition(_state, _predicate));
    }


    public class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }

        public Transition (IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in _anyTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        foreach (var transition in _currentTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }
        return null;
    }
}
