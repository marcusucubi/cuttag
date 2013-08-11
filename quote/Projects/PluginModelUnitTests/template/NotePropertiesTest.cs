namespace PluginModelUnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class NotePropertiesTest
    {
        [Test]
        public void TestMethod()
        {
            Host.App.RegisteredClasses.Clear();
                
            Model.Template.Header header = new Model.Template.Header();
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            Model.Template.NoteProperties notes = header.NoteProperties as Model.Template.NoteProperties;
            
            Assert.IsNotNull(notes);
            
            notes.Note2Customer = "test";
            notes.Note = "test";
            
            Assert.AreEqual(notes.Note2Customer, "test");
            Assert.AreEqual(notes.Note, "test");
        }
    }
}
