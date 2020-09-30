namespace BibliotecaVirtual.Application.Interfaces
{
    public interface IBaseService
    {
        /// <summary>
        /// Retorna se a operação foi bem sucedida.
        /// </summary>
        /// <returns></returns>
        bool IsSuccessful();

        /// <summary>
        /// Retorna as mensagens de erro da validação da regra de negócios.
        /// </summary>
        /// <returns></returns>
        string GetModelErrors();        
    }
}
