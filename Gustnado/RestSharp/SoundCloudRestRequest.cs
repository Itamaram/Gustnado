namespace Gustnado.RestSharp
{
    public interface SoundCloudRestRequest<out T>
    {
        T Execute(SoundCloudHttpClient client);
    }
}