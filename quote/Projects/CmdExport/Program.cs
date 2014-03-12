using System;
using System.Data.SqlClient;
using System.Xml;

using Model.IO;
using Model.Template;
using Model.Template.Ext;

namespace CmdExport
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Connecting...");
            
            Host.App.Init();
            
            SqlConnection c = DB.CuttagDatabaseConnection.Connection;
            
            Console.WriteLine("Loading...");
            
            DB.QuoteDataBaseTableAdapters._QuoteTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuoteTableAdapter();
            DB.QuoteDataBase._QuoteDataTable table = new DB.QuoteDataBase._QuoteDataTable();

            adaptor.FillByWithQuotes(table);
            
            using (XmlTextWriter writer = new XmlTextWriter(Console.Out))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Quotes");
                
                for(int i = 0; i < table.Rows.Count; i++)
                {
                    writer.WriteStartElement("Quote");
                    DB.QuoteDataBase._QuoteRow row = table.Rows[i] as DB.QuoteDataBase._QuoteRow;
                    
                    Model.Template.Header header = TemplateLoader.Load((int) row.id);
                    
                    DekalbProperties.DisplayableComputationProperties comp = 
                        header.ComputationProperties as DekalbProperties.DisplayableComputationProperties;
                    
                    IComputationWrapper wrapper = comp;
                    DekalbProperties.DekalbComputationProperties realComp =
                        wrapper.ComputationProperties as DekalbProperties.DekalbComputationProperties;
                    
                    writer.WriteElementString("ID", row.id.ToString());
                    writer.WriteElementString("TotalUnitCost", comp.TotalUnitCost.ToString());
                    writer.WriteElementString("SummaryProfit", comp.SummaryProfit);
                    
                    writer.WriteEndElement();
                    
                    writer.Flush();
                }
                
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
	
        }
    }
}