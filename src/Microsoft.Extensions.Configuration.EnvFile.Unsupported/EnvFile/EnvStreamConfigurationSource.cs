namespace Microsoft.Extensions.Configuration.EnvFile;

public class EnvStreamConfigurationSource : StreamConfigurationSource
{
	public override IConfigurationProvider Build(IConfigurationBuilder builder)
	{
		return new EnvStreamConfigurationProvider(this);
	}
}
