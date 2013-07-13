using System;

using Model.Quote;
using Model.Template;
using Model;

namespace SampleProperties
{
    class Weights
    {
        public static decimal CalcWeight(Model.Common.Header header)
        {
            decimal retValue = 0;
            foreach(Model.Common.Detail q in header.Details)
            {
                if (q.IsWire)
                {
                    DisplayableWireProperties p 
                        = (DisplayableWireProperties) q.QuoteDetailProperties;

                    decimal w = p.PoundsPer1000Feet;
                    retValue += q.Qty / (decimal)3.048 * w / 1000;
                }
            }

            return Math.Round(retValue, 4);
        }

    }
}
