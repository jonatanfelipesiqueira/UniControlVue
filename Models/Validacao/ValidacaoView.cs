using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WEB.Models.Validacao
{
    public class ValidacaoView
    {
        public static IEnumerable<ModelStateErro> ErrosModelState(ModelStateDictionary modelState)
        {
            return (from model in modelState
                    where model.Value.Errors.Any()
                    select
                        new ModelStateErro
                        {
                            Propriedade = model.Key,
                            Erros = (from mensagem in model.Value.Errors
                                select mensagem.ErrorMessage).ToArray()
                        })
                .AsEnumerable();
        }
    }
}
