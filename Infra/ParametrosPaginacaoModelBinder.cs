using InformaticaPecas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformaticaPecas.Infra
{
    public class ParametrosPaginacaoModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            ParametrosPaginacao parametrosPaginacao = new ParametrosPaginacao(request.Form);
            return parametrosPaginacao;
        }
    }
}