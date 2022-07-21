namespace Microsoft.Extensions.Configuration;

public static class EnvConfigurationExtensions
{
	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path)
	{
		return builder.AddEnvFile(null, path, optional: false, reloadOnChange: false);
	}

	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path, bool optional)
	{
		return builder.AddEnvFile(null, path, optional, reloadOnChange: false);
	}

	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
	{
		return builder.AddEnvFile(null, path, optional, reloadOnChange);
	}

	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
	{
		if (builder == null)
		{
			throw new ArgumentNullException("builder");
		}
		if (string.IsNullOrEmpty(path))
		{
			throw new ArgumentException("Invalid file path specified", "path");
		}
		return builder.AddEnvFile(delegate (EnvConfigurationSource s)
		{
			s.FileProvider = provider;
			s.Path = path;
			s.Optional = optional;
			s.ReloadOnChange = reloadOnChange;
			s.ResolveFileProvider();
		});
	}

	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, Action<EnvConfigurationSource> configureSource)
	{
		return builder.Add(configureSource);
	}

	public static IConfigurationBuilder AddEnvStream(this IConfigurationBuilder builder, Stream stream)
	{
		if (builder == null)
		{
			throw new ArgumentNullException("builder");
		}
		return builder.Add(delegate (EnvStreamConfigurationSource s)
		{
			s.Stream = stream;
		});
	}
}
