using Autofac;
using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Logging;
using DependenciesDemystified.Core.Parents;
using DependenciesDemystified.Core.Products;
using Spackle;
using System;
using System.ComponentModel;

namespace DependenciesDemystified.Core
{
	public sealed class CoreModule
		: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<Logger>().As<ILogger>();
			builder.RegisterType<SecureRandom>().As<Random>().SingleInstance();
			
			builder.Register(c => new Func<ProductChoices, IProduct>(
					p => p switch
					{
						ProductChoices.Car => new TeslaProduct(),
						ProductChoices.Computer => new SurfaceProduct(),
						ProductChoices.GameConsole => new XboxOneProduct(),
						_ => throw new InvalidEnumArgumentException($"The product choice value, {p}, is invalid.")
					}));
			
			builder.Register<IParent>(c => 
				c.Resolve<Random>().Next(0, 2) switch
				{
					0 => new LizAsParent(c.Resolve<Random>()),
					_ => new JasonAsParent()
				});
				
			builder.RegisterType<ComplexDependentChild>().As<IChild>();
		}
	}
}