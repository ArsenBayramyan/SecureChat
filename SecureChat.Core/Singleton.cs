using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.Core
{
    public class Singleton
    {
        private static Singleton instance;
        public string Name { get;private set; }
        private Singleton(string name)
        {
            this.Name = name;
        }
        public static Singleton getInstance(string name)
        {
            if (instance == null)
                instance = new Singleton(name);
            return instance;
        }
    }
}
