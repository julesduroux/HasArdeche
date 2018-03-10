using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Google.Maps;
using Google.Maps.Geocoding;


namespace RandomTrip
{
    public partial class _Default : Page
    {
        System.Web.UI.WebControls.CheckBox Cbmatin;
        System.Web.UI.WebControls.CheckBox Cbmidi;
        System.Web.UI.WebControls.CheckBox Cbaprem;
        System.Web.UI.WebControls.CheckBox Cbdiner;
        System.Web.UI.WebControls.CheckBox Cbsoir;
        int nbLoad;
        Evenement pointDepart;
        String[] buttonNames = { "Make my day !", "Surpends-moi !", "Encore !", "Encore ?!", "Tu ne serais pas trop exigeant ?", "Montre m'en encore !", "Tu l'aimes ce bouton, hein ?", "Tu as lu ce que j'avais mis ?", "J'avais fait des efforts :(", "C'est sur, tu es-trop exigeant", "Ce n'est pas en me cliquant dessus que tu me feras taire", "Ca devient gênant là !" };
        int nameIndex = 0;


        protected void Page_Init(object sender, EventArgs e)
        {
            Cbmatin = new System.Web.UI.WebControls.CheckBox();
            Cbmatin.ID = "Cbmatin";
            Cbmidi = new System.Web.UI.WebControls.CheckBox();
            Cbmidi.ID = "Cbmidi";
            Cbaprem = new System.Web.UI.WebControls.CheckBox();
            Cbaprem.ID = "Cbaprem";
            Cbdiner = new System.Web.UI.WebControls.CheckBox();
            Cbdiner.ID = "Cbdiner";
            Cbsoir = new System.Web.UI.WebControls.CheckBox();
            Cbsoir.ID = "Cbsoir";
            nbLoad = 0;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            nbLoad++;
            listeElements.Controls.Clear();


            if ("".Equals(Date.Text))
            {
                Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if ("".Equals(Adresse.Text))
            {
                //Masque impression
                printButton.Attributes.Add("style", "display: none");
            }
            else
            {
                if (nbLoad == 6)
                {
                    pointDepart = Utils.GetCoordonnees(Adresse.Text);
                }
                
                //Adresse.Attributes.Add("style", "display: none");

                //Pour test :
                //System.Web.UI.HtmlControls.HtmlGenericControl lattitude = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
                //lattitude.InnerHtml = "Lattitude : "+pointDepart.LATITUDE.ToString();
                //listeElements.Controls.Add(lattitude);
                //System.Web.UI.HtmlControls.HtmlGenericControl longitude = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
                //longitude.InnerHtml = "Longitude : " + pointDepart.LONGITUDE.ToString();
                //listeElements.Controls.Add(longitude);



                //if (pointDepart.LONGITUDE != null)
                //{

                    printButton.Attributes.Add("style", "display: block; font-size: large;");

                    using (var db = new masterEntities())
                    {
                        var query = from even in db.Evenement
                                    where (even.LATITUDE != null) && (even.LATITUDE > 42) && (even.LATITUDE < 45) && (even.LONGITUDE > 2) && (even.LONGITUDE < 6) && "EVENEMENT".Equals(even.TYPE_EVENEMENT) && (even.URL_PHOTO != null)
                                    orderby Guid.NewGuid()
                                    select even;

                        var evenements = query.Take(3).ToList();

                        query = from even in db.Evenement
                                    where (even.LATITUDE != null) && (even.LATITUDE > 42) && (even.LATITUDE < 45) && (even.LONGITUDE > 2) && (even.LONGITUDE < 6) && "RESTAURANT".Equals(even.TYPE_EVENEMENT)
                                    orderby Guid.NewGuid()
                                    select even;

                        var restaurants = query.Take(2).ToList();


                        Evenement matin;
                        if (Cbmatin.Checked)
                        {
                            matin = (Evenement)Session["matin"];
                        }
                        else
                        {
                            matin = evenements[0];
                        }

                        Evenement midi;
                        if (Cbmidi.Checked)
                        {
                            midi = (Evenement)Session["midi"];
                        }
                        else
                        {
                            midi = restaurants[0];
                        }

                        Evenement aprem;
                        if (Cbaprem.Checked)
                        {
                            aprem = (Evenement)Session["aprem"];
                        }
                        else
                        {
                            aprem = evenements[1];
                        }

                        Evenement diner;
                        if (Cbdiner.Checked)
                        {
                            diner = (Evenement)Session["diner"];
                        }
                        else
                        {
                            diner = restaurants[1];
                        }

                        Evenement soir;
                        if (Cbsoir.Checked)
                        {
                            soir = (Evenement)Session["soir"];
                        }
                        else
                        {
                            soir = evenements[2];
                        }

                        //Ceci est un gros hack d&geulasse
                        if (nbLoad > 1)
                        {
                            Session["matin"] = matin;
                            Session["midi"] = midi;
                            Session["aprem"] = aprem;
                            Session["diner"] = diner;
                            Session["soir"] = soir;

                            if (Session["nameIndex"] != null)
                            { 
                                nameIndex = (int)Session["nameIndex"];
                            }


                            if (nameIndex >= buttonNames.Length)
                            {
                                nameIndex = 0;
                            }
                            search.Text = buttonNames[nameIndex];

                            nameIndex += 1;
                            Session["nameIndex"] = nameIndex;

                        }

                        Control activiteMatin = AfficherEvenement(matin, "Matinée", Cbmatin);
                        listeElements.Controls.Add(activiteMatin);

                        Control repasMidi = AfficherEvenement(midi, "Repas du midi", Cbmidi, "background-color : lightgrey; margin-top: 10px; margin-bottom: 10px", "Content/restau.png");
                        listeElements.Controls.Add(repasMidi);

                        Control activiteApresMidin = AfficherEvenement(aprem, "Après-midi", Cbaprem);
                        listeElements.Controls.Add(activiteApresMidin);

                        Control repasSoir = AfficherEvenement(diner, "Repas du soir", Cbdiner, "background-color : lightgrey; margin-top: 10px; margin-bottom: 10px", "Content/restau.png");
                        listeElements.Controls.Add(repasSoir);

                        Control activiteSoiree = AfficherEvenement(soir, "Soirée", Cbsoir);
                        listeElements.Controls.Add(activiteSoiree);

                    }
                //}
                //else
                //{
                //    System.Web.UI.HtmlControls.HtmlGenericControl affichageErreur = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
                //    affichageErreur.InnerHtml = pointDepart.ADRESSE;
                //    listeElements.Controls.Add(affichageErreur);
                //}
            }
            

                //Random rnd = new Random();
                //int IDrand = rnd.Next(1, 10000);

                //var evenement = new Evenement { ID = IDrand, EVENT_NAME = "test import from code", LATITUDE = (decimal)4.245451, LONGITUDE = (decimal)44.5245451 };
                //db.Evenement.Add(evenement);
                //db.SaveChanges();

                // Display all Blogs from the database 
                
                //GridViewResult.DataBind();


            
        }

        //Vérifier la distance à vol d'oiseau pas trop grande évènement/logement et évènement/précédent point
        //private Boolean CheckDist(double latitudeTemp, double longitudeTemp, double latitudeLogement, double longitudeLogement, double latitudePrecedent, double longitudePrecedent, double DistMaxLogement, double DistMaxPrecedent)
        //{
        //    Boolean bDistLogementOk = Math.Sqrt((latitudeTemp - latitudeLogement) * (latitudeTemp - latitudeLogement) + (longitudeTemp - longitudeLogement) * (longitudeTemp - longitudeLogement)) < DistMaxLogement && Math.Sqrt((latitudeTemp - latitudePrecedent) * (latitudeTemp - latitudePrecedent) + (longitudeTemp - longitudePrecedent) * (longitudeTemp - longitudePrecedent)) < DistMaxPrecedent;
        //    Boolean bDistPrecedentOk = Math.Sqrt((latitudeTemp - latitudePrecedent) * (latitudeTemp - latitudePrecedent) + (longitudeTemp - longitudePrecedent) * (longitudeTemp - longitudePrecedent)) < DistMaxPrecedent;

        //    return bDistLogementOk && bDistPrecedentOk;
        //}

        private Control AfficherEvenement (Evenement event1, String title, System.Web.UI.WebControls.CheckBox checkbox, String slyleCss="", String pathImage="")
        {
            //Crée la photo
            System.Web.UI.HtmlControls.HtmlImage image = new System.Web.UI.HtmlControls.HtmlImage();
            if ("".Equals(pathImage))
            { 
                image.Src = event1.URL_PHOTO;
            }
            else
            {
                image.Src = pathImage;
                image.Width = 140;
                image.Height = 140;
                //image.Attributes["sytle"] = "padding-top : 30px;";
            }

            //Crée le titre
            System.Web.UI.HtmlControls.HtmlGenericControl header = new System.Web.UI.HtmlControls.HtmlGenericControl("H2");
            header.InnerHtml = title;

            //Crée le nom
            System.Web.UI.HtmlControls.HtmlGenericControl p1 = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
            p1.InnerHtml = event1.EVENT_NAME;

            //Crée le descritif
            System.Web.UI.HtmlControls.HtmlGenericControl p2 = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
            p2.InnerHtml = event1.DESCRIPTIF;

            //Crée le site web
            System.Web.UI.HtmlControls.HtmlGenericControl website = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
            website.InnerHtml = event1.WEBSITE;

            //Crée le descritif
            System.Web.UI.HtmlControls.HtmlGenericControl p3 = new System.Web.UI.HtmlControls.HtmlGenericControl("H2");
            p3.InnerHtml = "Garde-moi !";


            //Organiser les éléments

            //Premiere div contenant l'image
            System.Web.UI.HtmlControls.HtmlGenericControl divImage = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divImage.Attributes["class"] = "col-3 col-lg-3";
            //if (!"".Equals(pathImage))
            //{
            //    divImage.Attributes["sytle"] = "padding-top: 10px; padding-left: 30px; ";
            //}
            divImage.Controls.Add(image);

            //Deuxième div pour le texte
            System.Web.UI.HtmlControls.HtmlGenericControl divTexte = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divTexte.Attributes["class"] = "col-7 col-lg-7";

            divTexte.Controls.Add(header);
            divTexte.Controls.Add(p1);
            divTexte.Controls.Add(p2);
            divTexte.Controls.Add(website);
            

            //troisième div contenant la checkbox
            System.Web.UI.HtmlControls.HtmlGenericControl divCB = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            divCB.Attributes["class"] = "col-2 col-lg-2";
            divCB.Attributes["align"] = "center";
            divCB.Controls.Add(p3);
            divCB.Controls.Add(checkbox);


            //Créer un conteneur
            System.Web.UI.HtmlControls.HtmlGenericControl conteneur = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            conteneur.Attributes["class"] = "row";

            conteneur.Controls.Add(divImage);
            conteneur.Controls.Add(divTexte);
            conteneur.Controls.Add(divCB);

            //Créer une div pour affecter background-color
            System.Web.UI.HtmlControls.HtmlGenericControl cssDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            cssDiv.Attributes["style"] = slyleCss;

            cssDiv.Controls.Add(conteneur);

             //Créer l'élément qui sera renvoyé
             System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            createDiv.Attributes["class"] = "col-12 col-lg-12";

            createDiv.Controls.Add(cssDiv);

            return createDiv;
        }

        //protected void btnExportasPDF_Click(object sender, EventArgs e)
        //{
        //    Response.ContentType = "application/pdf";

        //    Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");

        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //    StringWriter sw = new StringWriter();

        //    HtmlTextWriter hw = new HtmlTextWriter(sw);

        //    this.Page.RenderControl(hw);

        //    StringReader sr = new StringReader(sw.ToString());

        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);

        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        //    pdfDoc.Open();

        //    htmlparser.Parse(sr);

        //    pdfDoc.Close();

        //    Response.Write(pdfDoc);

        //    Response.End();
        //}

        


//-- initialisation
//LatLogement = 0
//LongitLogement=0
//LatActiviteMatin = 0
//LongActiviteMatin =0
//LatActiviteAprem = 0
//LongActiviteAprem =0
//LatActiviteSoir = 0
//LongActiviteSoir =0
//LatRestoMidi = 0
//LongRestoMidi =0
//LatRestoSoir = 0
//LongRestoSoir =0



//fonction CheckDistLogement
//racinecarrée(latitudeTemp - latitudeLogement)² + (longitudeTemp - longitudeLogement)² < DistMaxLogement

//fonction CheckDistPtPrecedent
//racinecarrée(latitudeTemp - latitudePtPrecedent)² + (longitudeTemp - longitudePrecedent)² < DistMaxActiv


//Boucle jusqu'à validation par bouton imprimer

//-- en cas de relance ne réinitialiser que les données pas validées
//if bValidActivMatin = 0
//	LongitLogement=0
//	LatActiviteMatin = 0
//endif
//....

//Valider l'adresse 
//LatLogement et LongitLogement = adresse du logement


//valider la date(pas une date dépassée)

//en fonction de la checkbox vélo mettre la variable bModeVelo à true

//-- distance max autorisée
//--(8 minutes d'angle = environ 15 km à vol d'oiseau)

//-- si voiture
//DistMaxActiv = 0.5 (environ 15km à vol d'oiseau)
//DistMAxLogement= 1 (environ 30km à vol d'oiseau)

//-- si velo
//DistMaxActiv = 0.2 (environ 5km à vol d'oiseau)
//DistMAxLogement= 0.4 (environ 10km à vol d'oiseau)


//-- déterminer activité du matin 
//-- sauf si validée
//latitudePtPrecedent = latitudeLogement
//longitPtPrecedent = longitPtPrecedent

//dans base de donnée activités
//si la date est comprise entre entre la date de début et la date de fin
//    si l'heure est comprise entre 9h et 12h30
//		random parmis(CheckDistLogement = true and CheckDistActiv = true)
//appel google maps pour avoir temps réel et km(en vélo ou en voiture selon)
//LatActiviteMatin = latTemp
//LongActiviteMatin = LongTemp


//-- déterminer le restaurant de midi
//-- sauf si validé
//latitudePtPrecedent = LatActiviteMatin
//longitPtPrecedent = LongActiviteMatin

//dans base de donnée restaurant + equipement/aire de picnic
//si la date est comprise entre entre la date de début et la date de fin
//    si l'heure est comprise entre 12h et 14h
//		random parmis(CheckDistLogement = true and CheckDistActiv = true)
//appel google maps pour avoir temps réel et km(en vélo ou en voiture selon)
//LatRestoMidi = latTemp
//LongitRestoMidi = LongTemp


//-- déterminer activité de l'après midi
//-- sauf si validée
//latitudePtPrecedent = LatRestoMidi
//longitPtPrecedent = LongitRestoMidi
//dans base de donnée activités
//si la date est comprise entre entre la date de début et la date de fin
//    si l'heure est comprise entre 14h et 18
//		random parmis(CheckDistLogement = true and CheckDistActiv = true)
//appel google maps pour avoir temps réel et km(en vélo ou en voiture selon)
//LatActiviteAprem = latTemp
//LongActiviteAprem = LongTemp



//-- déterminer le restaurant du soir
//-- sauf si validé
//latitudePtPrecedent = LatActiviteAprem
//longitPtPrecedent = LongActiviteAprem

//dans base de donnée restaurant
//si la date est comprise entre entre la date de début et la date de fin
//    si l'heure est comprise entre 19h et 22h
//		random parmis(CheckDistLogement = true and CheckDistActiv = true)
//appel google maps pour avoir temps réel et km(en vélo ou en voiture selon)
//LatRestoSoir = latTemp
//LongitRestoSoir = LongTemp



//-- déterminer activité du soir
//-- sauf si validée
//latitudePtPrecedent = LatRestoSoir
//longitPtPrecedent = LongitRestoSoir
//dans base de donnée activités
//si la date est comprise entre entre la date de début et la date de fin
//    si l'heure est comprise entre 21h et 3h
//		random parmis(CheckDistLogement = true and CheckDistActiv = true)
//appel google maps pour avoir temps réel et km(en vélo ou en voiture selon)
//LatActiviteSoir = latTemp
//LongActiviteSoir = LongTemp




//-- afficher le tout




    }
}