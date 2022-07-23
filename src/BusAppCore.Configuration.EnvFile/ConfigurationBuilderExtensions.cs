namespace Microsoft.Extensions.Configuration;

/// <summary>
/// Extension methods for adding <see cref="EnvConfigurationProvider"/>.
/// </summary>
public static class EnvConfigurationExtensions
{
	/// <summary>
	/// Adds the .env configuration provider at <paramref name="path"/> to <paramref name="builder"/>.
	/// </summary>
	/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
	/// <param name="path">Path relative to the base path stored in
	/// <see cref="IConfigurationBuilder.Properties"/> of <paramref name="builder"/>.</param>
	/// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path)
	{
		return builder.AddEnvFile(null, path, optional: false, reloadOnChange: false);
	}

	/// <summary>
	/// Adds the .env configuration provider at <paramref name="path"/> to <paramref name="builder"/>.
	/// </summary>
	/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
	/// <param name="path">Path relative to the base path stored in
	/// <see cref="IConfigurationBuilder.Properties"/> of <paramref name="builder"/>.</param>
	/// <param name="optional">Whether the file is optional.</param>
	/// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path, bool optional)
	{
		return builder.AddEnvFile(null, path, optional, reloadOnChange: false);
	}

	/// <summary>
	/// Adds the .env configuration provider at <paramref name="path"/> to <paramref name="builder"/>.
	/// </summary>
	/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
	/// <param name="path">Path relative to the base path stored in
	/// <see cref="IConfigurationBuilder.Properties"/> of <paramref name="builder"/>.</param>
	/// <param name="optional">Whether the file is optional.</param>
	/// <param name="reloadOnChange">Whether the configuration should be reloaded if the file changes.</param>
	/// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
	{
		return builder.AddEnvFile(null, path, optional, reloadOnChange);
	}

	/// <summary>
	/// Adds a .env configuration source to <paramref name="builder"/>.
	/// </summary>
	/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
	/// <param name="provider">The <see cref="IFileProvider"/> to use to access the file.</param>
	/// <param name="path">Path relative to the base path stored in
	/// <see cref="IConfigurationBuilder.Properties"/> of <paramref name="builder"/>.</param>
	/// <param name="optional">Whether the file is optional.</param>
	/// <param name="reloadOnChange">Whether the configuration should be reloaded if the file changes.</param>
	/// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
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

	/// <summary>
	/// Adds a .env configuration source to <paramref name="builder"/>.
	/// </summary>
	/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
	/// <param name="configureSource">Configures the source.</param>
	/// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
	public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, Action<EnvConfigurationSource> configureSource)
	{
		return builder.Add(configureSource);
	}

	/// <summary>
	/// Adds a .env configuration source to <paramref name="builder"/>.
	/// </summary>
	/// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
	/// <param name="stream">The <see cref="Stream"/> to read the .env configuration data from.</param>
	/// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
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
