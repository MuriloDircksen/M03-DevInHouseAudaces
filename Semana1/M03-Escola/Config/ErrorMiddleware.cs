using M03_Escola.Exceptions;

namespace M03_Escola.Config
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Tudo escrito aqui será executado antes de chamar a controller da api
            try
            {
                await _next(context);
                // Tudo escrito aqui será executado depois de chamar a controller da api
            }
            catch (Exception ex)
            {
                FormatarExcecao(context, ex);
            }

        }

        private async void FormatarExcecao(HttpContext context, Exception ex)
        {
            string message = "Ocorreu um erro, tente novamente mais tarde";
            int status = 500;
            switch (ex)
            {
                case RegistroDuplicadoException _: 
                    //o _ significa que o sistema pode descartar a variavel, pq não será utilizada
                    message = ex.Message;
                    status = 409;
                    break;
                case ArgumentException _:
                case LoginInvalidoException _:
                    message = ex.Message;
                    status = 400;
                    break;
                case NotFoundException _:
                    message = ex.Message;
                    status = 404;
                    break;

            }

            context.Response.StatusCode = status;
            await context.Response.WriteAsync(message);

        }
    }
}
