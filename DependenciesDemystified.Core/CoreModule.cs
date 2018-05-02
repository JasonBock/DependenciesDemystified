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
	public class MyModule : Module { }

	public sealed class CoreModule
		: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterModule<MyModule>();

			builder.RegisterType<Logger>().As<ILogger>();
			builder.RegisterType<SecureRandom>().As<Random>().SingleInstance();
			builder.Register<Func<ProductChoices, IProduct>>(c =>
			{
				return new Func<ProductChoices, IProduct>(p =>
				{
					switch (p)
					{
						case ProductChoices.Car:
							return new TeslaProduct();
						case ProductChoices.Computer:
							return new SurfaceProduct();
						case ProductChoices.GameConsole:
							return new XboxOneProduct();
						default:
							throw new InvalidEnumArgumentException($"The product choice value, {p}, is invalid.");
					}
				});
			});
			builder.Register<IParent>(c =>
			{
				var random = c.Resolve<Random>();

				if (random.Next(0, 2) == 0)
				{
					return new LizAsParent(c.Resolve<Random>());
				}
				else
				{
					return new JasonAsParent();
				}
			});
			builder.RegisterType<ComplexDependentChild>().As<IChild>();
		}
	}
}