﻿using Microsoft.AspNet.Identity;
using RecomendacionMusicaZuquistrukis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecomendacionMusicaZuquistrukis
{
    public partial class mTuMusica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Context.User.Identity.GetUserId() != null)
                {
                    List<Cancion> lst = new List<Cancion>();
                    using (DBManualConnection db = new DBManualConnection())
                    {
                        lst = db.getCanciones(idUsuario: Context.User.Identity.GetUserId());
                    }
                    if (lst.Count != 0)
                    {
                        rep1.DataSource = lst;
                        rep1.DataBind();
                    }
                }
            }
        }

        protected void rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                List<Cancion> lstCancionesRecomendadasSinRepetir = rep1.DataSource as List<Cancion>;
                Repeater rep2 = (Repeater)e.Item.FindControl("rep2");
                List<Tag> lstTagsCancionesRecomendadasSinRepetir = new List<Tag>();
                Cancion cActual = e.Item.DataItem as Cancion;
                foreach (Cancion c in lstCancionesRecomendadasSinRepetir)
                {
                    if (c.Id == cActual.Id)
                    {
                        foreach (Tag t in c.Tags)
                        {
                            lstTagsCancionesRecomendadasSinRepetir.Add(t);
                        }
                    }
                }

                rep2.DataSource = lstTagsCancionesRecomendadasSinRepetir;
                rep2.DataBind();
            }
        }
    }
}