using MEkroth.Matrix.Api.StatusMatrices.Infrastructure;

namespace MEkroth.Matrix.Api.StatusMatrices
{
    /// <summary>
    /// This is a bootstrap class to setup the dependency injection for Status Matrix feature.
    /// </summary>
    public static class StatusMatrixSetup
    {
        public static IServiceCollection AddStatusMatrix(this IServiceCollection services)
        {
            services.AddScoped<IStatusMatrixRepository, StatusMatrixRepository>();
            return services;
        }

        public static WebApplication UseStatusMatrix(this WebApplication app)
        {
            app.UseDeleteStatusMatrixEndpoint();
            app.UseGetAllStatusMatricesEndpoint();
            app.UseGetStatusMatrixEndpoint();
            app.UseSaveStatusMatrixEndpoint();
            return app;
        }
    }
}
