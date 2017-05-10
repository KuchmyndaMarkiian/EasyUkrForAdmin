using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyUkr.DataAccessLayer.Entities.Grammar;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Models.ViewModels
{
    public class AnswerModel
    {
        public List<GrammarAnswer> Selected { get; set; }
        public List<GrammarAnswer> Avaible { get; set; }

        public AnswerModel(int id)
        {
            var answers = DbManager.Instance.Data.GrammarAnswers;
            Selected = answers.Where(x=>x.GrammarTask.Id==id && x.IsCorrect.HasValue && x.IsCorrect.Value).ToList();
            Avaible= answers.Where(x => x.GrammarTask.Id == id).ToList(); 
        }
    }
}