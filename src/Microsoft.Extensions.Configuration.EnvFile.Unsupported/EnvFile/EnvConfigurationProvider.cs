namespace Microsoft.Extensions.Configuration.EnvFile;

public class EnvConfigurationProvider : FileConfigurationProvider
{
	public EnvConfigurationProvider(EnvConfigurationSource source)
		: base(source)
	{
	}

	public override void Load(Stream stream)
	{
		base.Data = EnvStreamConfigurationProvider.Read(stream);
	}
}
