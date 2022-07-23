namespace Microsoft.Extensions.Configuration.EnvFile;

/// <summary>
/// An .env file based <see cref="StreamConfigurationProvider"/>.
/// </summary>
public class EnvStreamConfigurationProvider : StreamConfigurationProvider
{
	/// <summary>ctor</summary>
	/// <param name="source">The <see cref="EnvStreamConfigurationSource"/>.</param>
	public EnvStreamConfigurationProvider(EnvStreamConfigurationSource source)
		: base(source)
	{
	}

	/// <summary>
	/// Loads .env configuration key/values from a stream into a provider.
	/// </summary>
	/// <param name="stream">The <see cref="Stream"/> to load .env configuration data from.</param>
	public override void Load(Stream stream)
	{
		base.Data = Read(stream);
	}

	/// <summary>
	/// Read a stream of .env values into a key/value dictionary.
	/// </summary>
	/// <param name="stream">The stream of .env data.</param>
	/// <returns>The <see cref="IDictionary{String, String}"/> which was read from the stream.</returns>
	public static IDictionary<string, string> Read(Stream stream)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		using StreamReader streamReader = new StreamReader(stream);

		//var sectionPrefix  = string.Empty; //holder variable used by ini file reader

		while (streamReader.Peek() != -1)
		{
			var rawLine = streamReader.ReadLine();
			var trimmedLine = rawLine.Trim();

			// Ignore blank lines
			if (string.IsNullOrWhiteSpace(trimmedLine))
			{
				continue;
			}
			// Ignore comments
			if (trimmedLine[0] == ';' || trimmedLine[0] == '#' || trimmedLine[0] == '/')
			{
				continue;
			}

			////we are commenting out this block because env files will not have section headers like ini files
			//if (trimmedLine[0] == '[' && trimmedLine[trimmedLine.Length - 1] == ']')
			//{
			//	sectionPrefix  = trimmedLine.Substring(1, trimmedLine.Length - 2) + ConfigurationPath.KeyDelimiter;
			//	continue;
			//}

			// key = value OR "value"
			int separator = trimmedLine.IndexOf('=');
			if (separator < 0)
			{
				throw new FormatException($"UnrecognizedLineFormat {rawLine}");
			}

			var configKey = /*sectionPrefix +*/ trimmedLine.Substring(0, separator).Trim();
			//this is what is different than ini because of the keys; AddEnvironment and AddKeyPerFile have to use __ as delimeters and replace with ':' to be acceptable by the OS
			configKey = configKey.Replace("__", ":");

			if (dictionary.ContainsKey(configKey))
			{
				throw new FormatException($"Error_KeyIsDuplicated {configKey}");
			}

			var configValue = trimmedLine.Substring(separator + 1).Trim();

			//this block strips the quotes from quoted values
			if (configValue.Length > 1 && configValue[0] == '"' && configValue[configValue.Length - 1] == '"')
			{
				configValue = configValue.Substring(1, configValue.Length - 2);
			}

			dictionary[configKey] = configValue;
		}
		return dictionary;
	}
}
