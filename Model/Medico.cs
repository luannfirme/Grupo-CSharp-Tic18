namespace ProvaGrupoNET;

public class Medico : Pessoa
{
    public string Crm { get; set; }

    public override string ValidarCPF(List<string> cpfs)
    {
        var baseValidation = base.ValidarCPF(cpfs);

        if (baseValidation != null)
            return baseValidation;

        return null;
    }

    public string ValidarCRM(List<Medico> medicos){

        if (medicos.Any(m => m.Crm == Crm))
            return "CRM já cadastrado";

        return null;
    }
}
