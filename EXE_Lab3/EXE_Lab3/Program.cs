using System;

namespace EXE_Lab3
{
    public class Program
    {
        public enum TFirstBirds { tit, stork, kiwi }
        private enum TSecondBirds { tit, stork, kiwi }
        static void Main(string[] args)
        {
            DLL_Lab3.Birds bird = new DLL_Lab3.Birds("Tit", AllTypes.TLivingEnvironment.land_air, AllTypes.TClassBirds.flying);
            DLL_Lab3.Mammals mammal = new DLL_Lab3.Mammals("Cat", AllTypes.TLivingEnvironment.land_air, AllTypes.TClassMammals.placentals);

            Console.WriteLine(bird.PrintInfo());
            Console.WriteLine(mammal.PrintInfo());
        }
    }
}

namespace DLL_Lab3
{
    public interface IAnimalPlugin
    {
        string Name { get; set; }
        void AskInfo(Object[] args);
        string PrintInfo();
        Object[] GetInfo();
    }
    public abstract class Animals : IAnimalPlugin
    {
        public string AnimalType { get; set; }
        public string Name { get; set; }
        public Animals() { }
        public Animals(string AnimalType)
        {
            this.AnimalType = AnimalType;
        }
        public abstract void AskInfo(Object[] args);
        public abstract string PrintInfo();
        public abstract Object[] GetInfo();
    }

    public abstract class Vertebrate : Animals
    {
        public Vertebrate() { }
        public AllTypes.TLivingEnvironment LivingEnvironment { get; set; }
        public Vertebrate(string AnimalType) : base(AnimalType) { }
        public override void AskInfo(Object[] args) { }
        public override string PrintInfo()
        {
            return $"You choose a vertebrate animal '{AnimalType}' with" + ((LivingEnvironment == AllTypes.TLivingEnvironment.land_air) ? "land-air" : "{LivingEnvironment}") + "living environment";
        }
    }

    public class Birds : Vertebrate
    {
        public Birds()
        {
            Name = "Birds";
        }
        public AllTypes.TClassBirds ClassBirds { get; set; }
        public Birds(string AnimalType, AllTypes.TLivingEnvironment LivingEnvironment, AllTypes.TClassBirds ClassBirds) : base(AnimalType)
        {
            Name = "Birds";
            this.LivingEnvironment = LivingEnvironment;
            this.ClassBirds = ClassBirds;
        }
        public override string PrintInfo()
        {
            return $"A {ClassBirds} bird '{AnimalType}' with " + (LivingEnvironment == AllTypes.TLivingEnvironment.land_air ? "land-air" : $"{LivingEnvironment}") + " living environment";
        }

        public override void AskInfo(Object[] args)
        {
            AnimalType = (string)args[0];
            LivingEnvironment = (AllTypes.TLivingEnvironment)args[1];
            ClassBirds = (AllTypes.TClassBirds)args[2];
        }
        public override Object[] GetInfo()
        {
            Object[] obj = new Object[3];
            obj[0] = AnimalType;
            obj[1] = LivingEnvironment;
            obj[2] = ClassBirds;
            return obj;
        }
    }

    public class Mammals : Vertebrate
    {
        public Mammals()
        {
            Name = "Mammals";
        }
        public Mammals(string AnimalType, AllTypes.TLivingEnvironment LivingEnvironment, AllTypes.TClassMammals ClassMammals) : base(AnimalType)
        {
            Name = "Mammals";
            this.LivingEnvironment = LivingEnvironment;
            this.MammalsClass = ClassMammals;
        }
        public AllTypes.TClassMammals MammalsClass { get; set; }
        public override string PrintInfo()
        {
            return $"A {MammalsClass} mammal '{AnimalType}' with " + (LivingEnvironment == AllTypes.TLivingEnvironment.land_air ? "land-air" : $"{LivingEnvironment}") + " living environment";
        }

        public override void AskInfo(Object[] args)
        {
            AnimalType = (string)args[0];
            LivingEnvironment = (AllTypes.TLivingEnvironment)args[1];
            MammalsClass = (AllTypes.TClassMammals)args[2];
        }
        public override Object[] GetInfo()
        {
            Object[] obj = new Object[3];
            obj[0] = AnimalType;
            obj[1] = LivingEnvironment;
            obj[2] = MammalsClass;
            return obj;
        }
    }
}

namespace AllTypes
{
    public enum TClassBirds { flying = 1, walking }
    public enum TClassInsects { flying = 1, crawling, running, jumping }
    public enum TSymmetry { radial = 1, bilateral }
    public enum TClassMammals { protozoans = 1, marsupials, placentals }
    public enum TClassMollusca { cephalopods = 1, gastropods, bivalves }
    public enum TTail { HasTail = 1, HasNotTail }
    public enum TLivingEnvironment { water = 1, land_air, soil, organism }
}

