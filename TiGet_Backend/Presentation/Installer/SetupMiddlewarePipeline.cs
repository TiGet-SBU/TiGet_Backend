using Microsoft.AspNetCore.Rewrite;

namespace Presentation.Installer
{
    public static class SetupMiddlewarePipeline
    {
        public static WebApplication SetupMiddlewares(this WebApplication app)
        {
            
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TiGet API v1");
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("AllowAllOrigins");

            return app;
        }
    }
}
