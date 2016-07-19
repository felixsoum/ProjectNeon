using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;


public class CharacterState : State<CharacterState.Type>
{
	public enum Type
	{
		Idle,
		Jump,
		Fall,
		Attack,
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
		Set(Type.Idle);
	}
}
