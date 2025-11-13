using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServicesRegistration
{
	public static void ConfigureApplicationServices(this IServiceCollection services)
	{
		// AutoMapper
		services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
		// MediatR 
		services.AddMediatR(config =>
			config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

	}
}
