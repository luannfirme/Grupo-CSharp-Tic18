namespace Prova_grupo.Domain
{
    public class Pagamento
    {
        public readonly string TipoCartao = "Cartão de crédito";
        public readonly string TipoBoleto = "Boleto Bancário";
        public readonly string TipoDinheiro = "Dinheiro em Espécie";

        public string Descricao { get; set; }
        public double Valor { get; set; }
        public double Desconto { get; set; }
        public DateTime Data{get; set;}
        public string Tipo { get; set; }
    }
}
