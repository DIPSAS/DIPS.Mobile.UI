using System.Collections.Generic;

namespace DIPS.Mobile.UI.Samples.SampleData
{
    public static class SampleDataStorage
    {
        public static List<Person> People = new List<Person>()
        {
            new Person("Erika", "Isa"),
            new Person("Adam", "Lehi"),
            new Person("Emmerson", "Harve"),
            new Person("Sylvester", "Christel"),
            new Person("Hanne", "Dexter"),
            new Person("Ava", "Karl"),
            new Person("Knut", "Arne", "Johansen"),
            new Person("Mina", "Shawn"),
            new Person("Per", "Gunnar", "Hansen"),
            new Person("Pablo", "Picasso",
                "Diego José Francisco de Paula Juan Nepomuceno María de los Remedios Cipriano de la Santísima Trinidad Ruiz y"),
        };
    }
}