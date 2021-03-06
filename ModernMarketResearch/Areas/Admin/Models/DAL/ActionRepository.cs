﻿using ModernMarketResearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ModernMarketResearch.Areas.Admin.Models.DAL
{
    public class ActionRepository:IActionRepository
    {
        ModernMarketResearchEntities db = new ModernMarketResearchEntities();

        public List<ModernMarketResearch.Models.ActionMaster> GetAction()
        {
            return db.ActionMasters.ToList();
        }

        public void InsertAction(ViewModel.ActionVM actionvm)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var s = serializer.Serialize(actionvm);
            ActionMaster actionmaster = serializer.Deserialize<ActionMaster>(s);
            db.ActionMasters.Add(actionmaster);
            db.SaveChanges();
        }

        public ViewModel.ActionVM GetActionById(int ActionId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAction(ViewModel.ActionVM actionvm)
        {
            throw new NotImplementedException();
        }

        public void DeleteAction(int ActionId)
        {
            var action = db.ActionMasters.Find(ActionId);
            db.ActionMasters.Remove(action);
            db.SaveChanges();   
        }
    }
}