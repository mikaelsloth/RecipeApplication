namespace Recipe.Presenter
{
    using System;

    public static class IServiceProviderExtension
    {
        public static T GetServiceType<T>(this IServiceProvider serviceProvider)
        {
            object obj = serviceProvider.GetService(typeof(T)) ?? throw new ArgumentOutOfRangeException("type", nameof(T), "Not defined by ServiceProvider - call IT development");
            return (T)obj;
        }
    }
}
