using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor.Tokenizer.Symbols;

namespace FromsSystem.Models
{
    public class FormProvider : IProvider<form>
    {

        private FormsDBEntities1 db = new FormsDBEntities1();


        public int Delete(form t)
        {
            if (t == null) return 0;

            var model = db.form.ToList().FirstOrDefault(item => item.id == t.id);
            if (model == null) return 0;
            db.form.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        public int Insert(form t)
        {
            if (t == null) return 0;

            db.form.Add(t);
            int count = db.SaveChanges();
            return count;
        }

        public List<form> Select()
        {
            return db.form.ToList();
        }

        public List<form> SelectByKeyWord(string keyWord)
        {
            var data = db.form.ToList();
            if (!string.IsNullOrEmpty(keyWord))
            {
                data = db.form.Where(m => m.name.Contains(keyWord)
                || m.school.Contains(keyWord)
                || m.profile.Contains(keyWord)
                ).ToList();
            }
            return data;
        }

        public int Update(form t)
        {
            if (t == null) return 0;

            var model = db.form.ToList().FirstOrDefault(item => item.id == t.id);
            if (model == null) return 0;

            model.name = t.name;
            model.age = t.age;
            model.height = t.height;
            model.weight = t.weight;
            model.school = t.school;
            model.profile = t.profile;
            model.avatar = t.avatar;
            model.createTime = t.createTime;
            model.updateTime = t.updateTime;
            int count = db.SaveChanges();
            return count;
        }
    }
}