namespace Microsoft.Extensions.Configuration.EnvFile;

/// <summary>
/// An .env file based <see cref="FileConfigurationProvider"/>.
/// </summary>
public class EnvConfigurationProvider : FileConfigurationProvider
{
	/// <summary>ctor</summary>
	/// <param name="source">The <see cref="EnvConfigurationSource"/>.</param>
	public EnvConfigurationProvider(EnvConfigurationSource source)
		: base(source)
	{
	}

	/// <summary>
	/// Loads .env configuration key/values from a stream into a provider.
	/// </summary>
	/// <param name="stream">The <see cref="Stream"/> to load .env configuration data from.</param>
	public override void Load(Stream stream)
	{
		base.Data = EnvStreamConfigurationProvider.Read(stream);
	}
}
