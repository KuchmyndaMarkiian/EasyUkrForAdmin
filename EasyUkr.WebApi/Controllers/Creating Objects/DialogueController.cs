using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUkr.WebApi.Infrastructure;
using EasyUkr.WebApi.Models.ViewModels;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers.Creating_Objects
{
    public class DialogueController : Controller
    {
        public ActionResult CreateDialogue()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDialogue(Dialogue model,HttpPostedFileBase uploadImage)
        {
            TempData["TopicSuccess"] = false;
            if (ModelState.IsValid && uploadImage != null)
            {
                
                var file =uploadImage;
                var path = AppDomain.CurrentDomain.BaseDirectory + '\\' + Static.DialoguePath ;
                Directory.CreateDirectory(path);
                path +=  file.FileName;
                file.SaveAs(path);
                var dialogue = new DataAccessLayer.Entities.Dialogue.Dialogue
                {
                    Header = model.Header,
                    DialogueUkr = model.TextUkr,
                    DialogueEng = model.TextEng,
                    FileAdress = "~" + Static.DialoguePath + file.FileName
                };
                DbManager.Instance.Data.Dialogues.Add(dialogue);
                DbManager.Instance.SaveChanges();
                TempData["TopicSuccess"] = true;
                return RedirectToAction("DialogueView", "Admin");
            }
            return View(model);
        }

        public ActionResult DeleteDialogue(int id)
        {
            TempData["DelSuccess"] = false;
            var dialogue = DbManager.Instance.Data.Dialogues.FirstOrDefault(x => x.Id == id);
            if (dialogue != null)
            {
                DbManager.Instance.Data.Dialogues.Remove(dialogue);
                DbManager.Instance.Data.SaveChanges();
                TempData["DelSuccess"] = true;
                return RedirectToAction("DialogueView", "Admin");
            }
            return HttpNotFound();
        }

    }
}