using System;
using System.Collections.Generic;

namespace DependenciesDemystified.Core
{
	// Lifted from: http://ayende.com/blog/2886/building-an-ioc-container-in-15-lines-of-code
	public sealed class SimpleContainer
	{
		private readonly Dictionary<Type, Func<SimpleContainer, object>> typeToCreator = 
			new Dictionary<Type, Func<SimpleContainer, object>>();

		public void Register<T>(Func<SimpleContainer, object> creator) => 
			this.typeToCreator.Add(typeof(T), creator);

		public T Resolve<T>() => 
			(T)this.typeToCreator[typeof(T)](this);
	}
}