//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace RandomTrip
//{
//    public class Class1
//    {






////Vérifier la distance à vol d'oiseau pas trop grande évènement/logement et évènement/précédent point
//private boolean CheckDist(real latitudeTemp, real longitudeTemp, real latitudeLogement, real longitudeLogement, real latitudePrecedent, real latitudePrecedent, real DistMaxLogement, real DistMaxPrecedent)
//        {
//            bDistLogementOk = Math.Sqrt((LatitudeTemp - LatitudeLogement) * (LatitudeTemp - LatitudeLogement) + (LatitudeTemp - LatitudeLogement) * (LatitudeTemp - LatitudeLogement)) < DistMaxLogement;
//            bDistPrecedentOk = Math.Sqrt((LatitudeTemp - LatitudeLogement) * (LatitudeTemp - LatitudeLogement) + (LatitudeTemp - LatitudeLogement) * (LatitudeTemp - LatitudeLogement)) < DistMaxPrecedent;
//            bResult = bDistLogementOk and bDistPrecedentOk

//        return bResult;
//        }


//        Boucle jusqu'à validation par bouton imprimer

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
//latitudePrecedent = latitudeLogement
//longitPrecedent = longitPtPrecedent

//// choisir un évènement dans la base de donnée évènement
//// vérifier qu'il est en ardèche, la date et l'heure

//var query = from even in db.Evenement
//                            where(even.TYPE_EVENEMENT)=EVENEMENT && (even.LATITUDE != null) && (even.LATITUDE > 42) && (even.LATITUDE< 45) && (even.LONGITUDE > 2) && (even.LONGITUDE< 6) && (even.LATITUDE != null) && (even.DATE_DEBUT<DATE_VOULUE) && (even.DATE_FIN > DATE_VOULE) && (even.HEURE_DEBUT<HEURE_VOULUE) && (even.HEURE_FIN<HEURE_FIN)

//         checkDist(even.LATITUDE, even.LONGITUDE, latitudeLogement, longitudeLogement, latitudePrecedent, latitudePrecedent, DistMaxLogement, DistMaxPrecedent)),
//                            if bResult{
//				orderby Guid.NewGuid()

//                                select even;
//    }

//    var evenements = query.Take(1).ToList();



//    dans base de donnée activités
//    si la date est comprise entre entre la date de début et la date de fin
//        si l'heure est comprise entre 9h et 12h30
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




//}
//}