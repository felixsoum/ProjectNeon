using System.Collections.Generic;
using System;
using UnityEngine;

public class State<T> : MonoBehaviour
{
	private T current = default(T);

	private Dictionary<T, Action> actionEnter = new Dictionary<T, Action>();
	private Dictionary<T, Action> actionExit = new Dictionary<T, Action>();
	private Dictionary<T, Action> actionUpdate = new Dictionary<T, Action>();
	
	public void AddActionEnter( T state, Action action )
	{
		AddStateAction(actionEnter, state, action);
	}
	public void AddActionExit( T state, Action action )
	{
		AddStateAction(actionExit, state, action);
	}
	public void AddActionUpdate( T state, Action action )
	{
		AddStateAction(actionUpdate, state, action);
	}
	public void AddActionEnter( IEnumerable<T> states, Action action )
	{
		foreach( var state in states)
		{
			AddStateAction(actionEnter, state, action);
		}
	}
	public void AddActionExit( IEnumerable<T> states, Action action )
	{
		foreach( var state in states )
		{
			AddStateAction(actionExit, state, action);
		}
	}
	public void AddActionUpdate( IEnumerable<T> states, Action action )
	{
		foreach( var state in states )
		{
			AddStateAction(actionUpdate, state, action);
		}
	}

	public void Set( T state )
	{
		if( actionExit.ContainsKey(state) )
		{
			actionExit[state]();
		}

		current = state;

		if( actionEnter.ContainsKey(state) )
		{
			actionEnter[state]();
		}
	}

	public T Get()
	{
		return current;
	}

	void Update()
	{
		if( actionUpdate.ContainsKey(current) )
		{
			actionUpdate[current]();
		}
	}

	private void AddStateAction( Dictionary<T, Action> map, T state, Action action )
	{
		if( !map.ContainsKey(state) )
		{
			map[state] = action;
		}
		else
		{
			map[state] += action;
		}
	}
}
