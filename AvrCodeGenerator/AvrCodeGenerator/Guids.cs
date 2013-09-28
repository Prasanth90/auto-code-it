// Guids.cs
// MUST match guids.h
using System;

namespace Company.AvrCodeGenerator
{
    static class GuidList
    {
        public const string guidAvrCodeGeneratorPkgString = "0c56bce0-0b3b-4f96-ba24-814761fca737";
        public const string guidAvrCodeGeneratorCmdSetString = "4e780b98-bf3e-4386-b4b9-432efff7aa3f";
        public const string guidToolWindowPersistanceString = "c83564e6-9b39-432e-a9f2-d07751cac21e";

        public static readonly Guid guidAvrCodeGeneratorCmdSet = new Guid(guidAvrCodeGeneratorCmdSetString);
    };
}