using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PluginModelUnitTests
{
    using System;
    using System.Runtime.Serialization;
    
    using NUnit.Framework;
    
    [TestFixture]
    public class ModelExceptionTest
    {
        [Test]
        public void TestMethod()
        {
            Model.ModelException exception1 = new Model.ModelException();
            
            Model.ModelException exception2 = new Model.ModelException("this is a test");
            
            Model.ModelException exception3 = new Model.ModelException("this is a test", new Exception());
            
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, exception3);
            
            stream.Seek(0, SeekOrigin.Begin);
            formatter.Deserialize(stream);
            
            stream.Close();
        }
    }
}
