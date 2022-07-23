namespace Microsoft.Extensions.Configuration.EnvFile;

/// <summary>
/// Represents an INI file as an <see cref="IConfigurationSource"/>.
/// Files are simple line structures (<a href="https://dev.to/aadilraza339/what-is-env-file-in-node-js-3h6c">.env files on dev.to</a>)
/// </summary>
/// <examples>
/// [Section:Header]
/// key1=value1
/// key2 = " value2 "
/// ; comment
/// # comment
/// / comment
/// </examples>
public class EnvStreamConfigurationSource : StreamConfigurationSource
{
	/// <summary>
	/// Builds the <see cref="EnvConfigurationProvider"/> for this source.
	/// </summary>
	/// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
	/// <returns>An <see cref="EnvConfigurationProvider"/></returns>
	public override IConfigurationProvider Build(IConfigurationBuilder builder)
	{
		return new EnvStreamConfigurationProvider(this);
	}
}
