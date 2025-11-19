// Thomas_TrainingIndicator.cs
// Indicateur de formation simple pour NinjaTrader 8

#region Using declarations
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NinjaTrader.Data;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Tools;
using System.Windows.Media;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

namespace NinjaTrader.NinjaScript.Indicators
{
    /// <summary>
    /// Indicateur pédagogique qui trace une SMA configurable et une ligne horizontale
    /// représentant le dernier prix traité.
    /// </summary>
    public class Thomas_TrainingIndicator : Indicator
    {
        private SMA trainingSma;
        private const string LastPriceLineTag = "ThomasTraining_LastPrice";

        protected override void OnStateChange()
        {
            switch (State)
            {
                case State.SetDefaults:
                    Description = "Indicateur de formation : SMA configurable + ligne de dernier prix.";
                    Name = "Thomas_TrainingIndicator";
                    Calculate = Calculate.OnPriceChange; // Mise à jour temps réel
                    IsOverlay = true; // Trace directement sur le graphe de prix
                    IsSuspendedWhileInactive = true;
                    Period = 20;
                    ShowLastPriceLine = true;
                    AddPlot(Brushes.DodgerBlue, "TrainingSMA");
                    break;

                case State.DataLoaded:
                    // Instancie l'objet SMA une seule fois lorsque les données sont prêtes
                    trainingSma = SMA(Close, Period);
                    break;
            }
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < 0)
                return;

            // Alimente la série principale de l'indicateur avec la SMA
            Value[0] = trainingSma[0];

            // Optionnel : dessine une ligne horizontale au dernier prix
            if (ShowLastPriceLine)
            {
                Draw.HorizontalLine(this, LastPriceLineTag, Close[0], Brushes.Gray);
            }
            else
            {
                RemoveDrawObject(LastPriceLineTag);
            }
        }

        #region Propriétés
        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name = "Période SMA", Description = "Nombre de bars utilisées pour la moyenne.", GroupName = "Paramètres", Order = 0)]
        public int Period
        {
            get; set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Afficher la ligne du dernier prix", Description = "Active/désactive la ligne horizontale.", GroupName = "Paramètres", Order = 1)]
        public bool ShowLastPriceLine
        {
            get; set;
        }
        #endregion
    }
}
