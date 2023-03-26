using Autofac;
using DependenciesDemystified.Core.Children;
using DependenciesDemystified.Core.Logging;
using DependenciesDemystified.Core.Parents;
using DependenciesDemystified.Core.Products;
using Spackle;
using System.ComponentModel;

namespace DependenciesDemystified.Core;

public sealed class CoreModule
	: Module
{
	protected override void Load(ContainerBuilder builder)
	{
		base.Load(builder);

		builder.RegisterType<Logger>().As<ILogger>();
		builder.RegisterType<SecureRandom>().As<SecureRandom>().SingleInstance();

		builder.Register(c => new Func<ProductChoices, IProduct>(
			choice => choice switch
			{
				ProductChoices.Car => new DodgeViperProduct(),
				ProductChoices.Computer => new SurfaceProduct(),
				ProductChoices.GameConsole => new XboxOneProduct(),
				_ => throw new InvalidEnumArgumentException($"The product choice value, {choice}, is invalid.")
			}));

		builder.Register<IParent>(context =>
			context.Resolve<SecureRandom>().Next(0, 2) switch
			{
				0 => new LizAsParent(context.Resolve<SecureRandom>()),
				_ => new JasonAsParent()
			});

		builder.RegisterType<ComplexDependentChild>().As<IChild>();
	}
}