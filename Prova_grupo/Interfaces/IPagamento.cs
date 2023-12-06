using Prova_grupo.Domain;

namespace Prova_grupo.Interfaces
{
    internal interface IPagamento
    {
        public void RealizarPagamento(int id, Pagamento pagamento);
    }
}
