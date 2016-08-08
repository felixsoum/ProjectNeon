using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;


public class EnemyState : State<EnemyState.Type>
{
	public enum Type
	{
		Move,
		Range,
		Melee
	}

	// Returns list of character state types that are not the specified types.
	public static IEnumerable<Type> Not( IEnumerable<Type> types )
	{
		var matching = new List<Type>();
		foreach( Type type in Enum.GetValues(typeof(Type)) )
		{
			if( !types.Contains(type) )
			{
				matching.Add(type);
			}
		}
		return matching;
	}
	public static IEnumerable<Type> Not( Type type )
	{
		return Not(new List<Type>() { type });
	}

	void Start()
	{
		Set(Type.Move);
	}
}
